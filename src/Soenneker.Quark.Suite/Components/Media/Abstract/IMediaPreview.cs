namespace Soenneker.Quark;

/// <summary>Displays byte-backed media and optionally offers the original bytes for download.</summary>
public interface IMediaPreview
{
    /// <summary>The file contents. Supply a new array when the contents change.</summary>
    byte[]? Bytes { get; set; }

    /// <summary>The media MIME type. Unsupported types are offered as binary downloads only.</summary>
    string? ContentType { get; set; }

    /// <summary>The accessible media label and suggested download filename.</summary>
    string FileName { get; set; }

    /// <summary>Whether to display a download link, including for unsupported formats.</summary>
    bool ShowDownload { get; set; }
}
