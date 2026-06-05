using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents the sonner toast type.
/// </summary>
[EnumValue]
public sealed partial class SonnerToastType
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly SonnerToastType Default = new(0);
    /// <summary>
    /// The success.
    /// </summary>
    public static readonly SonnerToastType Success = new(1);
    /// <summary>
    /// The info.
    /// </summary>
    public static readonly SonnerToastType Info = new(2);
    /// <summary>
    /// The warning.
    /// </summary>
    public static readonly SonnerToastType Warning = new(3);
    /// <summary>
    /// The error.
    /// </summary>
    public static readonly SonnerToastType Error = new(4);
    /// <summary>
    /// The loading.
    /// </summary>
    public static readonly SonnerToastType Loading = new(5);
}
