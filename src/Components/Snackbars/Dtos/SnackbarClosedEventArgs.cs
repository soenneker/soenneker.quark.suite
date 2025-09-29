

namespace Soenneker.Quark;

/// <summary>
/// Provides data for the snackbar closed event.
/// </summary>
public class SnackbarClosedEventArgs
{
    /// <summary>
    /// Gets the snackbar key.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Gets the close reason.
    /// </summary>
    public SnackbarCloseReason CloseReason { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SnackbarClosedEventArgs"/> class.
    /// </summary>
    /// <param name="key">The snackbar key.</param>
    /// <param name="closeReason">The close reason.</param>
    public SnackbarClosedEventArgs(string key, SnackbarCloseReason closeReason)
    {
        Key = key;
        CloseReason = closeReason;
    }
}

