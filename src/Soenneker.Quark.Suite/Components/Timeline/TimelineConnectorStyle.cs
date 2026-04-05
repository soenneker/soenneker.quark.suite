using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class TimelineConnectorStyle
{
    public static readonly TimelineConnectorStyle Solid = new("solid");
    public static readonly TimelineConnectorStyle Dashed = new("dashed");
    public static readonly TimelineConnectorStyle Dotted = new("dotted");
}
