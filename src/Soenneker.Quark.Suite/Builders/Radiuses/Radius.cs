namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned radius utility.
/// </summary>
public static class Radius
{
    public static RadiusBuilder Default => new RadiusBuilder().Default;
    public static RadiusBuilder None => new RadiusBuilder().None;
    public static RadiusBuilder Sm => new RadiusBuilder().Sm;
    public static RadiusBuilder Md => new RadiusBuilder().Md;
    public static RadiusBuilder Lg => new RadiusBuilder().Lg;
    public static RadiusBuilder Xl => new RadiusBuilder().Xl;
    public static RadiusBuilder TwoXl => new RadiusBuilder().TwoXl;
    public static RadiusBuilder ThreeXl => new RadiusBuilder().ThreeXl;
    public static RadiusBuilder Full => new RadiusBuilder().Full;

    public static RadiusBuilder All => new RadiusBuilder().All;
    public static RadiusBuilder Top => new RadiusBuilder().Top;
    public static RadiusBuilder Bottom => new RadiusBuilder().Bottom;
    public static RadiusBuilder Left => new RadiusBuilder().Left;
    public static RadiusBuilder Right => new RadiusBuilder().Right;

    public static RadiusBuilder TopLeft => new RadiusBuilder().TopLeft;
    public static RadiusBuilder TopRight => new RadiusBuilder().TopRight;
    public static RadiusBuilder BottomLeft => new RadiusBuilder().BottomLeft;
    public static RadiusBuilder BottomRight => new RadiusBuilder().BottomRight;

    public static RadiusBuilder OnBase => new RadiusBuilder().OnBase;
    public static RadiusBuilder OnSm => new RadiusBuilder().OnSm;
    public static RadiusBuilder OnMd => new RadiusBuilder().OnMd;
    public static RadiusBuilder OnLg => new RadiusBuilder().OnLg;
    public static RadiusBuilder OnXl => new RadiusBuilder().OnXl;
    public static RadiusBuilder On2xl => new RadiusBuilder().On2xl;

    public static RadiusBuilder Token(string value) => new RadiusBuilder().Token(value);
}