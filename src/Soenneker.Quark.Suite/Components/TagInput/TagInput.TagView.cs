using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class TagInput
{
    private sealed class TagView : IHandleEvent
    {
        private readonly TagInput _owner;
        private readonly int _index;
        private int _truncateLength;
        private string? _truncated;
        internal string Tag { get; }
        internal string RemoveLabel { get; }
        internal Func<Task> Click { get; }
        internal Func<Task> Remove { get; }

        internal TagView(TagInput owner, string tag, int index)
        {
            _owner = owner;
            _index = index;
            Tag = tag;
            RemoveLabel = "Remove " + tag;
            Click = HandleClick;
            Remove = HandleRemove;
        }
        internal string GetText(bool truncate, int length)
        {
            if (!truncate || Tag.Length <= length) return Tag;
            if (_truncated is null || _truncateLength != length)
            {
                _truncated = string.Concat(Tag.AsSpan(0, Math.Max(0, length)), "...");
                _truncateLength = length;
            }
            return _truncated;
        }
        private Task HandleClick() => _owner.HandleTagClick(Tag, _index);
        private Task HandleRemove() => _owner.RemoveTagAt(_index, moveToPrevious: true);
        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
