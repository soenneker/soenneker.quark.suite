using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="ICollapseCoordinator"/>
public sealed class CollapseCoordinator : ICollapseCoordinator
{
    private readonly object _lock = new();
    private readonly HashSet<Collapse> _collapses = [];

    public void Register(Collapse collapse)
    {
        if (collapse == null)
            return;

        lock (_lock)
        {
            _collapses.Add(collapse);
        }
    }

    public void Unregister(Collapse collapse)
    {
        if (collapse == null)
            return;

        lock (_lock)
        {
            _collapses.Remove(collapse);
        }
    }

    public async Task ToggleTargets(string? targetExpression)
    {
        if (string.IsNullOrWhiteSpace(targetExpression))
            return;

        Collapse[] snapshot;
        lock (_lock)
        {
            snapshot = [.. _collapses];
        }

        if (snapshot.Length == 0)
            return;

        var tokens = targetExpression.Split([',', ' '], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var token in tokens)
        {
            if (token.StartsWith(".", StringComparison.Ordinal))
            {
                var className = token[1..];
                if (className.Length == 0)
                    continue;

                foreach (var collapse in snapshot)
                {
                    if (collapse.HasCssClass(className))
                        await collapse.Toggle();
                }

                continue;
            }

            var id = token.StartsWith("#", StringComparison.Ordinal) ? token[1..] : token;
            if (id.Length == 0)
                continue;

            foreach (var collapse in snapshot)
            {
                if (string.Equals(collapse.Id, id, StringComparison.Ordinal))
                {
                    await collapse.Toggle();
                    break;
                }
            }
        }
    }
}
