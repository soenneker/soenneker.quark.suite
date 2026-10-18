using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace Soenneker.Quark;
/// <summary>Owns the browser editor and sanitization boundary. Only a limited formatting allowlist and safe links are rendered.</summary>
public interface IRichTextEditorInterop
{
    /// <summary>Registers input and selection tracking for an editor.</summary>
    ValueTask Mount(ElementReference element, DotNetObjectReference<RichTextEditor> callback);
    /// <summary>Applies sanitized external content and editability, preserving the selection when content is unchanged.</summary>
    ValueTask SetContent(ElementReference element, string html, bool readOnly);
    /// <summary>Applies a formatting command or inserts a link, mention, or attachment at the saved selection.</summary>
    ValueTask Execute(ElementReference element, string command, string? value = null, string? label = null);
    /// <summary>Removes browser event handlers.</summary>
    ValueTask Unmount(ElementReference element);
}
