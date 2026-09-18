using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.LocalStorage.Abstract;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class FakeSidebarLocalStorageUtil : ILocalStorageUtil
{
    public Dictionary<string, string> Values { get; } = new();
    public int Reads { get; private set; }
    public int Writes { get; private set; }
    public bool Unavailable { get; set; }
    public TaskCompletionSource<string?>? PendingRead { get; set; }
    public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask<string?> Get(string key, CancellationToken cancellationToken = default)
    {
        Reads++;
        if (Unavailable) throw new JSException("Storage is blocked");
        if (PendingRead is { } pending) return new ValueTask<string?>(pending.Task);
        return ValueTask.FromResult(Values.GetValueOrDefault(key));
    }
    public ValueTask Set(string key, string value, CancellationToken cancellationToken = default)
    {
        Writes++;
        if (Unavailable) throw new JSException("Storage is blocked");
        Values[key] = value;
        return ValueTask.CompletedTask;
    }
    public ValueTask<T?> Get<T>(string key, CancellationToken cancellationToken = default) => throw new NotSupportedException();
    public ValueTask Set<T>(string key, T value, CancellationToken cancellationToken = default) => throw new NotSupportedException();
    public ValueTask Remove(string key, CancellationToken cancellationToken = default) => throw new NotSupportedException();
    public ValueTask Clear(CancellationToken cancellationToken = default) => throw new NotSupportedException();
    public ValueTask<bool> ContainsKey(string key, CancellationToken cancellationToken = default) => throw new NotSupportedException();
    public ValueTask<IReadOnlyList<string>> GetKeys(CancellationToken cancellationToken = default) => throw new NotSupportedException();
    public ValueTask<int> GetLength(CancellationToken cancellationToken = default) => throw new NotSupportedException();
}
