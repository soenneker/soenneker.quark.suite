using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed class DropdownOverlayTests : BunitContext
{
    private readonly SpyOverlayInterop _overlayInterop = new();

    public DropdownOverlayTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

        Services.AddBradixSuiteAsScoped();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddSingleton<IOverlayInterop>(_overlayInterop);
    }

    [Test]
    public void Closing_overlay_releases_its_original_lock_after_parameter_change()
    {
        var cut = Render<OverlayLifecycleProbe>(p => p.Add(c => c.Visible, true));
        cut.Render(p => p.Add(c => c.LockScroll, false).Add(c => c.Visible, false));
        _overlayInterop.Deactivations.Should().Be(1);
        _overlayInterop.LastUnlockScroll.Should().BeTrue();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Pending_overlay_activation_is_cleaned_up_after_hide_or_disposal(bool dispose)
    {
        var pending = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        _overlayInterop.PendingActivation = pending.Task;
        var cut = Render<OverlayLifecycleProbe>(p => p.Add(c => c.Visible, true));
        _overlayInterop.ScrollLockActivations.Should().Be(1);

        if (dispose)
            await cut.InvokeAsync(async () => await cut.Instance.DisposeAsync());
        else
            cut.Render(p => p.Add(c => c.Visible, false));

        pending.SetResult();
        cut.WaitForAssertion(() => _overlayInterop.Deactivations.Should().Be(1));
        _overlayInterop.LastUnlockScroll.Should().BeTrue();
    }

    [Test]
    public void Visible_dropdown_does_not_lock_document_scroll()
    {
        Render<Dropdown>(parameters => parameters
            .Add(component => component.Visible, true)
            .Add(component => component.RenderWrapper, true));

        _overlayInterop.ScrollLockActivations.Should().Be(0);
    }

    [Test]
    public void Dropdown_can_still_opt_in_to_scroll_lock()
    {
        Render<Dropdown>(parameters => parameters
            .Add(component => component.Visible, true)
            .Add(component => component.LockScroll, true)
            .Add(component => component.RenderWrapper, true));

        _overlayInterop.ScrollLockActivations.Should().Be(1);
    }

    [Test]
    public void Visible_dialog_does_not_use_quark_overlay_scroll_lock()
    {
        Render<Dialog>(parameters => parameters
            .Add(component => component.Visible, true));

        _overlayInterop.ScrollLockActivations.Should().Be(0);
    }

    [Test]
    public void Visible_command_dialog_does_not_use_quark_overlay_scroll_lock()
    {
        Render<CommandDialog>(parameters => parameters
            .Add(component => component.Visible, true));

        _overlayInterop.ScrollLockActivations.Should().Be(0);
    }

    private sealed class SpyOverlayInterop : IOverlayInterop
    {
        public int ScrollLockActivations { get; private set; }
        public int Deactivations { get; private set; }
        public bool LastUnlockScroll { get; private set; }
        public Task? PendingActivation { get; set; }

        public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Activate(string overlayId, ElementReference container, bool trapFocus = true, bool lockScroll = true, string? initialFocusSelector = null,
            CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask ActivateScrollLock(string overlayId, CancellationToken cancellationToken = default)
        {
            ScrollLockActivations++;
            return PendingActivation is null ? ValueTask.CompletedTask : new ValueTask(PendingActivation);
        }

        public ValueTask Deactivate(string overlayId, bool unlockScroll = true, CancellationToken cancellationToken = default)
        {
            Deactivations++;
            LastUnlockScroll = unlockScroll;
            return ValueTask.CompletedTask;
        }

        public ValueTask ReleaseScrollLocks(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

public sealed class OverlayLifecycleProbe : OverlayElement
{
    protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.CloseElement();
    }
}
