let activeDrag = null;

const resizableKeyboardGuards = new WeakMap();

/**
 * aria-orientation on the separator matches WAI-ARIA: horizontal line = vertical resize (ArrowUp/Down),
 * vertical line = horizontal resize (ArrowLeft/Right). Must align with ResizablePanelGroup.HandleHandleKeyDownAsync.
 */
function shouldPreventResizeKey(handleEl, key) {
  const o = handleEl.getAttribute("aria-orientation");
  if (o === "horizontal") {
    return key === "ArrowUp" || key === "ArrowDown";
  }
  if (o === "vertical") {
    return key === "ArrowLeft" || key === "ArrowRight";
  }
  return false;
}

/**
 * Capture-phase keydown on the panel group root: preventDefault only for resize arrow keys on a handle
 * so Tab/Escape/other keys are not swallowed (Blazor previously used blanket @onkeydown:preventDefault on the handle).
 */
export function attachResizableKeyboardGuard(groupElement) {
  if (!groupElement) {
    return;
  }

  detachResizableKeyboardGuard(groupElement);

  const handler = (ev) => {
    const handle = ev.target?.closest?.('[data-slot="resizable-handle"]');
    if (!handle || !groupElement.contains(handle)) {
      return;
    }

    if (!shouldPreventResizeKey(handle, ev.key)) {
      return;
    }

    ev.preventDefault();
  };

  groupElement.addEventListener("keydown", handler, true);
  resizableKeyboardGuards.set(groupElement, handler);
}

export function detachResizableKeyboardGuard(groupElement) {
  if (!groupElement) {
    return;
  }

  const handler = resizableKeyboardGuards.get(groupElement);
  if (!handler) {
    return;
  }

  groupElement.removeEventListener("keydown", handler, true);
  resizableKeyboardGuards.delete(groupElement);
}

function clamp(value, min, max) {
  return Math.min(Math.max(value, min), max);
}

function getMetrics(groupElement, clientX, clientY, orientation) {
  if (!groupElement) {
    return { percentage: 50, size: 0 };
  }

  const rect = groupElement.getBoundingClientRect();
  const isVertical = orientation === "vertical";
  const size = isVertical ? rect.height : rect.width;

  if (size <= 0) {
    return { percentage: 50, size: 0 };
  }

  if (isVertical) {
    return {
      percentage: clamp(((clientY - rect.top) / rect.height) * 100, 0, 100),
      size,
    };
  }

  const isRtl = window.getComputedStyle(groupElement).direction === "rtl";
  const ratio = isRtl
    ? (rect.right - clientX) / rect.width
    : (clientX - rect.left) / rect.width;

  return {
    percentage: clamp(ratio * 100, 0, 100),
    size,
  };
}

function removeActiveDrag() {
  if (!activeDrag) {
    return;
  }

  window.removeEventListener("pointermove", activeDrag.handlePointerMove);
  window.removeEventListener("pointerup", activeDrag.handlePointerEnd);
  window.removeEventListener("pointercancel", activeDrag.handlePointerEnd);
  activeDrag = null;
}

export function stopDrag() {
  removeActiveDrag();
}

export function startDrag(groupElement, pointerId, clientX, clientY, orientation, dotNetRef, handleIndex) {
  removeActiveDrag();

  const emitMove = (event) => {
    if (pointerId != null && event.pointerId !== pointerId) {
      return;
    }

    const metrics = getMetrics(groupElement, event.clientX, event.clientY, orientation);
    dotNetRef.invokeMethodAsync("HandlePointerDragMove", handleIndex, metrics.percentage, metrics.size);
  };

  const emitEnd = (event) => {
    if (pointerId != null && event.pointerId !== pointerId) {
      return;
    }

    const metrics = getMetrics(groupElement, event.clientX, event.clientY, orientation);
    removeActiveDrag();
    dotNetRef.invokeMethodAsync("HandlePointerDragEnd", handleIndex, metrics.percentage, metrics.size);
  };

  activeDrag = {
    handlePointerMove: emitMove,
    handlePointerEnd: emitEnd,
  };

  window.addEventListener("pointermove", emitMove);
  window.addEventListener("pointerup", emitEnd);
  window.addEventListener("pointercancel", emitEnd);

  const initialMetrics = getMetrics(groupElement, clientX, clientY, orientation);
  dotNetRef.invokeMethodAsync("HandlePointerDragMove", handleIndex, initialMetrics.percentage, initialMetrics.size);
}
