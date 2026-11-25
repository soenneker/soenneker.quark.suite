using System;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>Manages browser object URLs for byte-backed media.</summary>
public interface IMediaPreviewInterop : IAsyncDisposable
{
    /// <summary>Creates an object URL with the specified MIME type.</summary>
    ValueTask<string> Create(byte[] bytes, string contentType);

    /// <summary>Releases a previously created object URL.</summary>
    ValueTask Release(string url);
}
