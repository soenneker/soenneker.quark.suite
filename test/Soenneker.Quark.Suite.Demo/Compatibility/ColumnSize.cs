namespace Soenneker.Quark;

/// <summary>
/// Demo-only compatibility shim while demo pages migrate to Grid utilities.
/// </summary>
public static class ColumnSize
{
    public static GridBuilder Is1 => Grid.ColSpan(1);
    public static GridBuilder Is2 => Grid.ColSpan(2);
    public static GridBuilder Is3 => Grid.ColSpan(3);
    public static GridBuilder Is4 => Grid.ColSpan(4);
    public static GridBuilder Is5 => Grid.ColSpan(5);
    public static GridBuilder Is6 => Grid.ColSpan(6);
    public static GridBuilder Is7 => Grid.ColSpan(7);
    public static GridBuilder Is8 => Grid.ColSpan(8);
    public static GridBuilder Is9 => Grid.ColSpan(9);
    public static GridBuilder Is10 => Grid.ColSpan(10);
    public static GridBuilder Is11 => Grid.ColSpan(11);
    public static GridBuilder Is12 => Grid.ColSpan(12);
    public static GridBuilder Auto => Grid.ColSpan(12);
}
