using System;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Demo.Services;

public sealed class ThemeService(IThemeInterop themeInterop)
{
    private bool _initialized;

    public bool IsDark { get; private set; }
    public event Action? ThemeChanged;

    public async Task InitializeAsync()
    {
        if (_initialized)
            return;

        IsDark = await themeInterop.Initialize();
        _initialized = true;
        ThemeChanged?.Invoke();
    }

    public async Task ToggleAsync()
    {
        IsDark = await themeInterop.Toggle();
        ThemeChanged?.Invoke();
    }
}
