using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Soenneker.Blazor.MockJsRuntime.Registrars;
using Soenneker.Bradix;
using Soenneker.Quark.Gen.Lucide.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests : BunitContext
{
    public RenderedShadcnParityTests()
    {
        var module = JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix.js");
        module.SetupVoid("mountPortal", _ => true).SetVoidResult();
        module.SetupVoid("unmountPortal", _ => true).SetVoidResult();
        module.SetupVoid("registerAvatarImageLoadingStatus", _ => true).SetVoidResult();
        module.SetupVoid("unregisterAvatarImageLoadingStatus", _ => true).SetVoidResult();
        module.SetupVoid("registerDelegatedInteraction", _ => true).SetVoidResult();
        module.SetupVoid("unregisterDelegatedInteraction", _ => true).SetVoidResult();
        module.SetupVoid("registerLabelTextSelectionGuard", _ => true).SetVoidResult();
        module.SetupVoid("unregisterLabelTextSelectionGuard", _ => true).SetVoidResult();
        module.SetupVoid("registerPresence", _ => true).SetVoidResult();
        module.SetupVoid("unregisterPresence", _ => true).SetVoidResult();
        module.SetupVoid("registerRovingFocusNavigationKeys", _ => true).SetVoidResult();
        module.SetupVoid("unregisterRovingFocusNavigationKeys", _ => true).SetVoidResult();
        module.SetupVoid("observeCollapsibleContent", _ => true).SetVoidResult();
        module.SetupVoid("unobserveCollapsibleContent", _ => true).SetVoidResult();
        module.SetupVoid("registerDismissableLayerBranch", _ => true).SetVoidResult();
        module.SetupVoid("unregisterDismissableLayerBranch", _ => true).SetVoidResult();
        module.SetupVoid("registerSelectBubbleInput", _ => true).SetVoidResult();
        module.SetupVoid("unregisterSelectBubbleInput", _ => true).SetVoidResult();
        module.SetupVoid("syncSelectBubbleInputValue", _ => true).SetVoidResult();
        module.SetupVoid("registerMenubarDocumentDismiss", _ => true).SetVoidResult();
        module.SetupVoid("unregisterMenubarDocumentDismiss", _ => true).SetVoidResult();
        module.SetupVoid("registerNavigationMenuTriggerInteraction", _ => true).SetVoidResult();
        module.SetupVoid("unregisterNavigationMenuTriggerInteraction", _ => true).SetVoidResult();
        module.SetupVoid("registerRadioGroupItemKeys", _ => true).SetVoidResult();
        module.SetupVoid("unregisterRadioGroupItemKeys", _ => true).SetVoidResult();
        module.SetupVoid("registerSliderPointerBridge", _ => true).SetVoidResult();
        module.SetupVoid("unregisterSliderPointerBridge", _ => true).SetVoidResult();
        module.SetupVoid("syncInputValue", _ => true).SetVoidResult();
        module.SetupVoid("registerAssociatedFormReset", _ => true).SetVoidResult();
        module.SetupVoid("unregisterAssociatedFormReset", _ => true).SetVoidResult();
        module.SetupVoid("registerOneTimePasswordInput", _ => true).SetVoidResult();
        module.SetupVoid("unregisterOneTimePasswordInput", _ => true).SetVoidResult();
        module.SetupVoid("registerCheckboxRoot", _ => true).SetVoidResult();
        module.SetupVoid("unregisterCheckboxRoot", _ => true).SetVoidResult();
        module.SetupVoid("registerScrollAreaViewport", _ => true).SetVoidResult();
        module.SetupVoid("unregisterScrollAreaViewport", _ => true).SetVoidResult();
        module.SetupVoid("registerScrollAreaRoot", _ => true).SetVoidResult();
        module.SetupVoid("unregisterScrollAreaRoot", _ => true).SetVoidResult();
        module.SetupVoid("registerDismissableLayer", _ => true).SetVoidResult();
        module.SetupVoid("unregisterDismissableLayer", _ => true).SetVoidResult();
        module.SetupVoid("updateFocusScope", _ => true).SetVoidResult();
        module.SetupVoid("registerFocusScope", _ => true).SetVoidResult();
        module.SetupVoid("unregisterFocusScope", _ => true).SetVoidResult();
        module.SetupVoid("registerFocusGuards", _ => true).SetVoidResult();
        module.SetupVoid("unregisterFocusGuards", _ => true).SetVoidResult();
        module.SetupVoid("registerHideOthers", _ => true).SetVoidResult();
        module.SetupVoid("unregisterHideOthers", _ => true).SetVoidResult();
        module.SetupVoid("registerRemoveScroll", _ => true).SetVoidResult();
        module.SetupVoid("unregisterRemoveScroll", _ => true).SetVoidResult();
        module.SetupVoid("registerTooltipTrigger", _ => true).SetVoidResult();
        module.SetupVoid("unregisterTooltipTrigger", _ => true).SetVoidResult();
        module.SetupVoid("registerTooltipContent", _ => true).SetVoidResult();
        module.SetupVoid("unregisterTooltipContent", _ => true).SetVoidResult();
        module.SetupVoid("disableHoverCardContentTabNavigation", _ => true).SetVoidResult();
        module.SetupVoid("registerHoverCardSelectionContainment", _ => true).SetVoidResult();
        module.SetupVoid("unregisterHoverCardSelectionContainment", _ => true).SetVoidResult();
        module.SetupVoid("registerPopperContent", _ => true).SetVoidResult();
        module.SetupVoid("unregisterPopperContent", _ => true).SetVoidResult();
        module.SetupVoid("updatePopperContent", _ => true).SetVoidResult();
        module.Setup<string>("getTextContent", _ => true).SetResult(string.Empty);
        module.Setup<bool>("isFormControl", _ => true).SetResult(false);
        module.Setup<bool>("isDirectionRtl", _ => true).SetResult(false);

        JSInterop.SetupVoid("registerDismissableLayerBranch", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterDismissableLayerBranch", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerSelectBubbleInput", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterSelectBubbleInput", _ => true).SetVoidResult();
        JSInterop.SetupVoid("syncSelectBubbleInputValue", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerMenubarDocumentDismiss", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterMenubarDocumentDismiss", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerNavigationMenuTriggerInteraction", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterNavigationMenuTriggerInteraction", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerRadioGroupItemKeys", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterRadioGroupItemKeys", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerSliderPointerBridge", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterSliderPointerBridge", _ => true).SetVoidResult();
        JSInterop.SetupVoid("syncInputValue", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerAssociatedFormReset", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterAssociatedFormReset", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerOneTimePasswordInput", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterOneTimePasswordInput", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerCheckboxRoot", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterCheckboxRoot", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerScrollAreaViewport", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterScrollAreaViewport", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerScrollAreaRoot", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterScrollAreaRoot", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerDismissableLayer", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterDismissableLayer", _ => true).SetVoidResult();
        JSInterop.SetupVoid("updateFocusScope", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerFocusScope", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterFocusScope", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerFocusGuards", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterFocusGuards", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerHideOthers", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterHideOthers", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerRemoveScroll", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterRemoveScroll", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerTooltipTrigger", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterTooltipTrigger", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerTooltipContent", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterTooltipContent", _ => true).SetVoidResult();
        JSInterop.SetupVoid("disableHoverCardContentTabNavigation", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerHoverCardSelectionContainment", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterHoverCardSelectionContainment", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerPopperContent", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterPopperContent", _ => true).SetVoidResult();
        JSInterop.SetupVoid("updatePopperContent", _ => true).SetVoidResult();
        JSInterop.Setup<string>("getTextContent", _ => true).SetResult(string.Empty);
        JSInterop.Setup<bool>("isFormControl", _ => true).SetResult(false);
        JSInterop.Setup<bool>("isDirectionRtl", _ => true).SetResult(false);

        Services.AddMockJsRuntimeAsScoped();
        Services.AddBradixSuiteAsScoped();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddScoped<ILucideIconSvgProvider, FakeLucideIconSvgProvider>();
        Services.AddScoped<ICollapseCoordinator, CollapseCoordinator>();
        Services.AddScoped<IScoreInterop, FakeScoreInterop>();
        Services.AddScoped<ITreeViewInterop, FakeTreeViewInterop>();
        Services.AddScoped<IResizableInterop, FakeResizableInterop>();
        Services.AddScoped<IOverlayInterop, FakeOverlayInterop>();
        Services.AddScoped<IThemeInterop, FakeThemeInterop>();
        Services.AddScoped<global::Soenneker.Quark.Suite.Demo.Services.ThemeService>();
        Services.AddScoped<ICodeEditorInterop, FakeCodeEditorInterop>();
        Services.AddScoped<ITablesInterop, FakeTablesInterop>();
        Services.AddScoped<ISonnerService, SonnerService>();
        Services.AddScoped<ISonnerInterop, FakeSonnerInterop>();
    }

    private sealed class FakeLucideIconSvgProvider : ILucideIconSvgProvider
    {
        public string? GetSvg(string iconName)
        {
            return "<svg viewBox=\"0 0 24 24\" aria-hidden=\"true\"></svg>";
        }
    }

    private sealed class FakeScoreInterop : IScoreInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeTreeViewInterop : ITreeViewInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeThemeInterop : IThemeInterop
    {
        public ValueTask<bool> Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(false);

        public ValueTask<bool> Toggle(System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(true);

        public ValueTask<bool> GetIsDarkAsync(System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(false);

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeOverlayInterop : IOverlayInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Activate(string overlayId, ElementReference container, bool trapFocus = true, bool lockScroll = true, string? initialFocusSelector = null,
            System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Deactivate(string overlayId, bool unlockScroll = true, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeResizableInterop : IResizableInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask RegisterHandle(ElementReference handle, ElementReference group, string orientation,
            DotNetObjectReference<ResizablePanelGroup> callbackReference, int handleIndex, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask UnregisterHandle(ElementReference handle, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask StartDrag(ElementReference group, long pointerId, double clientX, double clientY, string orientation,
            DotNetObjectReference<ResizablePanelGroup> callbackReference, int handleIndex, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask StopDrag(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeCodeEditorInterop : ICodeEditorInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask CreateEditor(ElementReference container, string optionsJson, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask SetValue(ElementReference container, string value, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask<string?> GetValue(ElementReference container, System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult<string?>(string.Empty);

        public ValueTask SetLanguage(ElementReference container, string language, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask SetTheme(string theme, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeEditor(ElementReference container, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Layout(ElementReference container, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask UpdateContentHeight(ElementReference container, int? minLines = null, int? maxLines = null, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask AddContentChangeListener(ElementReference container, int? minLines = null, int? maxLines = null, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask RegisterThemeChangedCallback<T>(DotNetObjectReference<T> dotNetRef, System.Threading.CancellationToken cancellationToken = default) where T : class => ValueTask.CompletedTask;

        public ValueTask UnregisterThemeChangedCallback<T>(DotNetObjectReference<T> dotNetRef) where T : class => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeTablesInterop : ITablesInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeSonnerInterop : ISonnerInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask<Dictionary<string, double>> MeasureToastHeights(ElementReference section, System.Threading.CancellationToken cancellationToken = default)
            => ValueTask.FromResult(new Dictionary<string, double>());

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

}

