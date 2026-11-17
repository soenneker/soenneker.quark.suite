const registrations = new WeakMap();
const interactive = 'a,button,input,textarea,select,summary,[role="button"],[role="slider"],[contenteditable]:not([contenteditable="false"]),[data-swipe-ignore]';

export function destroy(element) {
    const state = element && registrations.get(element);
    if (!state) return;
    state.cleanup();
    registrations.delete(element);
}

export function configure(element, callback, options) {
    if (!element) return;
    const key = JSON.stringify(options);
    const previous = registrations.get(element);
    if (previous?.key === key) { previous.callback = callback; return; }
    destroy(element);
    if (options.disabled || !element.isConnected) return;

    const doc = element.ownerDocument;
    const state = { key, callback, pending: false };
    let gesture = null;
    let suppressClickUntil = 0;
    const listeners = [];
    const listen = (target, name, handler, settings) => {
        target.addEventListener(name, handler, settings);
        listeners.push(() => target.removeEventListener(name, handler, settings));
    };
    const cancel = () => { gesture = null; };
    const current = () => registrations.get(element) === state && element.isConnected;

    function begin(event, point, id) {
        if (!current() || state.pending || event.defaultPrevented || event.target?.closest?.(interactive)) return;
        // The innermost registered SwipeArea owns the gesture.
        for (let node = event.target; node && node !== element; node = node.parentElement)
            if (registrations.has(node)) return;
        const x = point.clientX, y = point.clientY;
        const viewportWidth = doc.documentElement.clientWidth;
        if (options.preserveNativeEdgeGestures && (x < 24 || x > viewportWidth - 24)) return;
        const rect = element.getBoundingClientRect();
        if (x < rect.left || x > rect.right || y < rect.top || y > rect.bottom) return;
        if ((options.startEdge === 'left' && x - rect.left > options.edgeSize) ||
            (options.startEdge === 'right' && rect.right - x > options.edgeSize) ||
            (options.startEdge === 'top' && y - rect.top > options.edgeSize) ||
            (options.startEdge === 'bottom' && rect.bottom - y > options.edgeSize)) return;
        suppressClickUntil = 0;
        gesture = { id, x, y, time: event.timeStamp, direction: null };
    }

    function direction(dx, dy) {
        if (Math.abs(dx) >= Math.abs(dy) * 1.5) return dx < 0 ? 'left' : 'right';
        if (Math.abs(dy) >= Math.abs(dx) * 1.5) return dy < 0 ? 'up' : 'down';
        return null;
    }

    function move(event, point) {
        if (!gesture || !current()) return;
        const dx = point.clientX - gesture.x, dy = point.clientY - gesture.y;
        if (event.timeStamp - gesture.time > options.maxDuration) { cancel(); return; }
        if (!gesture.direction) {
            if (Math.max(Math.abs(dx), Math.abs(dy)) < 10) return;
            const intent = direction(dx, dy);
            // An ambiguous diagonal or unhandled direction belongs to normal scrolling.
            if (!intent || !options[intent]) { cancel(); return; }
            gesture.direction = intent;
        }
        if (!event.cancelable || event.defaultPrevented) { cancel(); return; }
        event.preventDefault();
        suppressClickUntil = event.timeStamp + 500;
    }

    function end(event, point) {
        const completed = gesture;
        cancel();
        if (!completed || !current() || !completed.direction) return;
        const dx = point.clientX - completed.x, dy = point.clientY - completed.y;
        if (event.timeStamp - completed.time > options.maxDuration ||
            Math.max(Math.abs(dx), Math.abs(dy)) < options.threshold ||
            direction(dx, dy) !== completed.direction) return;
        state.pending = true;
        suppressClickUntil = event.timeStamp + 500;
        state.callback.invokeMethodAsync('NotifySwipe', completed.direction)
            .catch(error => { if (current()) console.error('SwipeArea callback failed.', error); })
            .finally(() => { state.pending = false; });
    }

    listen(element, 'touchstart', event => {
        cancel();
        if (event.touches.length === 1) begin(event, event.touches[0], event.touches[0].identifier);
    }, { passive: true });
    listen(doc, 'touchstart', event => { if (event.touches.length !== 1) cancel(); }, { passive: true });
    listen(doc, 'touchmove', event => {
        if (!gesture || gesture.id === 'mouse') return;
        if (event.touches.length !== 1) { cancel(); return; }
        const point = Array.from(event.touches).find(touch => touch.identifier === gesture.id);
        if (point) move(event, point);
    }, { passive: false });
    listen(doc, 'touchend', event => {
        if (!gesture || gesture.id === 'mouse') return;
        const point = Array.from(event.changedTouches).find(touch => touch.identifier === gesture.id);
        if (point) end(event, point);
    }, { passive: true });
    listen(doc, 'touchcancel', cancel, { passive: true });
    if (options.enableMouse) {
        listen(element, 'mousedown', event => {
            if (event.button === 0) begin(event, event, 'mouse');
        });
        listen(doc, 'mousemove', event => {
            if (gesture?.id !== 'mouse') return;
            if (event.buttons !== 1) { cancel(); return; }
            move(event, event);
        });
        listen(doc, 'mouseup', event => { if (gesture?.id === 'mouse') end(event, event); });
    }
    listen(element, 'click', event => {
        if (event.detail !== 0 && event.timeStamp <= suppressClickUntil) {
            event.preventDefault();
            event.stopPropagation();
        }
    }, true);
    listen(doc.defaultView, 'blur', cancel);
    state.cleanup = () => { cancel(); for (const remove of listeners) remove(); };
    registrations.set(element, state);
}
