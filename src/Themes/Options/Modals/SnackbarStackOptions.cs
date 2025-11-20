namespace Soenneker.Quark;

/// <summary>
/// Configuration options for snackbar stack components.
/// </summary>
public sealed class SnackbarStackOptions : ComponentOptions
{
    public SnackbarStackOptions()
    {
    }

    /// <summary>
    /// Gets or sets the default location where snackbars should appear on the screen.
    /// </summary>
    public SnackbarLocation? DefaultLocation { get; set; }

    /// <summary>
    /// Gets or sets the default delay in milliseconds before snackbars automatically hide.
    /// </summary>
    public int? DefaultDelay { get; set; }
}
