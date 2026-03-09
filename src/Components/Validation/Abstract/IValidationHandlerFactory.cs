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
    IValidationHandler Create(Type type);
}
