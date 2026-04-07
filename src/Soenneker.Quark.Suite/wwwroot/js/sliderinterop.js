let activeDrag = null;

function clamp(value, min, max) {
  return Math.min(Math.max(value, min), max);
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
