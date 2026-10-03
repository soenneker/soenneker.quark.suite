using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

public partial class Button
{
    // These handlers forward to the overlay owner. Pointer bookkeeping has no visual
    // state of its own; the owner's cascade delivers actual open/focus state changes.
    private sealed class TriggerEventReceiver(Button owner) : IHandleEvent
    {
        private object? _mouseEnterCallback;
        public object MouseEnterCallback => _mouseEnterCallback ??= EventCallback.Factory.Create<MouseEventArgs>(this, HandleTriggerMouseEnter);

        private object? _mouseMoveCallback;
        public object MouseMoveCallback => _mouseMoveCallback ??= EventCallback.Factory.Create<MouseEventArgs>(this, HandleTriggerMouseMove);

        private object? _mouseLeaveCallback;
        public object MouseLeaveCallback => _mouseLeaveCallback ??= EventCallback.Factory.Create<MouseEventArgs>(this, HandleTriggerMouseLeave);

        private object? _pointerMoveCallback;
        public object PointerMoveCallback => _pointerMoveCallback ??= EventCallback.Factory.Create<PointerEventArgs>(this, HandleTriggerPointerMove);

        private object? _pointerLeaveCallback;
        public object PointerLeaveCallback => _pointerLeaveCallback ??= EventCallback.Factory.Create<PointerEventArgs>(this, HandleTriggerPointerLeave);

        private object? _pointerUpCallback;
        public object PointerUpCallback => _pointerUpCallback ??= EventCallback.Factory.Create<PointerEventArgs>(this, HandleTriggerPointerUp);

        private object? _focusInCallback;
        public object FocusInCallback => _focusInCallback ??= EventCallback.Factory.Create<FocusEventArgs>(this, HandleTriggerFocusIn);

        private object? _focusOutCallback;
        public object FocusOutCallback => _focusOutCallback ??= EventCallback.Factory.Create<FocusEventArgs>(this, HandleTriggerFocusOut);

        private Func<PointerEventArgs, Task>? _pointerDownHandler;
        public EventCallback<PointerEventArgs> PointerDownCallback => EventCallback.Factory.Create<PointerEventArgs>(this, _pointerDownHandler ??= HandleTriggerPointerDown);

        public Task HandleTriggerMouseEnter(MouseEventArgs args) => owner.HandleTriggerMouseEnter(args);
        public Task HandleTriggerMouseMove(MouseEventArgs args) => owner.HandleTriggerMouseMove(args);
        public Task HandleTriggerMouseLeave(MouseEventArgs args) => owner.HandleTriggerMouseLeave(args);
        public Task HandleTriggerPointerMove(PointerEventArgs args) => owner.HandleTriggerPointerMove(args);
        public Task HandleTriggerPointerLeave(PointerEventArgs args) => owner.HandleTriggerPointerLeave(args);
        public Task HandleTriggerPointerDown(PointerEventArgs args) => owner.HandleTriggerPointerDown(args);
        public Task HandleTriggerPointerUp(PointerEventArgs args) => owner.HandleTriggerPointerUp(args);
        public Task HandleTriggerFocusIn(FocusEventArgs args) => owner.HandleTriggerFocusIn(args);
        public Task HandleTriggerFocusOut(FocusEventArgs args) => owner.HandleTriggerFocusOut(args);

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
