
namespace Soenneker.Quark;

public static class TextBreak
{
    public static TextBreakBuilder Enable => new(true);
    public static TextBreakBuilder Disable => new(false);
}
