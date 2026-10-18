using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Quark;
/// <summary>Persistence for named views. The default stores JSON in browser local storage; replace it for account-backed storage.</summary>
public interface IQuarkViewStore
{
    /// <summary>Reads a view collection for an application-scoped key.</summary>
    ValueTask<IReadOnlyList<QuarkSavedView>> Load(string key, CancellationToken cancellationToken = default);
    /// <summary>Replaces the stored collection. Callers should scope keys by account and screen.</summary>
    ValueTask Save(string key, IReadOnlyList<QuarkSavedView> views, CancellationToken cancellationToken = default);
}
