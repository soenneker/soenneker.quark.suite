using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

public interface IThemeProvider
{
    string? CurrentTheme { get; set; }

    Dictionary<string, Theme>? Themes { get; set; }

    Dictionary<string, Func<Theme, ComponentOptions?>> ComponentOptions { get; set; }

    string? GenerateRootCss();
}
