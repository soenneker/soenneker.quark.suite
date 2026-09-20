using System;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Demo.Services;

public sealed class ThemeService(IThemeInterop themeInterop) : IDisposable
{
    private bool _initialized;

    public ThemeMode Mode => themeInterop.Mode;

    public bool IsDark { get; private set; }
    public event Action? ThemeChanged;

    public async Task Initialize()
    {
        if (_initialized)
            return;

        themeInterop.ThemeChanged -= OnThemeChanged;
        themeInterop.ThemeChanged += OnThemeChanged;
        IsDark = await themeInterop.Initialize();
        _initialized = true;
        ThemeChanged?.Invoke();
    }

    private void OnThemeChanged(bool isDark)
    {
        IsDark = isDark;
        ThemeChanged?.Invoke();
    }

    public void Dispose() => themeInterop.ThemeChanged -= OnThemeChanged;

    public async Task SetMode(ThemeMode mode)
    {
        IsDark = await themeInterop.SetMode(mode);
        ThemeChanged?.Invoke();
    }

    public async Task Toggle()
    {
        IsDark = await themeInterop.Toggle();
        ThemeChanged?.Invoke();
    }
}
