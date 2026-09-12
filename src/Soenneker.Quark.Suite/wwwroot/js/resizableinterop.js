let activeDrag = null;
let pendingClientX = 0;
let pendingClientY = 0;
let moveFrame = 0;
let listenersInstalled = false;
const handleRegistrations = new WeakMap();

function invokeDotNet(handleElement, dotNetRef, methodName, handleIndex, percentage, size) {
  const registration = handleRegistrations.get(handleElement);
  try {
    return dotNetRef
      .invokeMethodAsync(methodName, handleIndex, percentage, size)
      .then(() => {
        if (handleRegistrations.get(handleElement) !== registration) return;
        handleElement.dataset.resizableDotnet = "ok";
        delete handleElement.dataset.resizableDotnetError;
      })
      .catch((error) => {
        if (handleRegistrations.get(handleElement) !== registration) return;
        handleElement.dataset.resizableDotnet = "error";
        handleElement.dataset.resizableDotnetError = String(error);
      });
  } catch (error) {
    handleElement.dataset.resizableDotnet = "error";
    handleElement.dataset.resizableDotnetError = String(error);
    return Promise.resolve();
  }
}

function clamp(value, min, max) {
  return Math.min(Math.max(value, min), max);
}

function getMetrics(groupElement, clientX, clientY, orientation, metrics) {
  if (!groupElement) {
    metrics.percentage = 50;
    metrics.size = 0;
    return metrics;
  }

  const rect = groupElement.getBoundingClientRect();
  const isVertical = orientation === "vertical";
  const size = isVertical ? rect.height : rect.width;

  if (size <= 0) {
    metrics.percentage = 50;
    metrics.size = 0;
    return metrics;
  }

  metrics.size = size;
  if (isVertical) {
    metrics.percentage = clamp(((clientY - rect.top) / rect.height) * 100, 0, 100);
    return metrics;
  }

  const isRtl = window.getComputedStyle(groupElement).direction === "rtl";
  const ratio = isRtl
    ? (rect.right - clientX) / rect.width
    : (clientX - rect.left) / rect.width;

  metrics.percentage = clamp(ratio * 100, 0, 100);
  return metrics;
}

function emitMove() {
  moveFrame = 0;
  if (!activeDrag) {
    return;
  }

  const metrics = getMetrics(activeDrag.groupElement, pendingClientX, pendingClientY, activeDrag.orientation, activeDrag);
  activeDrag.handleElement.dataset.resizableLastPercentage = String(metrics.percentage);
  invokeDotNet(activeDrag.handleElement, activeDrag.dotNetRef, "HandlePointerDragMove", activeDrag.handleIndex, metrics.percentage, metrics.size);
}

function scheduleMove(clientX, clientY, pointerId) {
  if (!ownsPointer(pointerId)) {
    return;
  }
  pendingClientX = clientX;
  pendingClientY = clientY;

  if (moveFrame) {
    return;
  }

  moveFrame = window.requestAnimationFrame(emitMove);
}

function cancelPendingMove() {
  if (moveFrame) {
    window.cancelAnimationFrame(moveFrame);
    moveFrame = 0;
  }
}

function ownsPointer(pointerId) {
  return activeDrag && (activeDrag.usePointerId
    ? pointerId === activeDrag.pointerId
    : pointerId == null);
}

function cancelActiveDrag() {
  const drag = activeDrag;
  activeDrag = null;
  cancelPendingMove();
  uninstallListeners();

  if (drag?.usePointerId) {
    try {
      if (drag.handleElement.hasPointerCapture?.(drag.pointerId)) {
        drag.handleElement.releasePointerCapture(drag.pointerId);
      }
    } catch {
      // Capture may already have been released by the browser or a detached handle.
    }
  }
}

function emitEnd(clientX, clientY, pointerId) {
  if (!ownsPointer(pointerId)) {
    return;
  }

  const metrics = getMetrics(activeDrag.groupElement, clientX, clientY, activeDrag.orientation, activeDrag);
  const drag = activeDrag;
  cancelActiveDrag();
  drag.handleElement.dataset.resizableLastPercentage = String(metrics.percentage);
  invokeDotNet(drag.handleElement, drag.dotNetRef, "HandlePointerDragEnd", drag.handleIndex, metrics.percentage, metrics.size);
}

function onPointerMove(event) {
  scheduleMove(event.clientX, event.clientY, event.pointerId);
}

function onPointerUp(event) {
  emitEnd(event.clientX, event.clientY, event.pointerId);
}

function onMouseMove(event) {
  scheduleMove(event.clientX, event.clientY, null);
}

