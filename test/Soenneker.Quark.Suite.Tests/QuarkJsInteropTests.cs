using System;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.JSInterop;
using Soenneker.Blazor.Extensions.IJSObjectReferences;

namespace Soenneker.Quark.Suite.Tests;

public sealed class QuarkJsInteropTests
{
    [Test]
    public async Task Calls_preserve_arguments_tokens_and_results()
    {
        using var source = new CancellationTokenSource();
        var module = new InteropModuleStub { Result = 42 };
        object?[] args = ["element", false, null];
        (await module.InvokeWithContextAsync<int>("measure", source.Token, args)).Should().Be(42);
        module.Identifier.Should().Be("measure");
        module.Token.Should().Be(source.Token);
        module.Arguments.Should().BeSameAs(args);

        module.Result = null;
        await module.InvokeVoidWithContextAsync("update", source.Token, args);
        module.Token.Should().Be(source.Token);
        module.Arguments.Should().BeSameAs(args);
        await module.InvokeVoidWithContextAsync("update", args);
        module.Token.Should().Be(CancellationToken.None);
        module.Arguments.Should().BeSameAs(args);
        (await module.InvokeWithContextAsync<string?>("selection", args)).Should().BeNull();
        module.Arguments.Should().BeSameAs(args);
    }

    [Test]
    public async Task Script_failures_include_operation_and_retain_original_exception()
    {
        var failure = new JSException("Script failed");
        var module = new InteropModuleStub { Failure = failure };
        Func<Task> query = async () => await module.InvokeWithContextAsync<int>("measure");
        var queryError = await query.Should().ThrowAsync<JSException>().WithMessage("*'measure'*Script failed*");
        queryError.Which.InnerException.Should().BeSameAs(failure);
        Func<Task> command = async () => await module.InvokeVoidWithContextAsync("update");
        var commandError = await command.Should().ThrowAsync<JSException>().WithMessage("*'update'*Script failed*");
        commandError.Which.InnerException.Should().BeSameAs(failure);
    }

    [Test]
    public async Task Lifecycle_and_non_script_failures_reach_callers_unchanged()
    {
        using var source = new CancellationTokenSource();
        source.Cancel();
        Exception[] failures = [new JSDisconnectedException("Disconnected"), new OperationCanceledException(source.Token),
            new ObjectDisposedException("module"), new InvalidOperationException("Prerendering")];
        foreach (var failure in failures)
        {
            var module = new InteropModuleStub { Failure = failure };
            Func<Task> query = async () => await module.InvokeWithContextAsync<int>("measure", source.Token);
            var queryError = await query.Should().ThrowAsync<Exception>();
            queryError.Which.Should().BeSameAs(failure);
            Func<Task> command = async () => await module.InvokeVoidWithContextAsync("update", source.Token);
            var commandError = await command.Should().ThrowAsync<Exception>();
            commandError.Which.Should().BeSameAs(failure);
        }
    }
}
