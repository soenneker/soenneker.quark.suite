
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for badge component styling.
/// </summary>
public sealed class BadgeOptions : ComponentOptions
{
    /// <summary>
    /// Initializes a new instance of the BadgeOptions class.
    /// </summary>
    public BadgeOptions()
    {
        Selector = ".badge, .badge.bg-primary, .badge.text-bg-primary, .badge.text-bg-secondary, .badge.text-bg-success, .badge.text-bg-danger, .badge.text-bg-warning, .badge.text-bg-info, .badge.text-bg-light, .badge.text-bg-dark";
    }
}
