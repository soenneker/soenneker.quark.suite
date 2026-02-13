using System;
using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

///<inheritdoc cref="IOffcanvasCoordinator"/>
public sealed class OffcanvasCoordinator : IOffcanvasCoordinator
{
    private readonly Dictionary<string, bool> _enteredById = new();
    private int _activeCount;

    public int ActiveCount => _activeCount;

    public event Action? StateChanged;

    public void Register(string id)
    {
        if (id.IsNullOrWhiteSpace())
            return;
        if (!_enteredById.ContainsKey(id))
            _enteredById[id] = false;
    }

    public void Unregister(string id)
    {
        if (!_enteredById.TryGetValue(id, out bool entered))
            return;
        if (entered)
        {
            _activeCount = Math.Max(0, _activeCount - 1);
            OnChanged();
        }
        _enteredById.Remove(id);
    }

    public void Enter(string id)
    {
        if (!_enteredById.TryGetValue(id, out bool entered) || entered)
            return;
        _enteredById[id] = true;
        _activeCount++;
        OnChanged();
    }

    public void Exit(string id)
    {
        if (!_enteredById.TryGetValue(id, out bool entered) || !entered)
            return;
        _enteredById[id] = false;
        _activeCount = Math.Max(0, _activeCount - 1);
        OnChanged();
    }

    private void OnChanged() => StateChanged?.Invoke();
}


