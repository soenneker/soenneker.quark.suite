using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.ClipPaths;

/// <summary>
/// Represents a single clip path rule with optional BreakpointType.
/// </summary>
internal record ClipPathRule(string Path, BreakpointType? breakpoint = null);
