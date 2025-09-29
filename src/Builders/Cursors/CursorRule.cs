using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Cursors;

/// <summary>
/// Represents a single cursor rule with optional BreakpointType.
/// </summary>
internal record CursorRule(string Cursor, BreakpointType? breakpoint = null);
