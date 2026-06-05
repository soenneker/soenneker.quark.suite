using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the prompt input message record.
/// </summary>
public sealed record PromptInputMessage
{
    /// <summary>
    /// Gets or sets text.
    /// </summary>
    public string? Text { get; init; }

    /// <summary>
    /// Gets or sets files.
    /// </summary>
    public IReadOnlyList<PromptInputAttachmentInfo> Files { get; init; } = Array.Empty<PromptInputAttachmentInfo>();
}
