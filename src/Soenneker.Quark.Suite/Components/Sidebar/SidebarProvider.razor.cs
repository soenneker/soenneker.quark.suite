using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

public partial class SidebarProvider
{
    private const string DefaultCookieKey = "sidebar_state";
    private const string DefaultShortcutKey = "b";
    private const string ModulePath = "./_content/Soenneker.Quark.Suite/js/sidebarinterop.js";

    private IJSObjectReference? _module;
    private DotNetObjectReference<SidebarProvider>? _dotNetRef;
    private bool _openInternal = true;
    private bool _openMobileInternal;
    private bool _isMobileDetected;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null!;

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
    public string CookieKey { get; set; } = DefaultCookieKey;

    [Parameter]
    public bool PersistState { get; set; } = true;

    [Parameter]
    public string KeyboardShortcutKey { get; set; } = DefaultShortcutKey;

    [Parameter]
    public string SidebarWidth { get; set; } = "16rem";

    [Parameter]
    public string SidebarWidthIcon { get; set; } = "3rem";

    [Parameter]
    public string SidebarWidthMobile { get; set; } = "18rem";

    protected override void OnInitialized()
    {
        _openInternal = DefaultOpen;
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
            _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", ModulePath);
            _dotNetRef = DotNetObjectReference.Create(this);

            bool? savedOpen = null;

            if (PersistState && !string.IsNullOrWhiteSpace(CookieKey))
            {
                JsonElement result = await _module.InvokeAsync<JsonElement>("getSidebarState", CookieKey);
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
            AppendClass(ref cls, "q-sidebar-wrapper");
            AppendClass(ref cls, "group/sidebar-wrapper flex min-h-svh w-full has-data-[variant=inset]:bg-sidebar");
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
                await _module.DisposeAsync();
            }
            catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or TaskCanceledException or ObjectDisposedException)
            {
            }
        }

        _dotNetRef?.Dispose();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
