using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

public sealed record PromptInputMessage
{
    public string? Text { get; init; }

    public IReadOnlyList<PromptInputAttachmentInfo> Files { get; init; } = Array.Empty<PromptInputAttachmentInfo>();
}
