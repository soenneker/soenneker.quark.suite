namespace Soenneker.Quark;

/// <summary>
/// Tailwind container utility.
/// </summary>
public static class ContainerType
{
    /// <summary>
    /// Generates Tailwind's <c>container</c> utility.
    /// </summary>
    public static ContainerBuilder Default => new(ContainerVariant.Default);
}
