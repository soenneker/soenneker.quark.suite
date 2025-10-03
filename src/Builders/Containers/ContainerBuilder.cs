using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap container utilities.
/// </summary>
public class ContainerBuilder : ICssBuilder
{
    private ContainerUtility? _container;

    public bool IsEmpty => !_container.HasValue;
    public bool IsCssClass => true;
    public bool IsCssStyle => false;

    public ContainerBuilder Set(ContainerUtility container)
    {
        _container = container;
        return this;
    }

    public ContainerBuilder Default()
    {
        _container = ContainerUtility.Default;
        return this;
    }

    public ContainerBuilder Fluid()
    {
        _container = ContainerUtility.Fluid;
        return this;
    }

    public ContainerBuilder Small()
    {
        _container = ContainerUtility.Small;
        return this;
    }

    public ContainerBuilder Medium()
    {
        _container = ContainerUtility.Medium;
        return this;
    }

    public ContainerBuilder Large()
    {
        _container = ContainerUtility.Large;
        return this;
    }

    public ContainerBuilder ExtraLarge()
    {
        _container = ContainerUtility.ExtraLarge;
        return this;
    }

    public ContainerBuilder ExtraExtraLarge()
    {
        _container = ContainerUtility.ExtraExtraLarge;
        return this;
    }

    public override string ToString()
    {
        if (!_container.HasValue) return string.Empty;

        return GetContainerClass(_container.Value);
    }

    private static string GetContainerClass(ContainerUtility container)
    {
        return container.Type.Value switch
        {
            ContainerType.DefaultValue => "container",
            ContainerType.FluidValue => "container-fluid",
            ContainerType.ResponsiveValue => container.Breakpoint.Value switch
            {
                ContainerBreakpoint.SmValue => "container-sm",
                ContainerBreakpoint.MdValue => "container-md",
                ContainerBreakpoint.LgValue => "container-lg",
                ContainerBreakpoint.XlValue => "container-xl",
                ContainerBreakpoint.XxlValue => "container-xxl",
                _ => "container"
            },
            _ => "container"
        };
    }

    public string ToClass() => ToString();
    public string ToStyle() => string.Empty;
}
