using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <inheritdoc cref="ICollapseCoordinator"/>
public sealed class CollapseCoordinator : ICollapseCoordinator
{
    private readonly object _gate = new();
    private readonly HashSet<Collapse> _collapses = [];

    public ValueTask Register(Collapse collapse)
    {
        if (collapse is null)
            return ValueTask.CompletedTask;

        lock (_gate)
        {
            _collapses.Add(collapse);
        }

        return ValueTask.CompletedTask;
    }

    public ValueTask Unregister(Collapse collapse)
    {
        if (collapse is null)
            return ValueTask.CompletedTask;

        lock (_gate)
        {
            _collapses.Remove(collapse);
        }

        return ValueTask.CompletedTask;
    }

    public async ValueTask ToggleTargets(string? targetExpression)
    {
        if (targetExpression.IsNullOrWhiteSpace())
            return;

        Collapse[] snapshot;
        lock (_gate)
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
