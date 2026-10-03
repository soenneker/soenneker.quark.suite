using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Dtos;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public partial class NavigationLinks
{
    private sealed class StackItemView : IHandleEvent
    {
        private readonly NavigationLinks _owner;
        internal NavigationItem Item { get; }
        internal bool IsRelativeNavigation { get; }
        internal Func<Task> Click { get; }

        internal StackItemView(NavigationLinks owner, NavigationItem item)
        {
            _owner = owner;
            Item = item;
            IsRelativeNavigation = item.Href.HasContent() && !item.Href!.StartsWith('#') &&
                !Uri.TryCreate(item.Href, UriKind.Absolute, out _);
            Click = HandleClick;
        }

        private Task HandleClick() => _owner.HandleStackItemClick(Item);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
