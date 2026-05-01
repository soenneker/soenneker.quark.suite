namespace Soenneker.Quark;

/// <summary>
/// Contains literal Tailwind class names so the CLI content scanner detects them.
/// Margin, Padding, and Rounded builders construct these at runtime from parts.
/// </summary>
internal static class TailwindContentStub
{
    // Margin: m + sides(t,e,b,s,x,y) + sizes(0,1,2,3,4,5,auto)
    private const string Margin = "m-0 m-1 m-2 m-3 m-4 m-5 m-auto mt-0 mt-1 mt-2 mt-3 mt-4 mt-5 mt-auto mb-0 mb-1 mb-2 mb-3 mb-4 mb-5 mb-auto ms-0 ms-1 ms-2 ms-3 ms-4 ms-5 ms-auto me-0 me-1 me-2 me-3 me-4 me-5 me-auto mx-0 mx-1 mx-2 mx-3 mx-4 mx-5 mx-auto my-0 my-1 my-2 my-3 my-4 my-5 my-auto";

    // Padding: p + sides(t,e,b,s,x,y) + sizes(0,1,2,3,4,5)
    private const string Padding = "p-0 p-1 p-2 p-3 p-4 p-5 pt-0 pt-1 pt-2 pt-3 pt-4 pt-5 pb-0 pb-1 pb-2 pb-3 pb-4 pb-5 ps-0 ps-1 ps-2 ps-3 ps-4 ps-5 pe-0 pe-1 pe-2 pe-3 pe-4 pe-5 px-0 px-1 px-2 px-3 px-4 px-5 py-0 py-1 py-2 py-3 py-4 py-5";

    // Layout classes emitted by built-in presets.
    private static readonly CssValue<MaxWidthBuilder> PresetMaxWidth = MaxWidth.Token("[1400px]");

    // Rounded: rounded + corners(tl,tr,bl,br,t,b,l,r) + sizes(none,sm,md,lg,xl,2xl,3xl,full)
    private const string Rounded = "rounded rounded-none rounded-sm rounded-md rounded-lg rounded-xl rounded-2xl rounded-3xl rounded-full rounded-t rounded-t-none rounded-t-sm rounded-t-md rounded-t-lg rounded-t-xl rounded-t-2xl rounded-t-3xl rounded-t-full rounded-b rounded-b-none rounded-b-sm rounded-b-md rounded-b-lg rounded-b-xl rounded-b-2xl rounded-b-3xl rounded-b-full rounded-l rounded-l-none rounded-l-sm rounded-l-md rounded-l-lg rounded-l-xl rounded-l-2xl rounded-l-3xl rounded-l-full rounded-r rounded-r-none rounded-r-sm rounded-r-md rounded-r-lg rounded-r-xl rounded-r-2xl rounded-r-3xl rounded-r-full rounded-tl rounded-tl-none rounded-tl-sm rounded-tl-md rounded-tl-lg rounded-tl-xl rounded-tl-2xl rounded-tl-3xl rounded-tl-full rounded-tr rounded-tr-none rounded-tr-sm rounded-tr-md rounded-tr-lg rounded-tr-xl rounded-tr-2xl rounded-tr-3xl rounded-tr-full rounded-bl rounded-bl-none rounded-bl-sm rounded-bl-md rounded-bl-lg rounded-bl-xl rounded-bl-2xl rounded-bl-3xl rounded-bl-full rounded-br rounded-br-none rounded-br-sm rounded-br-md rounded-br-lg rounded-br-xl rounded-br-2xl rounded-br-3xl rounded-br-full";

    // Ring: widths/colors/inset commonly used by the ring builder.
    private const string Ring = "ring ring-0 ring-1 ring-2 ring-4 ring-8 ring-inset ring-primary ring-secondary ring-destructive ring-muted ring-accent ring-white ring-black";

    // State variants that are difficult for the build-time scanner to discover reliably.
    private const string StateVariants = "data-[state=checked]:border-primary data-[state=checked]:bg-primary data-[state=checked]:text-primary-foreground dark:data-[state=checked]:bg-primary has-[[data-slot=checkbox][data-state=checked]]:border-primary/30 has-[[data-slot=checkbox][data-state=checked]]:bg-muted has-[[data-slot=radio-group-item][data-state=checked]]:border-primary/30 has-[[data-slot=radio-group-item][data-state=checked]]:bg-muted dark:has-[[data-slot=checkbox][data-state=checked]]:border-primary/20 dark:has-[[data-slot=checkbox][data-state=checked]]:bg-primary/10 dark:has-[[data-slot=radio-group-item][data-state=checked]]:border-primary/20 dark:has-[[data-slot=radio-group-item][data-state=checked]]:bg-primary/10";

    // Calendar variable utilities used by DayPicker-style markup.
    private const string Calendar = "min-w-(--cell-size) rounded-(--cell-radius) rounded-l-(--cell-radius) rounded-r-(--cell-radius)";

}
