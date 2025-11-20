using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

///<inheritdoc cref="IThemeProvider"/>
public sealed class ThemeProvider : IThemeProvider
{
    public string? CurrentTheme { get; set; } = "Default";

    public Dictionary<string, Theme>? Themes { get; set; }

    public void AddTheme(Theme theme)
    {
        Themes ??= new Dictionary<string, Theme>();
        Themes[theme.Name] = theme;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string? GenerateBootstrapCss()
    {
        var currentTheme = GetCurrentTheme();

        if (currentTheme?.BootstrapCssVariables == null)
            return null;

        return BootstrapCssGenerator.Generate(currentTheme.BootstrapCssVariables);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string? GenerateComponentsCss()
    {
        var currentTheme = GetCurrentTheme();

        if (currentTheme == null)
            return null;

        return ComponentsCssGenerator.Generate(currentTheme);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Theme? GetCurrentTheme()
    {
        if (CurrentTheme.IsNullOrEmpty() || Themes == null)
            return null;

        return Themes.GetValueOrDefault(CurrentTheme);
    }
}
