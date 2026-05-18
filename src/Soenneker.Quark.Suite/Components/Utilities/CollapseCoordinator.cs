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

        var tokenStart = -1;

        for (var i = 0; i <= targetExpression.Length; i++)
        {
            if (i < targetExpression.Length && targetExpression[i] != ',' && !char.IsWhiteSpace(targetExpression[i]))
            {
                if (tokenStart < 0)
                    tokenStart = i;

                continue;
            }

            if (tokenStart < 0)
                continue;

            await ToggleToken(targetExpression[tokenStart..i], snapshot);
            tokenStart = -1;
        }
    }

    private static async ValueTask ToggleToken(string token, Collapse[] snapshot)
    {
        if (token.Length == 0)
            return;

        if (token[0] == '.')
        {
            var className = token[1..];
            if (className.Length == 0)
                return;

            foreach (var collapse in snapshot)
            {
                if (collapse.HasCssClass(className))
                    await collapse.Toggle();
            }

            return;
        }

        var id = token[0] == '#' ? token[1..] : token;
        if (id.Length == 0)
            return;

        foreach (var collapse in snapshot)
        {
            if (!string.Equals(collapse.Id, id, StringComparison.Ordinal))
                continue;

            await collapse.Toggle();
            break;
        }
    }
}
