namespace Soenneker.Quark;
/// <summary>Formatting and link information at the current editor selection.</summary>
public sealed record RichTextFormatState
{
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public bool Underline { get; init; }
    public bool Bullets { get; init; }
    public bool Numbered { get; init; }
    public string? LinkUrl { get; init; }
    public string? LinkText { get; init; }
}
