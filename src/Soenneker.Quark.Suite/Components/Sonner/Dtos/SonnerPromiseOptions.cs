using System;

namespace Soenneker.Quark;

/// <summary>
/// Represents the sonner promise options.
/// </summary>
public sealed class SonnerPromiseOptions
{
    /// <summary>
    /// Gets or sets loading.
    /// </summary>
    public string Loading { get; set; } = "Loading...";

    /// <summary>
    /// Gets or sets success.
    /// </summary>
    public string Success { get; set; } = "Done";

    /// <summary>
    /// Gets or sets error.
    /// </summary>
    public string Error { get; set; } = "Something went wrong";

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets success description.
    /// </summary>
    public string? SuccessDescription { get; set; }

    /// <summary>
    /// Gets or sets error description.
    /// </summary>
    public string? ErrorDescription { get; set; }

    /// <summary>
    /// Gets or sets configure loading.
    /// </summary>
    public Action<SonnerToastOptions>? ConfigureLoading { get; set; }

    /// <summary>
    /// Gets or sets configure success.
    /// </summary>
    public Action<SonnerToastOptions>? ConfigureSuccess { get; set; }

    /// <summary>
    /// Gets or sets configure error.
    /// </summary>
    public Action<SonnerToastOptions>? ConfigureError { get; set; }
}
