namespace Soenneker.Quark;

/// <summary>
/// Max-width utility with fluent API. Tailwind-first (max-w-*).
/// </summary>
public static class MaxWidth
{
    /// <summary>
    /// Fluent step for `Is None` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsNone => new("none");
    /// <summary>
    /// Fluent step for `Is Xs` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsXs => new("xs");
    /// <summary>
    /// Fluent step for `Is Sm` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsSm => new("sm");
    /// <summary>
    /// Fluent step for `Is Md` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsMd => new("md");
    /// <summary>
    /// Fluent step for `Is Lg` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsLg => new("lg");
    /// <summary>
    /// Fluent step for `Is Xl` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsXl => new("xl");
    /// <summary>
    /// Fluent step for `Is2xl` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder Is2xl => new("2xl");
    /// <summary>
    /// Fluent step for `Is3xl` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder Is3xl => new("3xl");
    /// <summary>
    /// Fluent step for `Is4xl` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder Is4xl => new("4xl");
    /// <summary>
    /// Fluent step for `Is5xl` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder Is5xl => new("5xl");
    /// <summary>
    /// Fluent step for `Is6xl` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder Is6xl => new("6xl");
    /// <summary>
    /// Fluent step for `Is7xl` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder Is7xl => new("7xl");
    /// <summary>
    /// Fluent step for `Is Full` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsFull => new("full");
    /// <summary>
    /// Fluent step for `Is Min` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsMin => new("min");
    /// <summary>
    /// Fluent step for `Is Max` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsMax => new("max");
    /// <summary>
    /// Fluent step for `Is Fit` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsFit => new("fit");
    /// <summary>
    /// Fluent step for `Is Screen` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsScreen => new("screen");
    /// <summary>
    /// Fluent step for `Is Prose` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MaxWidthBuilder IsProse => new("prose");

    /// <summary>
    /// Create from an arbitrary Tailwind max-width token (e.g. "4xl", "[56rem]", "[75ch]").
    /// </summary>
    public static MaxWidthBuilder Token(string token) => new(token);
}
