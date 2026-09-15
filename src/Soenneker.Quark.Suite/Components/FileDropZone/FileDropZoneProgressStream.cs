using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class FileDropZoneProgressStream(Stream inner, FileDropZoneUploadRequest request) : Stream
{
    private long _loadedBytes;
    private int? _lastPercent;
    private bool _reported;

    public override bool CanRead => inner.CanRead;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => inner.Length;
    public override long Position
    {
        get => _loadedBytes;
        set => throw new NotSupportedException();
    }

    public override void Flush() => throw new NotSupportedException();
    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException("Use asynchronous reads for browser streams.");
    public override int Read(Span<byte> buffer) => throw new NotSupportedException("Use asynchronous reads for browser streams.");
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
        ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();

    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        int bytesRead = await inner.ReadAsync(buffer, cancellationToken);
        if (bytesRead == 0)
            return 0;

        _loadedBytes += bytesRead;
        long totalBytes = request.BrowserFile.Size;
        int? percent = totalBytes > 0 ? (int)Math.Min(100m, (decimal)_loadedBytes * 100 / totalBytes) : null;
        if (!_reported || percent != _lastPercent)
        {
            await request.ReportUploadProgress(percent);
            _lastPercent = percent;
            _reported = true;
        }

        return bytesRead;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            inner.Dispose();
        base.Dispose(disposing);
    }

    public override async ValueTask DisposeAsync()
    {
        await inner.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
