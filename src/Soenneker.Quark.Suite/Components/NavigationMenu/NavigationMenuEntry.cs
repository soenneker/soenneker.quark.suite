using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class NavigationMenuEntry
{
    public required NavigationMenuItem Item { get; init; }

    public NavigationMenuTrigger? Trigger { get; set; }

    public bool IsDisabled => Item.Disabled;

    public Task FocusTriggerAsync()
    {
        return Trigger?.FocusAsync() ?? Task.CompletedTask;
    }
}
