using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// JS interop for accordion measurement and animation support.
/// </summary>
public interface IAccordionInterop : IAsyncDisposable
{
    ValueTask Initialize(CancellationToken cancellationToken = default);

    ValueTask ObserveContentHeight(ElementReference container, ElementReference content, CancellationToken cancellationToken = default);

    ValueTask StopObservingContentHeight(ElementReference container, CancellationToken cancellationToken = default);
}
