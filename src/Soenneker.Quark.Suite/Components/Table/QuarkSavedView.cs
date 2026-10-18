namespace Soenneker.Quark;
/// <summary>A named snapshot of serializable view state.</summary>
public sealed record QuarkSavedView(string Id, string Name, QuarkViewState State);
