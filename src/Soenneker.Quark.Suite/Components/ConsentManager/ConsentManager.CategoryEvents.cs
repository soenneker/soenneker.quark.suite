using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Utils.Ids;

namespace Soenneker.Quark;

public partial class ConsentManager
{
    private sealed class CategoryEvents : IHandleEvent
    {
        private readonly ConsentManager _owner;

        internal CategoryEvents(ConsentManager owner, string category)
        {
            _owner = owner;
            Category = category;
            Id = BlazorIdGenerator.Child(owner._componentId, category);
            Changed = EventCallback.Factory.Create<bool>(this, SetSelected);
        }

        internal string Category { get; }
        internal string Id { get; }
        internal EventCallback<bool> Changed { get; }

        private Task SetSelected(bool value) => _owner.SetSelected(Category, value);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
