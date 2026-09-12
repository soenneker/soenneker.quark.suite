using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using QuarkThread = Soenneker.Quark.Thread;

namespace Soenneker.Quark.Suite.Tests;

public sealed class LeafLifecycleTests : BunitContext
{
    public LeafLifecycleTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Test]
    public void Animation_durations_use_css_decimal_points_in_decimal_comma_cultures()
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
            var marquee = Render<Marquee>(p => p.Add(c => c.ScrollDuration, 1.25));
            marquee.Find("[data-slot='marquee']").GetAttribute("style").Should().Contain("--marquee-duration:1.25s");
            var shimmer = Render<TextShimmer>(p => p.Add(c => c.ShimmerDuration, 1.25));
            shimmer.Find("[data-slot='text-shimmer']").GetAttribute("style").Should().Contain("--text-shimmer-duration:1.25s");
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [Test]
    public async Task Textarea_registration_finishing_after_disposal_is_removed()
    {
        var interop = new PendingPromptInterop();
        Services.AddSingleton<IPromptInputInterop>(interop);
        var cut = Render<PromptInputTextarea>();
        interop.Registered.Should().BeTrue();
        await cut.Instance.DisposeAsync();
        interop.Completion.SetResult();
        cut.WaitForAssertion(() => interop.Removals.Should().Be(1));
        await cut.Instance.DisposeAsync();
        interop.Removals.Should().Be(1);
    }

    [Test]
    public async Task Thread_initialization_finishing_after_disposal_is_destroyed_again()
    {
        var interop = new PendingThreadInterop();
        Services.AddSingleton<IThreadsInterop>(interop);
        var cut = Render<QuarkThread>();
        interop.Reference.Should().NotBeNull();
        await cut.Instance.DisposeAsync();
        interop.Removals.Should().Be(1);
        interop.Completion.SetResult();
        cut.WaitForAssertion(() => interop.Removals.Should().Be(2));
        Action access = () => _ = interop.Reference!.Value;
        access.Should().Throw<ObjectDisposedException>();
        await cut.Instance.DisposeAsync();
        interop.Removals.Should().Be(2);
    }

    private sealed class PendingPromptInterop : IPromptInputInterop
    {
        public TaskCompletionSource Completion { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public bool Registered { get; private set; }
        public int Removals { get; private set; }
        public ValueTask RegisterTextarea(ElementReference textarea, CancellationToken cancellationToken = default)
        {
            Registered = true;
            return new(Completion.Task);
        }
        public ValueTask UnregisterTextarea(ElementReference textarea, CancellationToken cancellationToken = default)
        {
            Removals++;
            return ValueTask.CompletedTask;
        }
        public ValueTask OpenFileDialogById(string inputId, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
        public ValueTask RegisterAttachmentsById(string inputId, DotNetObjectReference<PromptInputActionAddAttachments> callbackReference, bool globalDrop, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
        public ValueTask UnregisterAttachmentsById(string inputId, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class PendingThreadInterop : IThreadsInterop
    {
        public TaskCompletionSource Completion { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public DotNetObjectReference<QuarkThread>? Reference { get; private set; }
        public int Removals { get; private set; }
        public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
        public ValueTask InitializeThread(ElementReference element, DotNetObjectReference<QuarkThread> callbackReference, string initial, string resizeBehavior, bool stickToBottom, CancellationToken cancellationToken = default)
        {
            Reference = callbackReference;
            return new(Completion.Task);
        }
        public ValueTask ScrollToBottom(ElementReference element, string behavior, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
        public ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default)
        {
            Removals++;
            return ValueTask.CompletedTask;
        }
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
