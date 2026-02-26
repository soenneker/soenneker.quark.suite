using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents Bootstrap alert types.
/// </summary>
[EnumValue<string>]
public sealed partial class AlertType
{
    /// <summary>
    /// Primary alert type.
    /// </summary>
    public static readonly AlertType Primary = new("primary");

    /// <summary>
    /// Secondary alert type.
    /// </summary>
    public static readonly AlertType Secondary = new("secondary");

    /// <summary>
    /// Success alert type.
    /// </summary>
    public static readonly AlertType Success = new("success");

    /// <summary>
    /// Danger alert type.
    /// </summary>
    public static readonly AlertType Danger = new("danger");

    /// <summary>
    /// Warning alert type.
    /// </summary>
    public static readonly AlertType Warning = new("warning");

    /// <summary>
    /// Info alert type.
    /// </summary>
    public static readonly AlertType Info = new("info");

    /// <summary>
    /// Light alert type.
    /// </summary>
    public static readonly AlertType Light = new("light");

    /// <summary>
    /// Dark alert type.
    /// </summary>
    public static readonly AlertType Dark = new("dark");
}
