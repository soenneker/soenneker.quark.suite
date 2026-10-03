using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class Sonner
{
    private sealed class ToastView : IHandleEvent
    {
        private readonly Sonner _owner;
        private Func<Task>? _action;
        private Func<Task>? _dismiss;
        private (int Count, int Index, long Offset, long Height)? _styleKey;
        private string? _style;
        internal SonnerToast Toast { get; }
        internal QuarkInt32Attribute IndexAttribute;
        internal Func<Task> Action => _action ??= HandleAction;
        internal Func<Task> Dismiss => _dismiss ??= HandleDismiss;

        internal ToastView(Sonner owner, SonnerToast toast)
        {
            _owner = owner;
            Toast = toast;
        }

        internal string GetStyle(int count, int index, double offset, double height)
        {
            var key = (count, index, BitConverter.DoubleToInt64Bits(offset), BitConverter.DoubleToInt64Bits(height));
            if (_style is null || _styleKey != key)
            {
                _style = GetToastStyle(count, index, offset, height);
                _styleKey = key;
            }
            return _style;
        }

        private Task HandleAction() => _owner.HandleAction(Toast);
        private Task HandleDismiss() => _owner.HandleDismiss(Toast.Id);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
