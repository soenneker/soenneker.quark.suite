using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public interface ITreeViewInterop : IAsyncDisposable
{
    ValueTask Initialize(CancellationToken cancellationToken = default);
}