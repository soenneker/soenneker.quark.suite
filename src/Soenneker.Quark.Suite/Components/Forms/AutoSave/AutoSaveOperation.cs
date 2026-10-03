using System.Threading;

namespace Soenneker.Quark;

internal readonly record struct AutoSaveOperation(int Version, CancellationToken CancellationToken);
