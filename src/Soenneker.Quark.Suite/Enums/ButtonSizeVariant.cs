namespace Soenneker.Quark;

/// <summary>
/// Defines the size variant for a Button component.
/// Follows shadcn/ui size scale conventions.
/// </summary>
public enum ButtonSizeVariant
{
    /// <summary>Small size (h-9, px-3, text-xs).</summary>
    Small,

    /// <summary>Default size (h-10, px-4, py-2).</summary>
    Default,

    /// <summary>Large size (h-11, px-8).</summary>
    Large,

    /// <summary>Icon-only size (h-10 w-10).</summary>
    Icon,

    /// <summary>Small icon-only (h-9 w-9).</summary>
    IconSmall,

    /// <summary>Large icon-only (h-11 w-11).</summary>
    IconLarge
}
