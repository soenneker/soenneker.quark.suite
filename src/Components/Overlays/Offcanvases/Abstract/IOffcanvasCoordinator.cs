using System;

namespace Soenneker.Quark;

/// <summary>
/// Interface for coordinating offcanvas component state and lifecycle.
/// </summary>
public interface IOffcanvasCoordinator
{
    /// <summary>
    /// Gets the number of currently active offcanvas components.
    /// </summary>
    int ActiveCount { get; }
    /// <summary>
    /// Event raised when the offcanvas state changes.
    /// </summary>
    event Action? StateChanged;

    /// <summary>
    /// Registers an offcanvas component with the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier for the offcanvas component.</param>
    void Register(string id);
    /// <summary>
    /// Unregisters an offcanvas component with the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier for the offcanvas component.</param>
    void Unregister(string id);
    /// <summary>
    /// Marks an offcanvas component as entering (becoming active).
    /// </summary>
    /// <param name="id">The unique identifier for the offcanvas component.</param>
    void Enter(string id);
    /// <summary>
    /// Marks an offcanvas component as exiting (becoming inactive).
    /// </summary>
    /// <param name="id">The unique identifier for the offcanvas component.</param>
    void Exit(string id);
}


