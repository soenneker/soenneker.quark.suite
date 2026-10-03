using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Soenneker.Quark;

public partial class FileDropZone
{
    private sealed class InputEvents : IHandleEvent
    {
        private readonly FileDropZone _owner;
        private readonly string _key;

        internal InputEvents(FileDropZone owner, string key)
        {
            _owner = owner;
            _key = key;
            Changed = EventCallback.Factory.Create<InputFileChangeEventArgs>(this, Select);
        }

        internal EventCallback<InputFileChangeEventArgs> Changed { get; }
        private Task Select(InputFileChangeEventArgs args) => _owner.SelectFiles(args, _key);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
