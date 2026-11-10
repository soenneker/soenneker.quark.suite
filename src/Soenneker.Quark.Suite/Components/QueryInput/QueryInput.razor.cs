using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

public partial class QueryInput
{
    [Inject] private IInputInterop InputInterop { get; set; } = null!;
    [Parameter] public string Value { get; set; } = "";
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public EventCallback<QueryInputResult> ParsedChanged { get; set; }
    [Parameter] public IReadOnlyList<QueryInputField> Fields { get; set; } = [];
    [Parameter] public Func<QueryInputRequest, CancellationToken, ValueTask<IReadOnlyList<SelectOption>>>? SuggestValues { get; set; }
    [Parameter] public string? Placeholder { get; set; } = "Search or add a filter...";
    [Parameter] public string Label { get; set; } = "Search query";
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool UseSuggestionLabels { get; set; }
    [Parameter] public int MaxSuggestions { get; set; } = 10;

    private readonly string _listId = $"query-options-{Guid.NewGuid():N}";
    private ElementReference _input;
    private IReadOnlyList<SelectOption> _options = [];
    private CancellationTokenSource? _request;
    private long _revision;
    private bool _focused, _open, _registered;
    private int _active = -1, _start, _end;
    private string? _field;
    private bool _excluded;
    private string _observedValue = "";
    private int? _restoreCaret;
    private bool _completeValues;
    private readonly List<QueryInputSelection> _selections = [];

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(MaxSuggestions);
        Value ??= "";
        if (Value != _observedValue) _selections.Clear();
        if (Disabled || Value != _observedValue) Close();
        _observedValue = Value;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (!_registered)
        {
            await InputInterop.RegisterComboboxKeys(_input);
            _registered = true;
        }
        if (_restoreCaret is { } caret)
        {
            _restoreCaret = null;
            await InputInterop.RestoreSelection(_input, caret, caret, Value);
            if (_completeValues)
            {
                _completeValues = false;
                await RefreshSuggestions();
                await InvokeAsync(StateHasChanged);
            }
        }
    }

    private string OptionId(int index) => $"{_listId}-{index}";

    private void CancelRequest()
    {
        _revision++;
        _request?.Cancel();
        _request = null;
    }

    private void Close()
    {
        CancelRequest();
        _open = false;
        _active = -1;
        _options = [];
    }

    private async Task Publish(string value)
    {
        Value = _observedValue = value;
        QueryInputResult parsed = Parse(value);
        await ValueChanged.InvokeAsync(value);
        await ParsedChanged.InvokeAsync(parsed);
    }

    private QueryInputResult Parse(string text)
    {
        foreach (QueryInputSelection selection in _selections.OrderByDescending(s => s.Start))
            text = text.Remove(selection.Start, selection.Length).Insert(selection.Start, selection.Query);
        return QueryInputParser.Parse(text, Fields);
    }

    private void TrackEdit(string before, string after)
    {
        int start = 0;
        while (start < before.Length && start < after.Length && before[start] == after[start]) start++;
        int end = before.Length, nextEnd = after.Length;
        while (end > start && nextEnd > start && before[end - 1] == after[nextEnd - 1]) { end--; nextEnd--; }
        int delta = after.Length - before.Length;
        for (int i = _selections.Count - 1; i >= 0; i--)
        {
            QueryInputSelection selection = _selections[i];
            if (selection.Start + selection.Length < start) continue;
            if (selection.Start > end)
                _selections[i] = selection with { Start = selection.Start + delta };
            else
                _selections.RemoveAt(i);
        }
    }

    private async Task HandleInput(ChangeEventArgs args)
    {
        if (Disabled) return;
        Close();
        string updated = args.Value?.ToString() ?? "";
        TrackEdit(Value, updated);
        await Publish(updated);
        await RefreshSuggestions();
    }

    private async Task HandleFocus()
    {
        _focused = true;
        await RefreshSuggestions();
    }

    private void HandleBlur()
    {
        _focused = false;
        Close();
    }

    private async Task RefreshSuggestions()
    {
        Close();
        if (Disabled || !_focused || IsDisposed) return;
        long revision = _revision;
        string query = Value;
        InputSelectionSnapshot? selection = await InputInterop.GetSelection(_input);
        if (revision != _revision || (selection?.Value is { } live && live != query)) return;
        int caret = Math.Clamp(selection?.Start ?? query.Length, 0, query.Length);
        QueryInputToken? token = QueryInputParser.Tokenize(query).FirstOrDefault(t => t.Start <= caret && caret <= t.End);
        if (token is not null && QueryInputParser.IsOperator(token)) return;
        _start = token?.Start ?? caret;
        _end = token?.End ?? caret;
        QueryInputToken? prefix = QueryInputParser.Tokenize(query[_start..caret]).FirstOrDefault();
        string fragment = prefix?.Text ?? "";
        _excluded = fragment.StartsWith('-');
        int colon = prefix?.Colon ?? -1;
        _field = colon >= 0 ? fragment[(_excluded ? 1 : 0)..colon] : null;
        string search = colon >= 0 ? fragment[(colon + 1)..] : fragment[(_excluded ? 1 : 0)..];
        IReadOnlyList<SelectOption> options;
        if (_field is null)
            options = Fields.Where(f => Matches(f.Key, search) || Matches(f.Label, search))
                .Select(f => new SelectOption(f.Key, f.Label + ":")).ToArray();
        else
        {
            QueryInputField? field = Fields.FirstOrDefault(f => string.Equals(f.Key, _field, StringComparison.OrdinalIgnoreCase));
            if (field is null) return;
            _field = field.Key;
            if ((field.SuggestValues ?? SuggestValues) is { } provider)
            {
                using var request = new CancellationTokenSource();
                _request = request;
                try
                {
                    // Value completion can start after rendering, outside the normal event render cycle.
                    await InvokeAsync(StateHasChanged);
                    options = await provider(new QueryInputRequest(field.Key, search, query) { Parsed = Parse(query), MaxSuggestions = MaxSuggestions }, request.Token);
                }
                catch (OperationCanceledException) when (request.IsCancellationRequested) { return; }
                catch (Exception exception)
                {
                    if (revision == _revision) ReportError(exception, "query suggestions");
                    return;
                }
                finally
                {
                    if (ReferenceEquals(_request, request))
                    {
                        _request = null;
                        if (!IsDisposed) await InvokeAsync(StateHasChanged);
                    }
                }
            }
            else options = (field.Values ?? []).Where(v => Matches(v.Value, search) || Matches(v.Label, search)).ToArray();
        }
        if (revision != _revision || !_focused || Disabled || IsDisposed) return;
        _options = options.Where(o => !o.Disabled).Take(MaxSuggestions).ToArray();
        _active = _options.Count > 0 ? 0 : -1;
        _open = _options.Count > 0;
    }

    private static bool Matches(string value, string search) => value.Contains(search, StringComparison.OrdinalIgnoreCase);

    private async Task Choose(int index)
    {
        if (Disabled || !_open || index < 0 || index >= _options.Count) return;
        SelectOption option = _options[index];
        string value = option.Value;
        bool completingField = _field is null;
        string replacement = (_excluded ? "-" : "") + (completingField ? value + ":" : _field + ":" + QueryInputParser.Quote(UseSuggestionLabels ? option.Label : value));
        string suffix = Value[_end..];
        if (!completingField && (suffix.Length == 0 || !char.IsWhiteSpace(suffix[0]))) replacement += " ";
        int caret = _start + replacement.Length;
        string updated = Value[.._start] + replacement + suffix;
        TrackEdit(Value, updated);
        if (!completingField && UseSuggestionLabels)
            _selections.Add(new QueryInputSelection(_start, replacement.TrimEnd().Length,
                (_excluded ? "-" : "") + _field + ":" + QueryInputParser.Quote(value)));
        Close();
        _restoreCaret = caret;
        _completeValues = completingField;
        await Publish(updated);
        // Render the replacement before reading the browser caret for the next qualifier's values.
        await InvokeAsync(StateHasChanged);
    }

    private async Task HandleKeyDown(KeyboardEventArgs args)
    {
        if (Disabled || args.AltKey || args.CtrlKey || args.MetaKey) return;
        switch (args.Key)
        {
            case "Escape": Close(); break;
            case "ArrowDown":
            case "ArrowUp":
                if (!_open) await RefreshSuggestions();
                else _active = (_active + (args.Key == "ArrowDown" ? 1 : _options.Count - 1)) % _options.Count;
                break;
            case "Home" when _open: _active = 0; break;
            case "End" when _open: _active = _options.Count - 1; break;
            case "Enter" when _open: await Choose(_active); break;
            case "Tab": Close(); break;
        }
    }

    private Task HandleKeyUp(KeyboardEventArgs args) => args.Key is "ArrowLeft" or "ArrowRight" || (args.Key is "Home" or "End" && !_open)
        ? RefreshSuggestions() : Task.CompletedTask;

    public override async ValueTask DisposeAsync()
    {
        Close();
        if (_registered) await InputInterop.UnregisterComboboxKeys(_input);
        await base.DisposeAsync();
    }
}
