using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>Maintains fieldset container-query layout across browser resize and content updates.</summary>
public interface IFieldLayoutInterop : IAsyncDisposable
{
    /// <summary>Observes a fieldset and repairs collapsed query-container layout when necessary.</summary>
    ValueTask Observe(ElementReference fieldset, CancellationToken cancellationToken = default);

    /// <summary>Removes observers owned by the fieldset.</summary>
    ValueTask Unobserve(ElementReference fieldset, CancellationToken cancellationToken = default);
}
