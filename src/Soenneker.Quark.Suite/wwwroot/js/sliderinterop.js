let activeDrag = null;

/** Keys that should not scroll the page / move focus incorrectly when a slider thumb is focused (ARIA slider pattern). */
const sliderThumbNavigationKeys = new Set([
  'Home',
  'End',
  'PageUp',
  'PageDown',
  'ArrowLeft',
  'ArrowRight',
  'ArrowUp',
  'ArrowDown',
]);

const sliderKeyboardGuards = new WeakMap();

function clamp(value, min, max) {
  return Math.min(Math.max(value, min), max);
}

function isRtl(trackElement) {
  return !!trackElement && window.getComputedStyle(trackElement).direction === "rtl";
}

function getRatio(trackElement, clientX, clientY, orientation) {
  if (!trackElement) {
    return 0;
  }

  const rect = trackElement.getBoundingClientRect();

  if (orientation === "vertical") {
    if (rect.height <= 0) {
      return 0;
    }

    return clamp((rect.bottom - clientY) / rect.height, 0, 1);
  }

  if (rect.width <= 0) {
    return 0;
  }

  if (isRtl(trackElement)) {
    return clamp((rect.right - clientX) / rect.width, 0, 1);
  }

  return clamp((clientX - rect.left) / rect.width, 0, 1);
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

export function getValueFromPointer(trackElement, clientX, clientY, min, max, orientation) {
  if (!trackElement || max <= min) {
    return min;
  }

  const ratio = getRatio(trackElement, clientX, clientY, orientation);
  return min + (max - min) * ratio;
}

export function stopDrag() {
  removeActiveDrag();
}

export function isDirectionRtl(trackElement) {
  return isRtl(trackElement);
}

/**
 * Capture-phase keydown on the slider root: preventDefault only for thumb navigation keys
 * so Tab/Escape/other keys are not swallowed (Blazor previously used blanket @onkeydown:preventDefault on thumbs).
 */
export function attachSliderKeyboardGuard(rootElement) {
  if (!rootElement) {
    return;
  }

  detachSliderKeyboardGuard(rootElement);

  const handler = (ev) => {
    if (!sliderThumbNavigationKeys.has(ev.key)) {
      return;
    }

    const thumb = ev.target?.closest?.('[data-slot="slider-thumb"]');
    if (!thumb || !rootElement.contains(thumb)) {
      return;
    }

    ev.preventDefault();
  };

  rootElement.addEventListener('keydown', handler, true);
  sliderKeyboardGuards.set(rootElement, handler);
}

export function detachSliderKeyboardGuard(rootElement) {
  if (!rootElement) {
    return;
  }

  const handler = sliderKeyboardGuards.get(rootElement);
  if (!handler) {
    return;
  }

  rootElement.removeEventListener('keydown', handler, true);
  sliderKeyboardGuards.delete(rootElement);
}

export function startDrag(trackElement, pointerId, clientX, clientY, min, max, orientation, dotNetRef, thumbIndex) {
  removeActiveDrag();

  const handlePointerMove = (event) => {
    if (pointerId != null && event.pointerId !== pointerId) {
      return;
    }

    const value = getValueFromPointer(trackElement, event.clientX, event.clientY, min, max, orientation);
    dotNetRef.invokeMethodAsync("HandlePointerDragMove", thumbIndex, value);
  };

  const handlePointerEnd = (event) => {
    if (pointerId != null && event.pointerId !== pointerId) {
      return;
    }

    const value = getValueFromPointer(trackElement, event.clientX, event.clientY, min, max, orientation);
    removeActiveDrag();
    dotNetRef.invokeMethodAsync("HandlePointerDragEnd", thumbIndex, value);
  };

  activeDrag = {
    handlePointerMove,
    handlePointerEnd,
  };

  window.addEventListener("pointermove", handlePointerMove);
  window.addEventListener("pointerup", handlePointerEnd);
  window.addEventListener("pointercancel", handlePointerEnd);

  return getValueFromPointer(trackElement, clientX, clientY, min, max, orientation);
}
