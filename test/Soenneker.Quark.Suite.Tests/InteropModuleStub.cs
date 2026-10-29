using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class InteropModuleStub : IJSObjectReference
{
    public Exception? Failure { get; set; }
    public object? Result { get; set; }
    public string? Identifier { get; private set; }
    public CancellationToken Token { get; private set; }
    public object?[]? Arguments { get; private set; }

    public async ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, object?[]? args)
    {
        Identifier = identifier;
        Token = CancellationToken.None;
        Arguments = args;
        // Model an asynchronous circuit response without calling browser interop.
        await Task.Yield();
        if (Failure is not null)
            throw Failure;
        return Result is null ? default! : (TValue)Result;
    }

    public async ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
    {
        Identifier = identifier;
        Token = cancellationToken;
        Arguments = args;
        // Model an asynchronous circuit response without calling browser interop.
        await Task.Yield();
        if (Failure is not null)
            throw Failure;
        return Result is null ? default! : (TValue)Result;
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
