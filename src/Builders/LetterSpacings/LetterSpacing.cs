namespace Soenneker.Quark;

public static class LetterSpacing
{
    public static LetterSpacingBuilder Tighter => new("tracking-tighter", "");
    public static LetterSpacingBuilder Tight => new("tracking-tight", "");
    public static LetterSpacingBuilder Normal => new("tracking-normal", "");
    public static LetterSpacingBuilder Wide => new("tracking-wide", "");
    public static LetterSpacingBuilder Wider => new("tracking-wider", "");
    public static LetterSpacingBuilder Widest => new("tracking-widest", "");
    public static LetterSpacingBuilder Token(string value) => new("tracking", value);
}
