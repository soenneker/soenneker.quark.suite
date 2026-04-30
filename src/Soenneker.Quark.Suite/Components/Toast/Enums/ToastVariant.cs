using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the visual style variant for a Toast component.
/// </summary>
[EnumValue<string>]
public sealed partial class ToastVariant
{
    public static readonly ToastVariant Default = new("default");

    public static readonly ToastVariant Destructive = new("destructive");
}