function onMouseUp(event) {
  emitEnd(event.clientX, event.clientY, null);
}

function installListeners() {
  if (listenersInstalled) {
    return;
  }

  window.addEventListener("pointermove", onPointerMove);
  window.addEventListener("pointerup", onPointerUp);
  window.addEventListener("pointercancel", onPointerUp);
  window.addEventListener("mousemove", onMouseMove);
  window.addEventListener("mouseup", onMouseUp);

  listenersInstalled = true;
}

function uninstallListeners() {
  if (!listenersInstalled) {
    return;
  }

  window.removeEventListener("pointermove", onPointerMove);
  window.removeEventListener("pointerup", onPointerUp);
  window.removeEventListener("pointercancel", onPointerUp);
  window.removeEventListener("mousemove", onMouseMove);
  window.removeEventListener("mouseup", onMouseUp);
  listenersInstalled = false;
}

function beginDrag(handleElement, groupElement, orientation, dotNetRef, handleIndex, pointerId, clientX, clientY) {
  cancelActiveDrag();

  activeDrag = {
    dotNetRef,
    groupElement,
    handleElement,
    handleIndex,
    orientation,
    pointerId,
    percentage: 50,
    size: 0,
    usePointerId: Number.isFinite(pointerId) && pointerId > 0,
  };
  installListeners();

  if (activeDrag.usePointerId && typeof handleElement?.setPointerCapture === "function") {
    try {
      handleElement.setPointerCapture(pointerId);
    } catch {
      // Some browsers may reject synthetic capture attempts; global listeners still cover the drag.
    }
  }

  const metrics = getMetrics(groupElement, clientX, clientY, orientation, activeDrag);
  handleElement.dataset.resizableLastPercentage = String(metrics.percentage);
}

function disposeHandleRegistration(handleElement) {
  if (activeDrag?.handleElement === handleElement) {
    cancelActiveDrag();
  }
  const registration = handleRegistrations.get(handleElement);
  if (!registration) {
    return;
  }

  handleElement.removeEventListener("pointerdown", registration.onPointerDown);
  handleElement.removeEventListener("mousedown", registration.onMouseDown);
  handleElement.removeEventListener("keydown", registration.onKeyDown);
  delete handleElement.dataset.resizableReady;
  delete handleElement.dataset.resizableLastPercentage;
  delete handleElement.dataset.resizableDotnet;
  delete handleElement.dataset.resizableDotnetError;
  delete handleElement.dataset.resizableDown;
  handleRegistrations.delete(handleElement);
}

export function initialize() {
}

export function startDrag(groupElement, pointerId, clientX, clientY, orientation, dotNetRef, handleIndex) {
  if (!groupElement) {
    return;
  }

  let handleElement;
  let index = 0;
  for (const child of groupElement.children) {
    if (child.dataset?.slot === "resizable-handle" && index++ === handleIndex) {
      handleElement = child;
      break;
    }
  }

  if (!handleElement) {
    return;
  }

  beginDrag(handleElement, groupElement, orientation, dotNetRef, handleIndex, pointerId, clientX, clientY);
}

export function registerHandle(handleElement, groupElement, orientation, dotNetRef, handleIndex) {
  disposeHandleRegistration(handleElement);

  const onPointerDown = (event) => {
    if (event.button !== 0 || event.isPrimary === false || activeDrag) {
      return;
    }

    event.preventDefault();
    handleElement.dataset.resizableDown = "pointer";
    beginDrag(handleElement, groupElement, orientation, dotNetRef, handleIndex, event.pointerId, event.clientX, event.clientY);
  };

  const onMouseDown = (event) => {
    if (event.button !== 0 || activeDrag) {
      return;
    }

    event.preventDefault();
    handleElement.dataset.resizableDown = "mouse";

    beginDrag(handleElement, groupElement, orientation, dotNetRef, handleIndex, null, event.clientX, event.clientY);
  };

  const onKeyDown = (event) => {
    if (
      event.key === "ArrowLeft" ||
      event.key === "ArrowRight" ||
      event.key === "ArrowUp" ||
      event.key === "ArrowDown"
    ) {
      event.preventDefault();
    }
  };

  handleElement.addEventListener("pointerdown", onPointerDown);
  handleElement.addEventListener("mousedown", onMouseDown);
  handleElement.addEventListener("keydown", onKeyDown);
  handleElement.dataset.resizableReady = "true";

  handleRegistrations.set(handleElement, {
    onPointerDown,
    onMouseDown,
    onKeyDown,
  });
}

export function unregisterHandle(handleElement) {
  disposeHandleRegistration(handleElement);
}

export function stopDrag() {
  cancelActiveDrag();
}
