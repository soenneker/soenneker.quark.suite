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
    /// <param name="collapse">Collapse for the register operation.</param>
    /// <returns>A task that completes when callback registration is finished.</returns>
    ValueTask Register(Collapse collapse);

    /// <summary>
    /// Unregisters a collapse instance.
    /// </summary>
    /// <param name="collapse">Collapse for the unregister operation.</param>
    /// <returns>A task that completes when the unregister operation is complete.</returns>
    ValueTask Unregister(Collapse collapse);

    /// <summary>
    /// Toggles one or many collapse targets from an expression (id, #id, .class, or space/comma-delimited ids).
    /// </summary>
    /// <param name="targetExpression">Target Expression for the toggle targets operation.</param>
    /// <returns>A task that completes when the toggle targets operation is complete.</returns>
    ValueTask ToggleTargets(string? targetExpression);
}
