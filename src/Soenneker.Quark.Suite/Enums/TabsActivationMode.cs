using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>Radix Tabs <c>activationMode</c>: automatic selects on focus; manual requires activation (Space/Enter/click).</summary>
[EnumValue<string>]
public sealed partial class TabsActivationMode
{
    public static readonly TabsActivationMode Automatic = new("automatic");
    public static readonly TabsActivationMode Manual = new("manual");
}
