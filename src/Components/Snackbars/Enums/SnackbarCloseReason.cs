using Intellenum;

namespace Soenneker.Quark;

/// <summary>
/// Specifies the reason that a snackbar was closed.
/// </summary>
[Intellenum<string>]
public sealed partial class SnackbarCloseReason
{
    /// <summary>
    /// Snackbar is closed automatically by internal timer or by other unknown reason.
    /// </summary>
    public static readonly SnackbarCloseReason None = new("none");

    /// <summary>
    /// Snackbar is closed by the user.
    /// </summary>
    public static readonly SnackbarCloseReason UserClosed = new("user");
}
