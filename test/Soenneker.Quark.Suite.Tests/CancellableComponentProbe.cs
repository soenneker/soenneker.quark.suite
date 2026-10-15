using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class CancellableComponentProbe(CancellationToken linkedToken = default) : CancellableComponent(linkedToken);
