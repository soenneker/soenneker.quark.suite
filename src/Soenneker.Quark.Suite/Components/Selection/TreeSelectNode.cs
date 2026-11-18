using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>A selectable node. Keys must be unique across all branches.</summary>
public sealed record TreeSelectNode(string Key, string Label, IReadOnlyList<TreeSelectNode>? Children = null);
