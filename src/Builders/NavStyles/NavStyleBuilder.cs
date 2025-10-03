using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap nav utilities.
/// </summary>
public class NavStyleBuilder : ICssBuilder
{
    private NavStyle? _nav;

    public bool IsEmpty => !_nav.HasValue;
    public bool IsCssClass => true;
    public bool IsCssStyle => false;

    public NavStyleBuilder Set(NavStyle nav)
    {
        _nav = nav;
        return this;
    }

    // Nav styles
    public NavStyleBuilder Default()
    {
        _nav = NavStyle.Default;
        return this;
    }

    public NavStyleBuilder Tabs()
    {
        _nav = NavStyle.Tabs;
        return this;
    }

    public NavStyleBuilder Pills()
    {
        _nav = NavStyle.Pills;
        return this;
    }

    public NavStyleBuilder Justified()
    {
        _nav = NavStyle.Justified;
        return this;
    }

    public NavStyleBuilder Fill()
    {
        _nav = NavStyle.Fill;
        return this;
    }

    public override string ToString()
    {
        if (!_nav.HasValue) return string.Empty;
        return GetNavClass(_nav.Value);
    }

    private static string GetNavClass(NavStyle nav)
    {
        var baseClass = "nav";
        var styleClass = nav.Type.Value switch
        {
            NavStyleType.TabsValue => " nav-tabs",
            NavStyleType.PillsValue => " nav-pills",
            NavStyleType.JustifiedValue => " nav-justified",
            NavStyleType.FillValue => " nav-fill",
            _ => string.Empty
        };

        return $"{baseClass}{styleClass}";
    }

    public string ToClass() => ToString();
    public string ToStyle() => string.Empty;
}
