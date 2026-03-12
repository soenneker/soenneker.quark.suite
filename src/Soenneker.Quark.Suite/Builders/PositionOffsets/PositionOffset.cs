
namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating position offset builders with predefined values.
/// </summary>
public static class PositionOffset
{
    /// <summary>
    /// Gets a position offset builder with top 0 (0% from top).
    /// </summary>
    public static PositionOffsetBuilder Top0 => new("top", "0");
    /// <summary>
    /// Gets a position offset builder with top 50 (50% from top).
    /// </summary>
    public static PositionOffsetBuilder Top50 => new("top", "50");
    /// <summary>
    /// Gets a position offset builder with top 100 (100% from top).
    /// </summary>
    public static PositionOffsetBuilder Top100 => new("top", "100");

    /// <summary>
    /// Gets a position offset builder with bottom 0 (0% from bottom).
    /// </summary>
    public static PositionOffsetBuilder Bottom0 => new("bottom", "0");
    /// <summary>
    /// Gets a position offset builder with bottom 50 (50% from bottom).
    /// </summary>
    public static PositionOffsetBuilder Bottom50 => new("bottom", "50");
    /// <summary>
    /// Gets a position offset builder with bottom 100 (100% from bottom).
    /// </summary>
    public static PositionOffsetBuilder Bottom100 => new("bottom", "100");

    /// <summary>
    /// Gets a position offset builder with start 0 (0% from start).
    /// </summary>
    public static PositionOffsetBuilder Start0 => new("start", "0");
    /// <summary>
    /// Gets a position offset builder with start 50 (50% from start).
    /// </summary>
    public static PositionOffsetBuilder Start50 => new("start", "50");
    /// <summary>
    /// Gets a position offset builder with start 100 (100% from start).
    /// </summary>
    public static PositionOffsetBuilder Start100 => new("start", "100");

    /// <summary>
    /// Gets a position offset builder with end 0 (0% from end).
    /// </summary>
    public static PositionOffsetBuilder End0 => new("end", "0");
    /// <summary>
    /// Gets a position offset builder with end 50 (50% from end).
    /// </summary>
    public static PositionOffsetBuilder End50 => new("end", "50");
    /// <summary>
    /// Gets a position offset builder with end 100 (100% from end).
    /// </summary>
    public static PositionOffsetBuilder End100 => new("end", "100");

    /// <summary>
    /// Gets a position offset builder that translates to middle (centered both horizontally and vertically).
    /// </summary>
    public static PositionOffsetBuilder TranslateMiddle => new("translate", "middle");
    /// <summary>
    /// Gets a position offset builder that translates to middle-x (centered horizontally).
    /// </summary>
    public static PositionOffsetBuilder TranslateMiddleX => new("translate", "middle-x");
    /// <summary>
    /// Gets a position offset builder that translates to middle-y (centered vertically).
    /// </summary>
    public static PositionOffsetBuilder TranslateMiddleY => new("translate", "middle-y");
}
