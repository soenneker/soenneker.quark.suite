namespace Soenneker.Quark;

internal static class QuarkAssetPath
{
    internal static string JavaScript(string path, QuarkOptions? options) =>
        options?.UseMinifiedJavaScript == true ? path[..^3] + ".min.js" : path;

    internal static string Css(string path, QuarkOptions? options) =>
        options?.UseMinifiedCss == true ? path[..^4] + ".min.css" : path;
}
