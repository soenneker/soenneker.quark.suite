using System;

namespace Soenneker.Quark;

/// <summary>
/// Factory for constructing concrete validation handlers by type.
/// </summary>
public interface IValidationHandlerFactory
{
    /// <summary>
    /// Create a handler instance for the specified handler type.
    /// </summary>
    /// <param name="type">Runtime type to inspect or construct.</param>
    /// <returns>The resulting validation Handler.</returns>
    IValidationHandler Create(Type type);
}
