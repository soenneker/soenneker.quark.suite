namespace Soenneker.Quark;

public static class Align
{
    public static AlignBuilder JustifyStart => new("justify-start", "");
    public static AlignBuilder JustifyEnd => new("justify-end", "");
    public static AlignBuilder JustifyCenter => new("justify-center", "");
    public static AlignBuilder JustifyBetween => new("justify-between", "");
    public static AlignBuilder JustifyAround => new("justify-around", "");
    public static AlignBuilder JustifyEvenly => new("justify-evenly", "");

    public static AlignBuilder ItemsStart => new("items-start", "");
    public static AlignBuilder ItemsEnd => new("items-end", "");
    public static AlignBuilder ItemsCenter => new("items-center", "");
    public static AlignBuilder ItemsBaseline => new("items-baseline", "");
    public static AlignBuilder ItemsStretch => new("items-stretch", "");

    public static AlignBuilder ContentStart => new("content-start", "");
    public static AlignBuilder ContentEnd => new("content-end", "");
    public static AlignBuilder ContentCenter => new("content-center", "");
    public static AlignBuilder ContentBetween => new("content-between", "");
    public static AlignBuilder ContentAround => new("content-around", "");
    public static AlignBuilder ContentEvenly => new("content-evenly", "");
    public static AlignBuilder ContentStretch => new("content-stretch", "");

    public static AlignBuilder SelfAuto => new("self-auto", "");
    public static AlignBuilder SelfStart => new("self-start", "");
    public static AlignBuilder SelfEnd => new("self-end", "");
    public static AlignBuilder SelfCenter => new("self-center", "");
    public static AlignBuilder SelfStretch => new("self-stretch", "");
    public static AlignBuilder SelfBaseline => new("self-baseline", "");

    public static AlignBuilder JustifyItemsStart => new("justify-items-start", "");
    public static AlignBuilder JustifyItemsEnd => new("justify-items-end", "");
    public static AlignBuilder JustifyItemsCenter => new("justify-items-center", "");
    public static AlignBuilder JustifyItemsStretch => new("justify-items-stretch", "");

    public static AlignBuilder JustifySelfAuto => new("justify-self-auto", "");
    public static AlignBuilder JustifySelfStart => new("justify-self-start", "");
    public static AlignBuilder JustifySelfEnd => new("justify-self-end", "");
    public static AlignBuilder JustifySelfCenter => new("justify-self-center", "");
    public static AlignBuilder JustifySelfStretch => new("justify-self-stretch", "");
}
