using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Soenneker.Extensions.String;
using System.Threading;

namespace Soenneker.Quark;

/// <inheritdoc cref="ICollapseCoordinator"/>
public sealed class CollapseCoordinator : ICollapseCoordinator
{
    private readonly Lock _lock = new();
    private Collapse[]? _snapshot;
    private readonly HashSet<Collapse> _collapses = [];

    public ValueTask Register(Collapse collapse)
    {
        if (collapse is null) return ValueTask.CompletedTask;
        lock (_lock)
        {
            if (_collapses.Add(collapse)) _snapshot = null;
        }
        return ValueTask.CompletedTask;
    }

    public ValueTask Unregister(Collapse collapse)
    {
        if (collapse is null) return ValueTask.CompletedTask;
        lock (_lock)
        {
            if (_collapses.Remove(collapse)) _snapshot = null;
        }
        return ValueTask.CompletedTask;
    }

    public async ValueTask ToggleTargets(string? targetExpression)
    {
        if (targetExpression.IsNullOrWhiteSpace())
            return;

        Collapse[] snapshot;
        lock (_lock)
        {
            snapshot = _snapshot ??= [.. _collapses];
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

            await ToggleToken(targetExpression.AsMemory(tokenStart, i - tokenStart), snapshot);
            tokenStart = -1;
        }
    }

    private static async ValueTask ToggleToken(ReadOnlyMemory<char> token, Collapse[] snapshot)
    {
        if (token.Length == 0)
            return;

        if (token.Span[0] == '.')
        {
            var className = token[1..];
            if (className.Length == 0)
                return;

            foreach (var collapse in snapshot)
            {
                if (collapse.HasCssClass(className.Span))
                    await collapse.Toggle();
            }

            return;
        }

        var id = token.Span[0] == '#' ? token[1..] : token;
        if (id.Length == 0)
            return;

        foreach (var collapse in snapshot)
        {
            if (!collapse.Id.AsSpan().SequenceEqual(id.Span))
                continue;

            await collapse.Toggle();
            break;
        }
    }
}
