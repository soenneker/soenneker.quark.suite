using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap container utility with fluent API.
/// </summary>
public static class ContainerType
{
    /// <summary>
    /// Default container. Generates 'container' class.
    /// </summary>
    public static ContainerBuilder Default => new(ContainerVariant.Default);

    /// <summary>
    /// Fluid container. Generates 'container-fluid' class.
    /// </summary>
    public static ContainerBuilder Fluid => new(ContainerVariant.Fluid);

    /// <summary>
    /// Small responsive container. Generates 'container-sm' class.
    /// </summary>
    public static ContainerBuilder Small => new(ContainerVariant.Responsive, ContainerBreakpoint.Sm);

    /// <summary>
    /// Medium responsive container. Generates 'container-md' class.
    /// </summary>
    public static ContainerBuilder Medium => new(ContainerVariant.Responsive, ContainerBreakpoint.Md);

    /// <summary>
    /// Large responsive container. Generates 'container-lg' class.
    /// </summary>
    public static ContainerBuilder Large => new(ContainerVariant.Responsive, ContainerBreakpoint.Lg);

    /// <summary>
    /// Extra large responsive container. Generates 'container-xl' class.
    /// </summary>
    public static ContainerBuilder ExtraLarge => new(ContainerVariant.Responsive, ContainerBreakpoint.Xl);

    /// <summary>
    /// Extra extra large responsive container. Generates 'container-xxl' class.
    /// </summary>
    public static ContainerBuilder ExtraExtraLarge => new(ContainerVariant.Responsive, ContainerBreakpoint.Xxl);
}
