namespace Soenneker.Quark;

/// <summary>
/// Tailwind ring utility.
/// </summary>
public static class Ring
{
    public static RingBuilder Default => new("ring");
    public static RingBuilder None => new("0");
    public static RingBuilder One => new("1");
    public static RingBuilder Two => new("2");
    public static RingBuilder Four => new("4");
    public static RingBuilder Eight => new("8");
    public static RingBuilder Inset => new("inset");
    public static RingBuilder Primary => new("primary");
    public static RingBuilder Secondary => new("secondary");
    public static RingBuilder Destructive => new("destructive");
    public static RingBuilder Muted => new("muted");
    public static RingBuilder Accent => new("accent");
    public static RingBuilder White => new("white");
    public static RingBuilder Black => new("black");

    public static RingBuilder Token(string value) => new(value);
}
