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

    private sealed class SpyOverlayInterop : IOverlayInterop
    {
        public int ScrollLockActivations { get; private set; }

        public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Activate(string overlayId, ElementReference container, bool trapFocus = true, bool lockScroll = true, string? initialFocusSelector = null,
            CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask ActivateScrollLock(string overlayId, CancellationToken cancellationToken = default)
        {
            ScrollLockActivations++;
            return ValueTask.CompletedTask;
        }

        public ValueTask Deactivate(string overlayId, bool unlockScroll = true, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask ReleaseScrollLocks(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
