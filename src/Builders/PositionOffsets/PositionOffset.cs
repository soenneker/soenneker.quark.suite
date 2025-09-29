
namespace Soenneker.Quark;

public static class PositionOffset
{
    public static PositionOffsetBuilder Top0 => new("top", "0");
    public static PositionOffsetBuilder Top50 => new("top", "50");
    public static PositionOffsetBuilder Top100 => new("top", "100");

    public static PositionOffsetBuilder Bottom0 => new("bottom", "0");
    public static PositionOffsetBuilder Bottom50 => new("bottom", "50");
    public static PositionOffsetBuilder Bottom100 => new("bottom", "100");

    public static PositionOffsetBuilder Start0 => new("start", "0");
    public static PositionOffsetBuilder Start50 => new("start", "50");
    public static PositionOffsetBuilder Start100 => new("start", "100");

    public static PositionOffsetBuilder End0 => new("end", "0");
    public static PositionOffsetBuilder End50 => new("end", "50");
    public static PositionOffsetBuilder End100 => new("end", "100");

    public static PositionOffsetBuilder TranslateMiddle => new("translate", "middle");
    public static PositionOffsetBuilder TranslateMiddleX => new("translate", "middle-x");
    public static PositionOffsetBuilder TranslateMiddleY => new("translate", "middle-y");
}
