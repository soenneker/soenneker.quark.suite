using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue]
public sealed partial class TimelineConnectorFit
{
    public static readonly TimelineConnectorFit Spaced = new(0);
    public static readonly TimelineConnectorFit Connected = new(1);
}
