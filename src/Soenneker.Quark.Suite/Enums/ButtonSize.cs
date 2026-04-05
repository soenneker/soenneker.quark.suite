using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Button size following shadcn/ui conventions (data-size: default | xs | sm | lg | icon | icon-xs | icon-sm | icon-lg).
/// </summary>
[EnumValue<string>]
public sealed partial class ButtonSize
{
    public static readonly ButtonSize Default = new("default");
    public static readonly ButtonSize Xs = new("xs");
    public static readonly ButtonSize Sm = new("sm");
    public static readonly ButtonSize Lg = new("lg");
    public static readonly ButtonSize Icon = new("icon");
    public static readonly ButtonSize IconXs = new("icon-xs");
    public static readonly ButtonSize IconSm = new("icon-sm");
    public static readonly ButtonSize IconLg = new("icon-lg");
}
