using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Quark.Dtos;

namespace Soenneker.Quark;

/// <summary>
/// Represents the sidebar provider.
/// </summary>
public partial class SidebarProvider
{
    private const string _defaultCookieKey = "sidebar_state";
    private const string _defaultShortcutKey = "b";
    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/sidebarinterop.js";

    private IJSObjectReference? _module;
    private DotNetObjectReference<SidebarProvider>? _dotNetRef;
    private bool _openInternal = true;
    private bool _openMobileInternal;
    private bool _isMobileDetected;

    [Inject]
    private IModuleImportUtil ModuleImportUtil { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IOverlayInterop OverlayInterop { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether default open.
    /// </summary>
    [Parameter]
    public bool DefaultOpen { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether open.
    /// </summary>
    [Parameter]
    public bool? Open { get; set; }

    /// <summary>
    /// Gets or sets open changed.
    /// </summary>
    [Parameter]
    public EventCallback<bool> OpenChanged { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether open mobile.
    /// </summary>
    [Parameter]
    public bool? OpenMobile { get; set; }

    /// <summary>
    /// Gets or sets open mobile changed.
    /// </summary>
    [Parameter]
    public EventCallback<bool> OpenMobileChanged { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the instance is mobile.
    /// </summary>
    [Parameter]
    public bool? IsMobile { get; set; }

    /// <summary>
    /// Gets or sets cookie key.
    /// </summary>
    [Parameter]
    public string CookieKey { get; set; } = _defaultCookieKey;

    /// <summary>
    /// Gets or sets a value indicating whether persist state.
    /// </summary>
    [Parameter]
    public bool PersistState { get; set; } = true;

    /// <summary>
    /// Gets or sets keyboard shortcut key.
    /// </summary>
    [Parameter]
    public string KeyboardShortcutKey { get; set; } = _defaultShortcutKey;

    /// <summary>
    /// Gets or sets a value indicating whether close mobile on navigation.
    /// </summary>
    [Parameter]
    public bool CloseMobileOnNavigation { get; set; } = true;

    /// <summary>
    /// Gets or sets sidebar width.
    /// </summary>
    [Parameter]
    public string SidebarWidth { get; set; } = "16rem";

    /// <summary>
    /// Gets or sets sidebar width icon.
    /// </summary>
    [Parameter]
    public string SidebarWidthIcon { get; set; } = "3rem";

    /// <summary>
    /// Gets or sets sidebar width mobile.
    /// </summary>
    [Parameter]
    public string SidebarWidthMobile { get; set; } = "18rem";

    protected override void ApplyDefaultParameters()
    {
        base.ApplyDefaultParameters();
        DataSlot ??= "sidebar-wrapper";

        Display ??= Quark.Display.Flex;
        Width ??= Quark.Width.IsFull;
    }

    protected override void OnInitialized()
    {
        _openInternal = DefaultOpen;
        NavigationManager.LocationChanged += OnLocationChanged;
        base.OnInitialized();
    }

    /// <summary>
    /// Gets open.
    /// </summary>
    /// <returns>A value indicating whether the operation succeeded.</returns>
    public bool GetOpen() => Open ?? _openInternal;

    /// <summary>
    /// Gets open mobile.
    /// </summary>
    /// <returns>A value indicating whether the operation succeeded.</returns>
    public bool GetOpenMobile() => OpenMobile ?? _openMobileInternal;

    /// <summary>
    /// Gets is mobile.
    /// </summary>
    /// <returns>A value indicating whether the operation succeeded.</returns>
    public bool GetIsMobile() => IsMobile ?? _isMobileDetected;

    /// <summary>
    /// Gets state.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    public SidebarContextState GetState()
    {
        return new SidebarContextState
        {
            Open = GetOpen(),
            OpenMobile = GetOpenMobile(),
            IsMobile = GetIsMobile()
        };
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        try
        {
            _module = await ModuleImportUtil.GetContentModuleReference(_modulePath);
            _dotNetRef = DotNetObjectReference.Create(this);

            bool? savedOpen = null;

            if (PersistState && !string.IsNullOrWhiteSpace(CookieKey))
            {
                var result = await _module.InvokeAsync<JsonElement>("getSidebarState", CookieKey);
                savedOpen = result.ValueKind switch
                {
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    _ => null
                };
            }

            if (Open is null)
                _openInternal = savedOpen ?? DefaultOpen;

            await _module.InvokeVoidAsync("initializeSidebar", _dotNetRef, KeyboardShortcutKey);
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or TaskCanceledException or ObjectDisposedException)
        {
            if (Open is null)
                _openInternal = DefaultOpen;

            await InvokeAsync(StateHasChanged);
        }
    }

    /// <summary>
    /// Sets open.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SetOpen(bool value)
    {
        if (Open is null)
            _openInternal = value;

        if (OpenChanged.HasDelegate)
            await OpenChanged.InvokeAsync(value);

        await PersistOpen(value);
        await RefreshOffThread();
    }

    /// <summary>
    /// Sets open mobile.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SetOpenMobile(bool value)
    {
        if (OpenMobile is null)
            _openMobileInternal = value;

        if (OpenMobileChanged.HasDelegate)
            await OpenMobileChanged.InvokeAsync(value);

        await RefreshOffThread();
    }

    /// <summary>
    /// Executes the toggle sidebar operation.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task ToggleSidebar()
    {
        return GetIsMobile() ? SetOpenMobile(!GetOpenMobile()) : SetOpen(!GetOpen());
    }

    /// <summary>
    /// Executes the on mobile change operation.
    /// </summary>
    /// <param name="isMobile">The is mobile.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [JSInvokable]
    public async Task OnMobileChange(bool isMobile)
    {
        _isMobileDetected = isMobile;

        if (!isMobile && GetOpenMobile())
            await SetOpenMobile(false);
        else
            await RefreshOffThread();
    }

    /// <summary>
    /// Executes the on toggle shortcut operation.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [JSInvokable]
    public Task OnToggleShortcut()
    {
        return ToggleSidebar();
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        if (!CloseMobileOnNavigation || !GetOpenMobile())
            return;

        _ = InvokeAsync(CloseMobileAfterNavigation);
    }

    private async Task CloseMobileAfterNavigation()
    {
        await SetOpenMobile(false);

        try
        {
            await OverlayInterop.ReleaseScrollLocks();
        }
        catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or TaskCanceledException or ObjectDisposedException)
        {
        }
    }

    private async Task PersistOpen(bool value)
    {
        if (!PersistState || _module is null || string.IsNullOrWhiteSpace(CookieKey))
            return;

        try
        {
            await _module.InvokeVoidAsync("saveSidebarState", CookieKey, value);
        }
        catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or TaskCanceledException or ObjectDisposedException)
        {
        }
    }

    protected override void BuildAttributesCore(Dictionary<string, object> attributes)
    {
        base.BuildAttributesCore(attributes);


        BuildClassAndStyleAttributes(attributes, (ref cls, ref sty) =>
        {
            AppendClass(ref cls, "group/sidebar-wrapper has-data-[variant=inset]:bg-sidebar");

            if (!HasExplicitHeight())
                AppendClass(ref cls, "min-h-svh");

            AppendStyleDecl(ref sty, $"--sidebar-width: {SidebarWidth}");
            AppendStyleDecl(ref sty, $"--sidebar-width-icon: {SidebarWidthIcon}");
            AppendStyleDecl(ref sty, $"--sidebar-width-mobile: {SidebarWidthMobile}");
        });
    }

    private bool HasExplicitHeight()
    {
        if (Height is not null || MinHeight is not null)
            return true;

        if (!string.IsNullOrWhiteSpace(Class) &&
            (Class.Contains("h-", StringComparison.Ordinal) || Class.Contains("min-h-", StringComparison.Ordinal)))
        {
            return true;
        }

        return !string.IsNullOrWhiteSpace(Style) &&
               (Style.Contains("height", StringComparison.OrdinalIgnoreCase) || Style.Contains("min-height", StringComparison.OrdinalIgnoreCase));
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        hc.Add(DefaultOpen);
        hc.Add(Open);
        hc.Add(OpenMobile);
        hc.Add(IsMobile);
        hc.Add(CookieKey);
        hc.Add(PersistState);
        hc.Add(KeyboardShortcutKey);
        hc.Add(CloseMobileOnNavigation);
        hc.Add(SidebarWidth);
        hc.Add(SidebarWidthIcon);
        hc.Add(SidebarWidthMobile);
        hc.Add(_isMobileDetected);
        hc.Add(_openInternal);
        hc.Add(_openMobileInternal);
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async ValueTask DisposeAsync()
    {
        if (_module is not null)
        {
            try
            {
                await _module.InvokeVoidAsync("cleanup");
            }
            catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or TaskCanceledException or ObjectDisposedException)
            {
            }
        }

        NavigationManager.LocationChanged -= OnLocationChanged;
        _dotNetRef?.Dispose();
        if (_module is not null)
            await ModuleImportUtil.DisposeContentModule(_modulePath);
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}