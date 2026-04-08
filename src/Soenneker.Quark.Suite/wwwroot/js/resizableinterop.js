let activeDrag = null;

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
