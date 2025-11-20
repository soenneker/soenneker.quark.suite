namespace Soenneker.Quark;

/// <summary>
/// Base badge selector.
/// </summary>
public sealed class BootstrapBadgeCssVariables : BootstrapBaseBadgeCssVariables
{
    public override string GetSelector()
    {
        return ".badge";
    }
}


