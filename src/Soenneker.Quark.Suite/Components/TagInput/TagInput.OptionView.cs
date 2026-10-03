using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class TagInput
{
    private sealed class OptionView : IHandleEvent
    {
        private readonly TagInput _owner;
        internal string Option { get; }
        internal Func<Task> Click { get; }
        internal OptionView(TagInput owner, string option)
        {
            _owner = owner;
            Option = option;
            Click = HandleClick;
        }
        private Task HandleClick() => _owner.AddAutocompleteTag(Option);
        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
