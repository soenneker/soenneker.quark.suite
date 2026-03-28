using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Coordinates interactions between collapse triggers and <see cref="Collapse"/> components.
/// </summary>
public interface ICollapseCoordinator
{
    /// <summary>
    /// Registers a collapse instance for target-based lookup.
    /// </summary>
    ValueTask Register(Collapse collapse);

    /// <summary>
    /// Unregisters a collapse instance.
    /// </summary>
    ValueTask Unregister(Collapse collapse);

    /// <summary>
    /// Toggles one or many collapse targets from an expression (id, #id, .class, or space/comma-delimited ids).
    /// </summary>
    ValueTask ToggleTargets(string? targetExpression);
}
