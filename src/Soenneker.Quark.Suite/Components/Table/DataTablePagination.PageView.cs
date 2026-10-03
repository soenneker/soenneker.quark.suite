using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

public partial class DataTablePagination
{
    private sealed class PageView : IHandleEvent
    {
        private readonly DataTablePagination _owner;
        internal int Number { get; }
        internal string Text { get; }
        internal Func<MouseEventArgs, Task> Click { get; }

        internal PageView(DataTablePagination owner, int number)
        {
            _owner = owner;
            Number = number;
            Text = number.ToString(CultureInfo.InvariantCulture);
            Click = HandleClick;
        }

        private Task HandleClick(MouseEventArgs _) => _owner.HandleGoToPage(Number);
        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
