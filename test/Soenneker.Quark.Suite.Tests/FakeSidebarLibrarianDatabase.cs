using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.JSInterop;
using Soenneker.Dtos.IdValuePair;
using Soenneker.Librarian.Abstractions;
using Soenneker.Librarian.Abstractions.Transactions;
using Soenneker.Librarian.Browser;
using Soenneker.Librarian.Core;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class FakeSidebarLibrarianDatabase : IBrowserLibrarianDatabase
{
    public Dictionary<string, string> Values { get; } = new(StringComparer.Ordinal);
    public int Reads { get; private set; }
    public int Writes { get; private set; }
    public bool Unavailable { get; set; }
    public bool Conflict { get; set; }
    public TaskCompletionSource<string?>? PendingRead { get; set; }
    private readonly List<ILibrarianContainer> _containers = [];

    public async ValueTask<ILibrarianContainer> GetContainer(string containerName, CancellationToken cancellationToken = default)
    {
        Reads++;
        if (Unavailable) throw new JSException("Storage is blocked");
        string? saved = PendingRead is { } pending ? await pending.Task : Values.GetValueOrDefault(containerName);
        var container = new LibrarianContainer(containerName, this, NullLogger.Instance,
            saved is null ? [] : [new IdValuePair { Id = "width", Value = saved }]);
        _containers.Add(container);
        return container;
    }

    public ValueTask<bool> Execute(LibrarianBatch batch, CancellationToken cancellationToken = default)
    {
        Writes++;
        if (Unavailable) throw new JSException("Storage is blocked");
        if (Conflict) throw new LibrarianConcurrencyException("Storage changed.");
        foreach (LibrarianWrite write in batch.Writes)
        {
            if (write.Id != "width") throw new InvalidOperationException("Expected a sidebar width document.");
            Values[write.Container] = write.Value!;
        }
        return ValueTask.FromResult(true);
    }

    public ValueTask MarkDirty(string containerName, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask Save(CancellationToken cancellationToken = default) => throw new NotSupportedException();
    public ValueTask<bool> UnloadContainer(string containerName, CancellationToken cancellationToken = default) => throw new NotSupportedException();

    public ValueTask DiscardAsync()
    {
        foreach (ILibrarianContainer container in _containers) container.Dispose();
        _containers.Clear();
        return ValueTask.CompletedTask;
    }

    public ValueTask DisposeAsync() => DiscardAsync();
}
