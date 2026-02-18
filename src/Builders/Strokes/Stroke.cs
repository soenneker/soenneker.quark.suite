namespace Soenneker.Quark;

public static class Stroke
{
    public static StrokeBuilder Token(string value) => new("stroke", value);
    public static StrokeBuilder Width(int value) => new("stroke", value.ToString());
    public static StrokeBuilder None => new("stroke", "none");
}
