using Soenneker.Blazor.Utils.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public sealed class ToastUtil : IToastUtil
{
    private const int ToastLimit = 1;
    private readonly List<ToastEntry> _toasts = [];

    public event Action? Changed;

    public IReadOnlyList<ToastEntry> Toasts => _toasts;

    public ValueTask<string> Toast(string title, Action<ToastOptions>? configure = null, CancellationToken cancellationToken = default)
    {
        ToastOptions options = new();
        configure?.Invoke(options);

        ToastEntry toast = new()
        {
            Id = BlazorIdGenerator.New("quark-toast"),
            Title = title,
            Description = options.Description,
            Variant = options.Variant,
            ActionLabel = options.ActionLabel,
            ActionAltText = options.ActionAltText,
            Action = options.Action,
            Duration = options.Duration,
            Open = true
        };

        _toasts.Insert(0, toast);

        if (_toasts.Count > ToastLimit)
            _toasts.RemoveRange(ToastLimit, _toasts.Count - ToastLimit);

        NotifyChanged();
        return ValueTask.FromResult(toast.Id);
    }

    public ValueTask Dismiss(string? id = null, CancellationToken cancellationToken = default)
    {
        foreach (ToastEntry toast in GetMatchingToasts(id))
        {
            toast.Open = false;
        }

        NotifyChanged();
        return ValueTask.CompletedTask;
    }

    public ValueTask Remove(string? id = null, CancellationToken cancellationToken = default)
    {
        if (id is null)
            _toasts.Clear();
        else
            _toasts.RemoveAll(toast => string.Equals(toast.Id, id, StringComparison.Ordinal));

        NotifyChanged();
        return ValueTask.CompletedTask;
    }

    private IEnumerable<ToastEntry> GetMatchingToasts(string? id)
    {
        return id is null
            ? _toasts.ToArray()
            : _toasts.Where(toast => string.Equals(toast.Id, id, StringComparison.Ordinal)).ToArray();
    }

    private void NotifyChanged()
    {
        Changed?.Invoke();
    }
}
