namespace Soenneker.Quark;

public partial class Slider
{
    private struct ThumbView
    {
        internal QuarkAttributeDictionary Attributes;
        internal string? ParentId, Id, SourceLabel, Label;
        internal int TotalValues;
    }
}
