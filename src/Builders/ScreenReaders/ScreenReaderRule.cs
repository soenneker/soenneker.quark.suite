using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.ScreenReaders;

/// <summary>
/// Represents a single screen reader rule with optional BreakpointType.
/// </summary>
internal record ScreenReaderRule(string Type, BreakpointType? breakpoint = null);

