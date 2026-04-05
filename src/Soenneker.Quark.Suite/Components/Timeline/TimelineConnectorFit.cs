using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class TimelineConnectorFit
{
    public static readonly TimelineConnectorFit Spaced = new("spaced");
    public static readonly TimelineConnectorFit Connected = new("connected");
}
