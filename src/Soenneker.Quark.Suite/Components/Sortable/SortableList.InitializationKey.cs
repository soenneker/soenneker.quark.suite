namespace Soenneker.Quark;

public partial class SortableList
{
    private readonly record struct InitializationKey(bool Disabled, bool Sort, int AnimationMs, bool ForceFallback,
        string ItemSelector, string? HandleSelector, string? FilterSelector, string? Group, string ListId, bool NotifyOnReorder);
}
