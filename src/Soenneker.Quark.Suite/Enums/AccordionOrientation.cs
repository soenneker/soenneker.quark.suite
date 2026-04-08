using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>Radix Accordion orientation (controls keyboard roving and <c>data-orientation</c>).</summary>
[EnumValue<string>]
public sealed partial class AccordionOrientation
{
    public static readonly AccordionOrientation Vertical = new("vertical");
    public static readonly AccordionOrientation Horizontal = new("horizontal");
}
