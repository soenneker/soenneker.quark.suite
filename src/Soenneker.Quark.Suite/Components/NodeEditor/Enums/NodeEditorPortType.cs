using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines whether a node port begins or accepts a connection.
/// </summary>
[EnumValue<string>]
public sealed partial class NodeEditorPortType
{
    /// <summary>
    /// Identifies a port that begins outgoing connections.
    /// </summary>
    public static readonly NodeEditorPortType Source = new("source");

    /// <summary>
    /// Identifies a port that accepts incoming connections.
    /// </summary>
    public static readonly NodeEditorPortType Target = new("target");
}
