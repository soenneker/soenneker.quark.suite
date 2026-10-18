using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
namespace Soenneker.Quark;
/// <summary>An HTML rich-text editor with a restricted browser rendering allowlist. Validate and sanitize content on the server before publishing it elsewhere.</summary>
public interface IRichTextEditor : IElement
{
    /// <summary>Controlled HTML content. Unsupported tags and attributes are removed when rendered.</summary>
    string Value { get; set; }
    /// <summary>Receives sanitized HTML after edits.</summary>
    EventCallback<string> ValueChanged { get; set; }
    /// <summary>Renders sanitized content without the editing toolbar.</summary>
    bool ReadOnly { get; set; }
    /// <summary>Available mention keys and display names.</summary>
    IReadOnlyList<SelectOption> Mentions { get; set; }
    /// <summary>Optional application-owned attachment upload. Enforce size and content policy in this callback and on the server.</summary>
    Func<IBrowserFile, CancellationToken, ValueTask<RichTextAttachment>>? UploadAttachment { get; set; }
}
