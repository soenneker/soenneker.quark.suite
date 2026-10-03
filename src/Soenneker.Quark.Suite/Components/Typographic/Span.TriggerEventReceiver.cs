using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

public partial class Span
{
    private sealed class TriggerEventReceiver(Span owner) : IHandleEvent
    {
        private object? _mouseEnterCallback;
        internal object MouseEnterCallback => _mouseEnterCallback ??= EventCallback.Factory.Create<MouseEventArgs>(this, HandleMouseEnter);

        private object? _mouseMoveCallback;
        internal object MouseMoveCallback => _mouseMoveCallback ??= EventCallback.Factory.Create<MouseEventArgs>(this, HandleMouseMove);

        private object? _mouseLeaveCallback;
        internal object MouseLeaveCallback => _mouseLeaveCallback ??= EventCallback.Factory.Create<MouseEventArgs>(this, HandleMouseLeave);

        private object? _pointerDownCallback;
        internal object PointerDownCallback => _pointerDownCallback ??= EventCallback.Factory.Create<PointerEventArgs>(this, HandlePointerDown);

        private object? _focusInCallback;
        internal object FocusInCallback => _focusInCallback ??= EventCallback.Factory.Create<FocusEventArgs>(this, HandleFocusIn);

        private object? _focusOutCallback;
        internal object FocusOutCallback => _focusOutCallback ??= EventCallback.Factory.Create<FocusEventArgs>(this, HandleFocusOut);

        private object? _keyDownCallback;
        internal object KeyDownCallback => _keyDownCallback ??= EventCallback.Factory.Create<KeyboardEventArgs>(this, HandleKeyDown);

        public Task HandleMouseEnter(MouseEventArgs args) => owner.HandleMouseEnter(args);
        public Task HandleMouseMove(MouseEventArgs args) => owner.HandleMouseMove(args);
        public Task HandleMouseLeave(MouseEventArgs args) => owner.HandleMouseLeave(args);
        public Task HandlePointerDown(PointerEventArgs args) => owner.HandlePointerDown(args);
        public Task HandleFocusIn(FocusEventArgs args) => owner.HandleFocusIn(args);
        public Task HandleFocusOut(FocusEventArgs args) => owner.HandleFocusOut(args);
        public Task HandleKeyDown(KeyboardEventArgs args) => owner.HandleKeyDown(args);
        public async Task HandleEventAsync(EventCallbackWorkItem callback, object? argument)
        {
            Task? callbackTask = null;
            try
            {
                callbackTask = callback.InvokeAsync(argument);
                await callbackTask;
            }
            catch (Exception exception) when (callbackTask?.IsCanceled != true)
            {
                await owner.DispatchExceptionAsync(exception);
            }
        }
    }

}
