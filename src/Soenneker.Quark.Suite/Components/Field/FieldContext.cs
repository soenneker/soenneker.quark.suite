using System;
using System.Collections.Generic;
using System.Linq;
using Soenneker.Blazor.Utils.Ids;

namespace Soenneker.Quark;

internal sealed class FieldContext
{
    private int _descriptionRegistrations;
    private int _errorRegistrations;
    private bool _explicitInvalid;
    private bool _validationInvalid;

    public event Action? StateChanged;

    public FieldContext()
    {
        var baseId = BlazorIdGenerator.New("quark-field");
        ControlId = BlazorIdGenerator.Child(baseId, "control");
        DescriptionId = BlazorIdGenerator.Child(baseId, "description");
        ErrorId = BlazorIdGenerator.Child(baseId, "error");
    }

    public string ControlId { get; }

    public string DescriptionId { get; }

    public string ErrorId { get; }

    public bool Horizontal { get; private set; }

    public bool IsInvalid => _explicitInvalid || _validationInvalid;

    public bool HasDescription => _descriptionRegistrations > 0;

    public bool HasError => _errorRegistrations > 0;

    public void Update(bool horizontal, bool isInvalid)
    {
        if (Horizontal == horizontal && _explicitInvalid == isInvalid)
            return;

        Horizontal = horizontal;
        _explicitInvalid = isInvalid;
        StateChanged?.Invoke();
    }

    public void SetValidationInvalid(bool isInvalid)
    {
        if (_validationInvalid == isInvalid)
            return;

        _validationInvalid = isInvalid;
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

    public string? BuildDescribedBy(string? existing, bool isInvalid = false)
    {
        var ids = new List<string>();

        if (!string.IsNullOrWhiteSpace(existing))
        {
            ids.AddRange(existing.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        }

        if (HasDescription)
            ids.Add(DescriptionId);

        if ((IsInvalid || isInvalid) && HasError)
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
