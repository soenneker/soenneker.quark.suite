namespace Soenneker.Quark;

public static class Align
{
    /// <summary>
    /// `justify-start` — packs flex/grid items to the start of the main axis (`justify-content: flex-start`).
    /// </summary>
    public static AlignBuilder JustifyStart => new("justify-start", "");
    /// <summary>
    /// `justify-end` — packs items to the end of the main axis (`justify-content: flex-end`).
    /// </summary>
    public static AlignBuilder JustifyEnd => new("justify-end", "");
    /// <summary>
    /// `justify-center` — centers items on the main axis (`justify-content: center`).
    /// </summary>
    public static AlignBuilder JustifyCenter => new("justify-center", "");
    /// <summary>
    /// `justify-between` — space between items (`justify-content: space-between`).
    /// </summary>
    public static AlignBuilder JustifyBetween => new("justify-between", "");
    /// <summary>
    /// `justify-around` — space around each item (`justify-content: space-around`).
    /// </summary>
    public static AlignBuilder JustifyAround => new("justify-around", "");
    /// <summary>
    /// `justify-evenly` — evenly distributed space (`justify-content: space-evenly`).
    /// </summary>
    public static AlignBuilder JustifyEvenly => new("justify-evenly", "");

    /// <summary>
    /// `items-start` — align items to the start of the cross axis (`align-items: flex-start`).
    /// </summary>
    public static AlignBuilder ItemsStart => new("items-start", "");
    /// <summary>
    /// `items-end` — align to the end of the cross axis (`align-items: flex-end`).
    /// </summary>
    public static AlignBuilder ItemsEnd => new("items-end", "");
    /// <summary>
    /// `items-center` — center on the cross axis (`align-items: center`).
    /// </summary>
    public static AlignBuilder ItemsCenter => new("items-center", "");
    /// <summary>
    /// `items-baseline` — align baselines (`align-items: baseline`).
    /// </summary>
    public static AlignBuilder ItemsBaseline => new("items-baseline", "");
    /// <summary>
    /// `items-stretch` — stretch to fill the cross axis (`align-items: stretch`).
    /// </summary>
    public static AlignBuilder ItemsStretch => new("items-stretch", "");

    /// <summary>
    /// `content-start` — packs rows/columns to the start (`align-content: flex-start`).
    /// </summary>
    public static AlignBuilder ContentStart => new("content-start", "");
    /// <summary>
    /// `content-end` — packs to the end (`align-content: flex-end`).
    /// </summary>
    public static AlignBuilder ContentEnd => new("content-end", "");
    /// <summary>
    /// `content-center` — centers wrapped lines (`align-content: center`).
    /// </summary>
    public static AlignBuilder ContentCenter => new("content-center", "");
    /// <summary>
    /// `content-between` — space between rows (`align-content: space-between`).
    /// </summary>
    public static AlignBuilder ContentBetween => new("content-between", "");
    /// <summary>
    /// `content-around` — space around rows (`align-content: space-around`).
    /// </summary>
    public static AlignBuilder ContentAround => new("content-around", "");
    /// <summary>
    /// `content-evenly` — evenly spaced rows (`align-content: space-evenly`).
    /// </summary>
    public static AlignBuilder ContentEvenly => new("content-evenly", "");
    /// <summary>
    /// `content-stretch` — stretch rows (`align-content: stretch`).
    /// </summary>
    public static AlignBuilder ContentStretch => new("content-stretch", "");

    /// <summary>
    /// `self-auto` — default alignment for this item (`align-self: auto`).
    /// </summary>
    public static AlignBuilder SelfAuto => new("self-auto", "");
    /// <summary>
    /// `self-start` — align this item to the cross-start edge (`align-self: flex-start`).
    /// </summary>
    public static AlignBuilder SelfStart => new("self-start", "");
    /// <summary>
    /// `self-end` — align to the cross-end edge (`align-self: flex-end`).
    /// </summary>
    public static AlignBuilder SelfEnd => new("self-end", "");
    /// <summary>
    /// `self-center` — center this item on the cross axis (`align-self: center`).
    /// </summary>
    public static AlignBuilder SelfCenter => new("self-center", "");
    /// <summary>
    /// `self-stretch` — stretch this item (`align-self: stretch`).
    /// </summary>
    public static AlignBuilder SelfStretch => new("self-stretch", "");
    /// <summary>
    /// `self-baseline` — baseline alignment (`align-self: baseline`).
    /// </summary>
    public static AlignBuilder SelfBaseline => new("self-baseline", "");

    /// <summary>
    /// `justify-items-start` — grid items align to column start.
    /// </summary>
    public static AlignBuilder JustifyItemsStart => new("justify-items-start", "");
    /// <summary>
    /// `justify-items-end` — grid items align to column end.
    /// </summary>
    public static AlignBuilder JustifyItemsEnd => new("justify-items-end", "");
    /// <summary>
    /// `justify-items-center` — grid items centered in their cell.
    /// </summary>
    public static AlignBuilder JustifyItemsCenter => new("justify-items-center", "");
    /// <summary>
    /// `justify-items-stretch` — items stretch to fill the cell.
    /// </summary>
    public static AlignBuilder JustifyItemsStretch => new("justify-items-stretch", "");

    /// <summary>
    /// `justify-self-auto` — default grid self-alignment.
    /// </summary>
    public static AlignBuilder JustifySelfAuto => new("justify-self-auto", "");
    /// <summary>
    /// `justify-self-start` — align this grid item to the start of its area.
    /// </summary>
    public static AlignBuilder JustifySelfStart => new("justify-self-start", "");
    /// <summary>
    /// `justify-self-end` — align to the end of its area.
    /// </summary>
    public static AlignBuilder JustifySelfEnd => new("justify-self-end", "");
    /// <summary>
    /// `justify-self-center` — center within its area.
    /// </summary>
    public static AlignBuilder JustifySelfCenter => new("justify-self-center", "");
    /// <summary>
    /// `justify-self-stretch` — stretch across the area.
    /// </summary>
    public static AlignBuilder JustifySelfStretch => new("justify-self-stretch", "");
}
