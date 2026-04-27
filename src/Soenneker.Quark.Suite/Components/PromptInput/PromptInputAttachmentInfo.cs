using System;

namespace Soenneker.Quark;

public sealed record PromptInputAttachmentInfo
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N");

    public string Name { get; init; } = string.Empty;

    public long Size { get; init; }

    public string? Type { get; init; }

    public long LastModified { get; init; }

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
