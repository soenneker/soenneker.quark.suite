using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class ModelSelector
{
    private sealed class OptionView : IHandleEvent
    {
        private readonly ModelSelector _owner;
        internal ModelSelectorOption Option { get; }
        internal Func<Task> Select { get; }

        internal OptionView(ModelSelector owner, ModelSelectorOption option)
        {
            _owner = owner;
            Option = option;
            Select = SelectOption;
        }

        private Task SelectOption() => _owner.SelectOption(Option);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
