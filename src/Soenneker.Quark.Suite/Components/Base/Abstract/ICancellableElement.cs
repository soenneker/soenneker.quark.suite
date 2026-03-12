namespace Soenneker.Quark;

/// <summary>
/// Represents an element with cancellable async operations support.
/// Combines <see cref="ICancellableComponent"/> functionality with <see cref="IElement"/> properties.
/// </summary>
public interface ICancellableElement : ICancellableComponent, IElement;