using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Soenneker.Blazor.C15t.Abstract;
using Soenneker.Blazor.C15t.Models;
using Soenneker.Blazor.Utils.Clipboard.Abstract;
using Soenneker.Blazor.Utils.Clipboard.Dtos;
using Soenneker.Blazor.Utils.Clipboard.Enums;
using Soenneker.Blazor.MockJsRuntime.Registrars;
using Soenneker.Bradix;
using Soenneker.Quark.Gen.Lucide.Abstractions;
using Soenneker.Quark.Gen.SimpleIcons.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests : BunitContext
{
    public RenderedShadcnParityTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

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
        module.SetupVoid("registerSelectViewport", _ => true).SetVoidResult();
        module.SetupVoid("unregisterSelectViewport", _ => true).SetVoidResult();
        module.SetupVoid("scrollSelectViewportByItem", _ => true).SetVoidResult();
        module.SetupVoid("registerSelectContentPointerTracker", _ => true).SetVoidResult();
        module.SetupVoid("unregisterSelectContentPointerTracker", _ => true).SetVoidResult();
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
        module.SetupVoid("registerScrollAreaScrollbar", _ => true).SetVoidResult();
        module.SetupVoid("unregisterScrollAreaScrollbar", _ => true).SetVoidResult();
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
        module.SetupVoid("registerPopperContentBySelector", _ => true).SetVoidResult();
        module.SetupVoid("unregisterPopperContent", _ => true).SetVoidResult();
        module.SetupVoid("updatePopperContent", _ => true).SetVoidResult();
        module.Setup<string>("getTextContent", _ => true).SetResult(string.Empty);
        module.Setup<bool>("isFormControl", _ => true).SetResult(false);
        module.Setup<bool>("isDirectionRtl", _ => true).SetResult(false);
        module.Setup<bool>("isKeyboardInteractionMode", _ => true).SetResult(false);

        var formsModule = JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix/forms.js");
        formsModule.Setup<bool>("isFormControl", _ => true).SetResult(false);
        formsModule.SetupVoid("registerRadioGroupItemKeys", _ => true).SetVoidResult();
        formsModule.SetupVoid("unregisterRadioGroupItemKeys", _ => true).SetVoidResult();
        formsModule.SetupVoid("syncInputValue", _ => true).SetVoidResult();
        formsModule.SetupVoid("registerAssociatedFormReset", _ => true).SetVoidResult();
        formsModule.SetupVoid("unregisterAssociatedFormReset", _ => true).SetVoidResult();
        formsModule.SetupVoid("registerSelectBubbleInput", _ => true).SetVoidResult();
        formsModule.SetupVoid("unregisterSelectBubbleInput", _ => true).SetVoidResult();
        formsModule.SetupVoid("syncSelectBubbleInputValue", _ => true).SetVoidResult();

        var scrollAreaModule = JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix/scrollArea.js");
        scrollAreaModule.SetupVoid("registerScrollAreaViewport", _ => true).SetVoidResult();
        scrollAreaModule.SetupVoid("unregisterScrollAreaViewport", _ => true).SetVoidResult();
        scrollAreaModule.SetupVoid("registerScrollAreaRoot", _ => true).SetVoidResult();
        scrollAreaModule.SetupVoid("unregisterScrollAreaRoot", _ => true).SetVoidResult();
        scrollAreaModule.SetupVoid("registerScrollAreaScrollbar", _ => true).SetVoidResult();
        scrollAreaModule.SetupVoid("unregisterScrollAreaScrollbar", _ => true).SetVoidResult();

        var dismissableLayerModule = JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix/dismissableLayer.js");
        dismissableLayerModule.SetupVoid("registerDismissableLayerBranch", _ => true).SetVoidResult();
        dismissableLayerModule.SetupVoid("unregisterDismissableLayerBranch", _ => true).SetVoidResult();
        dismissableLayerModule.SetupVoid("registerDismissableLayer", _ => true).SetVoidResult();
        dismissableLayerModule.SetupVoid("unregisterDismissableLayer", _ => true).SetVoidResult();

        var rovingFocusModule = JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix/rovingFocus.js");
        rovingFocusModule.SetupVoid("registerRovingFocusNavigationKeys", _ => true).SetVoidResult();
        rovingFocusModule.SetupVoid("unregisterRovingFocusNavigationKeys", _ => true).SetVoidResult();

        var portalModule = JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix/portal.js");
        portalModule.SetupVoid("mountPortal", _ => true).SetVoidResult();
        portalModule.SetupVoid("unmountPortal", _ => true).SetVoidResult();

        var delegatedInteractionModule = JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix/delegatedInteraction.js");
        delegatedInteractionModule.SetupVoid("registerDelegatedInteraction", _ => true).SetVoidResult();
        delegatedInteractionModule.SetupVoid("unregisterDelegatedInteraction", _ => true).SetVoidResult();

        var selectModule = JSInterop.SetupModule("./_content/Soenneker.Bradix.Suite/js/bradix/select.js");
        selectModule.SetupVoid("registerSelectBubbleInput", _ => true).SetVoidResult();
        selectModule.SetupVoid("unregisterSelectBubbleInput", _ => true).SetVoidResult();
        selectModule.SetupVoid("syncSelectBubbleInputValue", _ => true).SetVoidResult();
        selectModule.SetupVoid("registerSelectViewport", _ => true).SetVoidResult();
        selectModule.SetupVoid("unregisterSelectViewport", _ => true).SetVoidResult();
        selectModule.SetupVoid("scrollSelectViewportByItem", _ => true).SetVoidResult();
        selectModule.SetupVoid("registerSelectContentPointerTracker", _ => true).SetVoidResult();
        selectModule.SetupVoid("unregisterSelectContentPointerTracker", _ => true).SetVoidResult();

        var resourceLoaderModule = JSInterop.SetupModule("./_content/Soenneker.Blazor.Utils.ResourceLoader/js/resourceloader.js");
        resourceLoaderModule.SetupVoid("loadScript", _ => true).SetVoidResult();
        resourceLoaderModule.SetupVoid("loadStyle", _ => true).SetVoidResult();

        var jsVariableModule = JSInterop.SetupModule("./_content/Soenneker.Blazor.Utils.JsVariable/js/jsvariableinterop.js");
        jsVariableModule.Setup<bool>("isVariableAvailable", _ => true).SetResult(true);
        jsVariableModule.SetupVoid("waitForVariable", _ => true).SetVoidResult();
        jsVariableModule.SetupVoid("cancelWaitForVariable", _ => true).SetVoidResult();

        var sidebarModule = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/sidebarinterop.js");
        sidebarModule.Setup<System.Text.Json.JsonElement>("getSidebarState", _ => true).SetResult(default);
        sidebarModule.SetupVoid("initializeSidebar", _ => true).SetVoidResult();
        sidebarModule.SetupVoid("saveSidebarState", _ => true).SetVoidResult();
        sidebarModule.SetupVoid("cleanup", _ => true).SetVoidResult();

        var promptInputModule = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/promptinputinterop.js");
        promptInputModule.SetupVoid("registerTextarea", _ => true).SetVoidResult();
        promptInputModule.SetupVoid("unregisterTextarea", _ => true).SetVoidResult();
        promptInputModule.SetupVoid("registerAttachments", _ => true).SetVoidResult();
        promptInputModule.SetupVoid("unregisterAttachments", _ => true).SetVoidResult();
        promptInputModule.SetupVoid("openFileDialog", _ => true).SetVoidResult();
        promptInputModule.SetupVoid("registerAttachmentsById", _ => true).SetVoidResult();
        promptInputModule.SetupVoid("unregisterAttachmentsById", _ => true).SetVoidResult();
        promptInputModule.SetupVoid("openFileDialogById", _ => true).SetVoidResult();

        var threadModule = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/threadinterop.js");
        threadModule.SetupVoid("initialize", _ => true).SetVoidResult();
        threadModule.SetupVoid("scrollToBottom", _ => true).SetVoidResult();
        threadModule.SetupVoid("dispose", _ => true).SetVoidResult();

        JSInterop.SetupVoid("registerDismissableLayerBranch", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterDismissableLayerBranch", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerSelectBubbleInput", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterSelectBubbleInput", _ => true).SetVoidResult();
        JSInterop.SetupVoid("syncSelectBubbleInputValue", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerSelectViewport", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterSelectViewport", _ => true).SetVoidResult();
        JSInterop.SetupVoid("scrollSelectViewportByItem", _ => true).SetVoidResult();
        JSInterop.SetupVoid("registerSelectContentPointerTracker", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterSelectContentPointerTracker", _ => true).SetVoidResult();
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
        JSInterop.SetupVoid("registerScrollAreaScrollbar", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterScrollAreaScrollbar", _ => true).SetVoidResult();
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
        JSInterop.SetupVoid("registerPopperContentBySelector", _ => true).SetVoidResult();
        JSInterop.SetupVoid("unregisterPopperContent", _ => true).SetVoidResult();
        JSInterop.SetupVoid("updatePopperContent", _ => true).SetVoidResult();
        JSInterop.SetupVoid("quarkDemo.scrollElementToTop", _ => true).SetVoidResult();
        JSInterop.Setup<string>("getTextContent", _ => true).SetResult(string.Empty);
        JSInterop.Setup<bool>("isFormControl", _ => true).SetResult(false);
        JSInterop.Setup<bool>("isDirectionRtl", _ => true).SetResult(false);
        JSInterop.Setup<bool>("isKeyboardInteractionMode", _ => true).SetResult(false);

        Services.AddMockJsRuntimeAsScoped();
        Services.AddBradixSuiteAsScoped();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddScoped<ILucideIconSvgProvider, FakeLucideIconSvgProvider>();
        Services.AddScoped<ISimpleIconsSvgProvider, FakeSimpleIconsSvgProvider>();
        Services.AddScoped<ICollapseCoordinator, CollapseCoordinator>();
        Services.AddScoped<IScoreInterop, FakeScoreInterop>();
        Services.AddScoped<IScrollspyInterop, FakeScrollspyInterop>();
        Services.AddScoped<IThreadsInterop, FakeThreadsInterop>();
        Services.AddScoped<ISortableInterop, FakeSortableInterop>();
        Services.AddScoped<ICarouselInterop, FakeCarouselInterop>();
        Services.AddScoped<IPromptInputInterop, FakePromptInputInterop>();
        Services.AddScoped<IResizableInterop, FakeResizableInterop>();
        Services.AddScoped<IOverlayInterop, FakeOverlayInterop>();
        Services.AddScoped<IThemeInterop, FakeThemeInterop>();
        Services.AddScoped<Demo.Services.ThemeService>();
        Services.AddScoped<ICodeEditorInterop, FakeCodeEditorInterop>();
        Services.AddScoped<ITablesInterop, FakeTablesInterop>();
        Services.AddScoped<IColorPickerInterop, FakeColorPickerInterop>();
        Services.AddScoped<IClipboardUtil, FakeClipboardUtil>();
        Services.AddScoped<ISonnerService, SonnerService>();
        Services.AddScoped<ISonnerInterop, FakeSonnerInterop>();
        Services.AddScoped<IC15tConsentService, FakeC15tConsentService>();
    }

    private sealed class FakeLucideIconSvgProvider : ILucideIconSvgProvider
    {
        public string? GetSvg(string iconName)
        {
            return "<svg viewBox=\"0 0 24 24\" aria-hidden=\"true\"></svg>";
        }
    }

    private sealed class FakeSimpleIconsSvgProvider : ISimpleIconsSvgProvider
    {
        public string? GetSvg(string iconName)
        {
            return "<svg role=\"img\" viewBox=\"0 0 24 24\"><title>GitHub</title><path d=\"M1 1h22v22H1z\" /></svg>";
        }
    }

    private sealed class FakeScoreInterop : IScoreInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeScrollspyInterop : IScrollspyInterop
    {
        public ValueTask Initialize(ElementReference element, object options, DotNetObjectReference<Scrollspy> callbackReference,
            System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Destroy(ElementReference element, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    }

    private sealed class FakeSortableInterop : ISortableInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask InitializeList(ElementReference element, bool disabled, bool sort, int animation, bool forceFallback, string itemSelector, string? handleSelector,
            string? filterSelector, string? group, bool notifyOnReorder, DotNetObjectReference<SortableList> callbackReference,
            System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Destroy(ElementReference element, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeThreadsInterop : IThreadsInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask InitializeThread(ElementReference element, DotNetObjectReference<Thread> callbackReference, string initial, string resizeBehavior,
            bool stickToBottom, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask ScrollToBottom(ElementReference element, string behavior, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Destroy(ElementReference element, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeThemeInterop : IThemeInterop
    {
        public ValueTask<bool> Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(false);

        public ValueTask<bool> Toggle(System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(true);

        public ValueTask<bool> GetIsDark(System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(false);

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeOverlayInterop : IOverlayInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Activate(string overlayId, ElementReference container, bool trapFocus = true, bool lockScroll = true, string? initialFocusSelector = null,
            System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask ActivateScrollLock(string overlayId, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Deactivate(string overlayId, bool unlockScroll = true, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask ReleaseScrollLocks(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeCarouselInterop : ICarouselInterop
    {
        public ValueTask Initialize(ElementReference element, DotNetObjectReference<Carousel> callbackReference, int currentIndex, bool isVertical, string? align,
            System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask<double> MeasureOffset(ElementReference element, int currentIndex, bool isVertical, string? align,
            System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(0d);

        public ValueTask Destroy(ElementReference element, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakePromptInputInterop : IPromptInputInterop
    {
        public ValueTask RegisterTextarea(ElementReference textarea, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask UnregisterTextarea(ElementReference textarea, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask OpenFileDialogById(string inputId, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask RegisterAttachmentsById(string inputId, DotNetObjectReference<PromptInputActionAddAttachments> callbackReference, bool globalDrop,
            System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask UnregisterAttachmentsById(string inputId, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

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

    private sealed class FakeColorPickerInterop : IColorPickerInterop
    {
        public ValueTask<bool> RegisterCanvas(ElementReference canvas, DotNetObjectReference<ColorPicker> callbackReference, bool disabled,
            System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(true);

        public ValueTask UnregisterCanvas(ElementReference canvas, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeClipboardUtil : IClipboardUtil
    {
        public ValueTask<bool> HasClipboard(System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(true);

        public ValueTask<ClipboardPermissionState> GetReadPermissionState(System.Threading.CancellationToken cancellationToken = default)
            => ValueTask.FromResult(ClipboardPermissionState.Granted);

        public ValueTask<ClipboardPermissionState> GetWritePermissionState(System.Threading.CancellationToken cancellationToken = default)
            => ValueTask.FromResult(ClipboardPermissionState.Granted);

        public ValueTask<string> ReadText(System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(string.Empty);

        public ValueTask<(bool Success, string? Text)> TryReadText(System.Threading.CancellationToken cancellationToken = default)
            => ValueTask.FromResult((true, (string?) string.Empty));

        public ValueTask WriteText(string? text, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask<bool> TryWriteText(string? text, System.Threading.CancellationToken cancellationToken = default) => ValueTask.FromResult(true);

        public ValueTask CopyText(string? text, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask CopyPlainAndHtml(string plainText, string? html = null, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask<IReadOnlyList<ClipboardItemDto>> Read(System.Threading.CancellationToken cancellationToken = default)
            => ValueTask.FromResult<IReadOnlyList<ClipboardItemDto>>(System.Array.Empty<ClipboardItemDto>());

        public ValueTask Write(IEnumerable<ClipboardItemDto> items, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask Clear(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    }

    private sealed class FakeSonnerInterop : ISonnerInterop
    {
        public ValueTask Initialize(System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask RegisterHotkey(ElementReference section, IReadOnlyList<string>? hotkey, System.Threading.CancellationToken cancellationToken = default)
            => ValueTask.CompletedTask;

        public ValueTask UnregisterHotkey(ElementReference section, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask<bool> RegisterSwipeHandlers(ElementReference section, DotNetObjectReference<Sonner> callbackReference,
            System.Threading.CancellationToken cancellationToken = default)
            => ValueTask.FromResult(true);

        public ValueTask UnregisterSwipeHandlers(ElementReference section, System.Threading.CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

        public ValueTask<Dictionary<string, double>> MeasureToastHeights(ElementReference section, System.Threading.CancellationToken cancellationToken = default)
            => ValueTask.FromResult(new Dictionary<string, double>());

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class FakeC15tConsentService : IC15tConsentService
    {
        public C15tConsentState CurrentState { get; private set; } = new();

        public bool HasConsent(string category) =>
            CurrentState.Consents?.TryGetValue(category, out bool value) == true && value;

        public ValueTask<C15tConsentState?> Initialize(C15tOptions? options = null, CancellationToken cancellationToken = default) =>
            CurrentStateResult();

        public ValueTask<C15tConsentState?> Refresh(CancellationToken cancellationToken = default) =>
            CurrentStateResult();

        public ValueTask<C15tConsentState?> AcceptAll(CancellationToken cancellationToken = default) =>
            CurrentStateResult();

        public ValueTask<C15tConsentState?> RejectNonNecessary(CancellationToken cancellationToken = default) =>
            CurrentStateResult();

        public ValueTask<C15tConsentState?> SaveCustom(CancellationToken cancellationToken = default) =>
            CurrentStateResult();

        public ValueTask<C15tConsentState?> SetConsent(string category, bool value, CancellationToken cancellationToken = default)
        {
            CurrentState.Consents ??= [];
            CurrentState.Consents[category] = value;
            return CurrentStateResult();
        }

        public ValueTask<C15tConsentState?> SetSelectedConsent(string category, bool value, CancellationToken cancellationToken = default)
        {
            CurrentState.SelectedConsents ??= [];
            CurrentState.SelectedConsents[category] = value;
            return CurrentStateResult();
        }

        public ValueTask<C15tConsentState?> OpenDialog(CancellationToken cancellationToken = default)
        {
            CurrentState.IsPrivacyDialogOpen = true;
            return CurrentStateResult();
        }

        public ValueTask<C15tConsentState?> ShowBanner(CancellationToken cancellationToken = default)
        {
            CurrentState.ShowPopup = true;
            return CurrentStateResult();
        }

        public ValueTask<C15tConsentState?> CloseUi(CancellationToken cancellationToken = default)
        {
            CurrentState.ShowPopup = false;
            CurrentState.IsPrivacyDialogOpen = false;
            return CurrentStateResult();
        }

        public ValueTask<C15tConsentState?> ResetConsents(CancellationToken cancellationToken = default)
        {
            CurrentState = new C15tConsentState();
            return CurrentStateResult();
        }

        private ValueTask<C15tConsentState?> CurrentStateResult() =>
            ValueTask.FromResult<C15tConsentState?>(CurrentState);
    }

}
