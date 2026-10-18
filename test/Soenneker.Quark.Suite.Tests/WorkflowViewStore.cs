using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Quark.Suite.Tests;
public sealed class WorkflowViewStore : IQuarkViewStore
{
    public bool Fail { get; set; }
    public IReadOnlyList<QuarkSavedView> Views { get; private set; } = [];
    public ValueTask<IReadOnlyList<QuarkSavedView>> Load(string key, CancellationToken cancellationToken = default) => ValueTask.FromResult(Views);
    public ValueTask Save(string key, IReadOnlyList<QuarkSavedView> views, CancellationToken cancellationToken = default)
    { if (Fail) throw new InvalidOperationException("offline"); Views = views; return ValueTask.CompletedTask; }
}
