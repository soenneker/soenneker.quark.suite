using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class FileDropZone
{
    private sealed class FileView : IHandleEvent
    {
        private readonly FileDropZone _owner;
        private readonly string _id;
        private FileDropZoneFile? _file;
        private CultureInfo? _culture;
        private string? _cancelLabel, _deleteLabel, _retryLabel, _reorderLabel, _retryDeleteLabel, _progressLabel;
        private string? _status, _size;

        internal FileView(FileDropZone owner, string id)
        {
            _owner = owner;
            _id = id;
            Cancel = CancelFile;
            Delete = DeleteFile;
            Retry = RetryFile;
        }

        internal int Generation { get; set; }
        internal Action Cancel { get; }
        internal Func<Task> Delete { get; }
        internal Func<Task> Retry { get; }

        internal void Update(FileDropZoneFile file)
        {
            var culture = CultureInfo.CurrentCulture;
            if (culture.IsReadOnly && ReferenceEquals(file, _file) && ReferenceEquals(culture, _culture))
                return;
            if (_file?.Name != file.Name)
                _cancelLabel = _deleteLabel = _retryLabel = _reorderLabel = _retryDeleteLabel = null;
            if (_file?.Size != file.Size || !culture.IsReadOnly || !ReferenceEquals(culture, _culture))
                _size = null;
            if (_file?.State != file.State || _file?.Progress != file.Progress || _file?.StatusText != file.StatusText ||
                !culture.IsReadOnly || !ReferenceEquals(culture, _culture))
                _status = null;
            _progressLabel = null;
            _file = file;
            _culture = culture;
        }

        internal string CancelLabel => _cancelLabel ??= $"Cancel upload of {_file!.Name}";
        internal string DeleteLabel => _deleteLabel ??= $"Delete {_file!.Name}";
        internal string RetryLabel => _retryLabel ??= $"Retry upload of {_file!.Name}";
        internal string ReorderLabel => _reorderLabel ??= $"Reorder {_file!.Name}";
        internal string RetryDeleteLabel => _retryDeleteLabel ??= $"Retry deleting {_file!.Name}";
        internal string ProgressLabel => _progressLabel ??= $"{_file!.Name}: {StatusText}";
        internal string StatusText => _status ??= Status(_file!);
        internal string? Size => _file!.Size is { } bytes ? _size ??= FormatSize(bytes) : null;

        private void CancelFile() => _owner.Cancel(_id);
        private Task DeleteFile() => _owner.DeleteFile(_id);
        private Task RetryFile() => _owner.UploadFile(_id);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
