using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class CancellableLayoutProbe(CancellationToken linkedToken = default) : CancellableLayout(linkedToken);
