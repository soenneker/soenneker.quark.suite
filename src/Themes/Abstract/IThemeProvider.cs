using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

public interface IThemeProvider
{
    string? CurrentTheme { get; set; }

    Dictionary<string, Theme>? Themes { get; set; }

    string? GenerateBootstrapCss();

    string? GenerateComponentsCss();
}
