using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Soenneker.Extensions.String;
using Soenneker.Asyncs.Locks;

namespace Soenneker.Quark;

/// <inheritdoc cref="ICollapseCoordinator"/>
public sealed class CollapseCoordinator : ICollapseCoordinator
{
    private readonly AsyncLock _lock = new();
    private readonly HashSet<Collapse> _collapses = [];

    public async ValueTask Register(Collapse collapse)
    {
        if (collapse == null)
            return;

        using (await _lock.Lock())
        {
            _collapses.Add(collapse);
        }
    }

    public async ValueTask Unregister(Collapse collapse)
    {
        if (collapse == null)
            return;

        using (await _lock.Lock())
        {
            _collapses.Remove(collapse);
        }
    }

    public async ValueTask ToggleTargets(string? targetExpression)
    {
        if (targetExpression.IsNullOrWhiteSpace())
            return;

        Collapse[] snapshot;
        using (await _lock.Lock())
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
