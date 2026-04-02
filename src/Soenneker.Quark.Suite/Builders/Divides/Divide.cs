namespace Soenneker.Quark;

public static class Divide
{
    /// <summary>
    /// Fluent step for `X` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static DivideBuilder X => new("divide-x");
    /// <summary>
    /// Fluent step for `Y` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static DivideBuilder Y => new("divide-y");
    public static DivideBuilder Color(string value) => new("divide", value);
    public static DivideBuilder Opacity(int value) => new("divide-opacity", value.ToString());
}
