using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Tag input field placement when tags are rendered outside the field.
/// </summary>
[EnumValue]
public sealed partial class TagInputFieldPosition
{
    public static readonly TagInputFieldPosition Bottom = new(0);
    public static readonly TagInputFieldPosition Top = new(1);
}
