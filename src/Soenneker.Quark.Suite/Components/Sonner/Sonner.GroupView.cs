using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class Sonner
{
    private sealed class GroupView : IHandleEvent
    {
        private readonly Sonner _owner;
        private (long Height, string? Style)? _styleKey;
        private string? _style;
        internal SonnerPosition Position { get; }
        internal List<SonnerToast> Toasts { get; } = [];
        internal Func<Task> Enter { get; }
        internal Func<Task> Leave { get; }
        internal Func<Task> FocusIn { get; }
        internal Func<Task> FocusOut { get; }
        internal Func<Task> InteractStart { get; }
        internal Func<Task> InteractEnd { get; }

        internal GroupView(Sonner owner, SonnerPosition position)
        {
            _owner = owner;
            Position = position;
            Enter = HandleEnter;
            Leave = HandleLeave;
            FocusIn = HandleFocusIn;
            FocusOut = HandleFocusOut;
            InteractStart = HandleInteractStart;
            InteractEnd = HandleInteractEnd;
        }

        private Task HandleEnter() => _owner.SetExpanded(Position, true);

        private Task HandleLeave() => _owner.SetExpanded(Position, false);

        private Task HandleFocusIn() => _owner.SetFocused(Position, true);

        private Task HandleFocusOut() => _owner.SetFocused(Position, false);

        private Task HandleInteractStart() => _owner.SetInteracting(Position, true);

        private Task HandleInteractEnd() => _owner.SetInteracting(Position, false);

        internal string GetStyle()
        {
            var height = Toasts.Count > 0 ? _owner.GetToastHeight(Toasts[0]) : 0;
            var key = (BitConverter.DoubleToInt64Bits(height), _owner.Style);
            if (_style is null || _styleKey != key)
            {
                _style = _owner.GetToasterStyle(Toasts);
                _styleKey = key;
            }
            return _style;
        }

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
