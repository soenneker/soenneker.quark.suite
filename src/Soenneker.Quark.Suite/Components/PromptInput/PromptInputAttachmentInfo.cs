using System;
using Soenneker.Blazor.Utils.Ids;

namespace Soenneker.Quark;

/// <summary>
/// Represents the prompt input attachment info record.
/// </summary>
public sealed record PromptInputAttachmentInfo
{
    /// <summary>
    /// Gets or sets id.
    /// </summary>
    public string Id { get; init; } = BlazorIdGenerator.New("prompt-input-attachment");

    /// <summary>
    /// Gets or sets name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets size.
    /// </summary>
    public long Size { get; init; }

    /// <summary>
    /// Gets or sets type.
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// Gets or sets last modified.
    /// </summary>
    public long LastModified { get; init; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    public string Description => !string.IsNullOrWhiteSpace(Type)
        ? $"{FormatSize(Size)} {Type}"
        : FormatSize(Size);

    private static string FormatSize(long bytes)
    {
        if (bytes < 1024)
            return $"{bytes} B";

        var kb = bytes / 1024d;
        if (kb < 1024)
            return $"{kb:0.#} KB";

        var mb = kb / 1024d;
        if (mb < 1024)
            return $"{mb:0.#} MB";

        return $"{mb / 1024d:0.#} GB";
    }
}
