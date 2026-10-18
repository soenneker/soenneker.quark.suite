using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>Saves and restores view snapshots. Browser persistence is opt-in through StorageKey.</summary>
public interface ISavedViews : IElement
{
    /// <summary>Optional current field schema used when restoring views.</summary>
    IReadOnlyList<FilterField>? Fields { get; set; }
    /// <summary>Old field keys mapped to their current names.</summary>
    IReadOnlyDictionary<string, string>? FieldAliases { get; set; }
    /// <summary>The current application view.</summary>
    QuarkViewState State { get; set; }
    /// <summary>Receives an independent snapshot when a saved view is selected.</summary>
    EventCallback<QuarkViewState> StateChanged { get; set; }
    /// <summary>Available named views; application-owned when StorageKey is null.</summary>
    IReadOnlyList<QuarkSavedView> Views { get; set; }
    /// <summary>Receives updated named views after successful persistence.</summary>
    EventCallback<IReadOnlyList<QuarkSavedView>> ViewsChanged { get; set; }
    /// <summary>Optional persistence key, scoped by account and screen. Loaded after interactive rendering.</summary>
    string? StorageKey { get; set; }
}
