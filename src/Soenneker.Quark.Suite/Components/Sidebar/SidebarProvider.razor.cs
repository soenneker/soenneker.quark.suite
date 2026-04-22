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

    [Parameter]
    public bool DefaultOpen { get; set; } = true;

    [Parameter]
    public bool? Open { get; set; }

    [Parameter]
    public EventCallback<bool> OpenChanged { get; set; }

    [Parameter]
    public bool? OpenMobile { get; set; }

    [Parameter]
    public EventCallback<bool> OpenMobileChanged { get; set; }

    [Parameter]
    public bool? IsMobile { get; set; }

    [Parameter]
    public string CookieKey { get; set; } = _defaultCookieKey;

    [Parameter]
    public bool PersistState { get; set; } = true;

    [Parameter]
    public string KeyboardShortcutKey { get; set; } = _defaultShortcutKey;

    [Parameter]
    public bool CloseMobileOnNavigation { get; set; } = true;

    [Parameter]
    public string SidebarWidth { get; set; } = "16rem";

    [Parameter]
    public string SidebarWidthIcon { get; set; } = "3rem";

    [Parameter]
    public string SidebarWidthMobile { get; set; } = "18rem";

    protected override void ApplyDefaultParameters()
    {
        base.ApplyDefaultParameters();

        Display ??= Quark.Display.Flex;
        Width ??= Quark.Width.IsFull;
    }

    protected override void OnInitialized()
    {
        _openInternal = DefaultOpen;
        NavigationManager.LocationChanged += OnLocationChanged;
        base.OnInitialized();
    }

    public bool GetOpen() => Open ?? _openInternal;

    public bool GetOpenMobile() => OpenMobile ?? _openMobileInternal;

    public bool GetIsMobile() => IsMobile ?? _isMobileDetected;

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

    public async Task SetOpen(bool value)
    {
        if (Open is null)
            _openInternal = value;

        if (OpenChanged.HasDelegate)
            await OpenChanged.InvokeAsync(value);

        await PersistOpenAsync(value);
        await RefreshOffThread();
    }

    public async Task SetOpenMobile(bool value)
    {
        if (OpenMobile is null)
            _openMobileInternal = value;

        if (OpenMobileChanged.HasDelegate)
            await OpenMobileChanged.InvokeAsync(value);

        await RefreshOffThread();
    }

    public Task ToggleSidebar()
    {
        return GetIsMobile() ? SetOpenMobile(!GetOpenMobile()) : SetOpen(!GetOpen());
    }

    [JSInvokable]
    public async Task OnMobileChange(bool isMobile)
    {
        _isMobileDetected = isMobile;

        if (!isMobile && GetOpenMobile())
            await SetOpenMobile(false);
        else
            await RefreshOffThread();
    }

    [JSInvokable]
    public Task OnToggleShortcut()
    {
        return ToggleSidebar();
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        if (!CloseMobileOnNavigation || !GetOpenMobile())
            return;

        _ = InvokeAsync(() => SetOpenMobile(false));
    }

    private async Task PersistOpenAsync(bool value)
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

        attributes["data-slot"] = "sidebar-wrapper";

        BuildClassAndStyleAttributes(attributes, (ref cls, ref sty) =>
        {
            AppendClass(ref cls, "group/sidebar-wrapper min-h-svh has-data-[variant=inset]:bg-sidebar");
            AppendStyleDecl(ref sty, $"--sidebar-width: {SidebarWidth}");
            AppendStyleDecl(ref sty, $"--sidebar-width-icon: {SidebarWidthIcon}");
            AppendStyleDecl(ref sty, $"--sidebar-width-mobile: {SidebarWidthMobile}");
        });
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
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
