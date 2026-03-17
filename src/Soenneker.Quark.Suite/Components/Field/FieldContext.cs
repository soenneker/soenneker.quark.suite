using System;
using System.Collections.Generic;
using System.Linq;

namespace Soenneker.Quark;

internal sealed class FieldContext
{
    private int _descriptionRegistrations;
    private int _errorRegistrations;

    public event Action? StateChanged;

    public FieldContext()
    {
        var id = Guid.NewGuid().ToString("N");
        ControlId = $"field-{id}-control";
        DescriptionId = $"field-{id}-description";
        ErrorId = $"field-{id}-error";
    }

    public string ControlId { get; }

    public string DescriptionId { get; }

    public string ErrorId { get; }

    public bool Horizontal { get; private set; }

    public bool IsInvalid { get; private set; }

    public bool HasDescription => _descriptionRegistrations > 0;

    public bool HasError => _errorRegistrations > 0;

    public void Update(bool horizontal, bool isInvalid)
    {
        if (Horizontal == horizontal && IsInvalid == isInvalid)
            return;

        Horizontal = horizontal;
        IsInvalid = isInvalid;
        StateChanged?.Invoke();
    }

    public IDisposable RegisterDescription()
    {
        _descriptionRegistrations++;
        StateChanged?.Invoke();
        return new Registration(() =>
        {
            _descriptionRegistrations--;
            StateChanged?.Invoke();
        });
    }

    public IDisposable RegisterError()
    {
        _errorRegistrations++;
        StateChanged?.Invoke();
        return new Registration(() =>
        {
            _errorRegistrations--;
            StateChanged?.Invoke();
        });
    }

    public string? BuildDescribedBy(string? existing)
    {
        var ids = new List<string>();

        if (!string.IsNullOrWhiteSpace(existing))
        {
            ids.AddRange(existing.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        }

        if (HasDescription)
            ids.Add(DescriptionId);

        if (IsInvalid && HasError)
            ids.Add(ErrorId);

        return ids.Count == 0 ? null : string.Join(" ", ids.Distinct(StringComparer.Ordinal));
    }

    private sealed class Registration : IDisposable
    {
        private Action? _dispose;

        public Registration(Action dispose)
        {
            _dispose = dispose;
        }

        public void Dispose()
        {
            var dispose = _dispose;
            _dispose = null;
            dispose?.Invoke();
        }
    }
}
