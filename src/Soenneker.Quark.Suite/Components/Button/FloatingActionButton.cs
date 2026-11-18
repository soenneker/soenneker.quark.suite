namespace Soenneker.Quark;

/// <summary>A round, viewport-positioned Quark button. Use AriaLabel for icon-only content.
/// Override Bottom and Right to place it above a bottom navigation bar or another action.</summary>
public sealed class FloatingActionButton : Button
{
    protected override void ApplyDefaultParameters()
    {
        DataSlot ??= "floating-action-button";
        Position ??= Quark.Position.Fixed;
        Bottom ??= Quark.Bottom.Token("[calc(1rem+env(safe-area-inset-bottom))]");
        Right ??= Quark.Right.Is4;
        ZIndex ??= Quark.ZIndex.Is50;
        Rounded ??= Quark.Rounded.Full;
        Size ??= Quark.Size.Is12;
        Shadow ??= Quark.Shadow.Md;
        base.ApplyDefaultParameters();
    }
}
