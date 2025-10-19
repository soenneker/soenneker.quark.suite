
namespace Soenneker.Quark;

public sealed class StepsOptions : ComponentOptions
{
    public StepsOptions()
    {
        ThemeKey = "Steps";
    }

    public string? ConnectorColor { get; set; }
    public string? MarkerActiveBg { get; set; }
    public string? MarkerActiveBorder { get; set; }
    public string? MarkerActiveColor { get; set; }
    public string? TextActive { get; set; }
    public string? Success { get; set; }
    public string? DisabledBg { get; set; }
    public string? DisabledColor { get; set; }
    public string? ContentBg { get; set; }
    public string? ContentBorder { get; set; }
    public string? ContentRadius { get; set; }
    public string? ContentShadow { get; set; }
    public string? FocusOutline { get; set; }
    public string? MarkerSuccessColor { get; set; }
}

