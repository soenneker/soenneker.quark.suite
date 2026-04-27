using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue]
public sealed partial class TimelineConnectorStyle
{
    public static readonly TimelineConnectorStyle Solid = new(0);
    public static readonly TimelineConnectorStyle Dashed = new(1);
    public static readonly TimelineConnectorStyle Dotted = new(2);
}
