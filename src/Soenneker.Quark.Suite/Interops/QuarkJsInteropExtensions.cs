using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

internal static class QuarkJsInteropExtensions
{
    internal static ValueTask InvokeQuarkVoidAsync(this IJSObjectReference module, string identifier, params object?[]? args) =>
        module.InvokeQuarkVoidAsync(identifier, CancellationToken.None, args);

    internal static async ValueTask InvokeQuarkVoidAsync(this IJSObjectReference module, string identifier, CancellationToken cancellationToken,
        params object?[]? args)
    {
        try
        {
            await module.InvokeVoidAsync(identifier, cancellationToken, args);
        }
        catch (JSException exception)
        {
            // Preserve lifecycle exceptions for the caller's disconnect/cancellation handling.
            // Real script failures must remain failures, with the operation and original stack available.
            throw new JSException($"Quark JavaScript operation '{identifier}' failed: {exception.Message}", exception);
        }
    }

    internal static ValueTask<T> InvokeQuarkAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] T>(this IJSObjectReference module, string identifier, params object?[]? args) =>
        module.InvokeQuarkAsync<T>(identifier, CancellationToken.None, args);

    internal static async ValueTask<T> InvokeQuarkAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] T>(this IJSObjectReference module, string identifier, CancellationToken cancellationToken,
        params object?[]? args)
    {
        try
        {
            return await module.InvokeAsync<T>(identifier, cancellationToken, args);
        }
        catch (JSException exception)
        {
            throw new JSException($"Quark JavaScript operation '{identifier}' failed: {exception.Message}", exception);
        }
    }
}
