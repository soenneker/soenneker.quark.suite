let activeDrag = null;
let lastPointerPosition = null;
let listenersInstalled = false;
const handleRegistrations = new WeakMap();

function invokeDotNet(handleElement, dotNetRef, methodName, handleIndex, percentage, size) {
  try {
    return dotNetRef
      .invokeMethodAsync(methodName, handleIndex, percentage, size)
      .then(() => {
        handleElement.dataset.resizableDotnet = "ok";
        delete handleElement.dataset.resizableDotnetError;
      })
      .catch((error) => {
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

function updateLastPointerPosition(clientX, clientY) {
  if (!Number.isFinite(clientX) || !Number.isFinite(clientY)) {
    return;
  }

  lastPointerPosition = { clientX, clientY };
}

function emitMove(clientX, clientY, pointerId) {
  updateLastPointerPosition(clientX, clientY);

  if (!activeDrag) {
    return;
  }

  if (activeDrag.usePointerId && pointerId != null && pointerId !== activeDrag.pointerId) {
    return;
  }

  const metrics = getMetrics(activeDrag.groupElement, clientX, clientY, activeDrag.orientation);
  activeDrag.handleElement.dataset.resizableLastPercentage = String(metrics.percentage);
  invokeDotNet(activeDrag.handleElement, activeDrag.dotNetRef, "HandlePointerDragMove", activeDrag.handleIndex, metrics.percentage, metrics.size);
}

function emitEnd(clientX, clientY, pointerId) {
  updateLastPointerPosition(clientX, clientY);

  if (!activeDrag) {
    return;
  }

  if (activeDrag.usePointerId && pointerId != null && pointerId !== activeDrag.pointerId) {
    return;
  }

  const metrics = getMetrics(activeDrag.groupElement, clientX, clientY, activeDrag.orientation);
  const drag = activeDrag;
  activeDrag = null;
  drag.handleElement.dataset.resizableLastPercentage = String(metrics.percentage);
  invokeDotNet(drag.handleElement, drag.dotNetRef, "HandlePointerDragEnd", drag.handleIndex, metrics.percentage, metrics.size);
}

function installListeners() {
  if (listenersInstalled) {
    return;
  }

  window.addEventListener("pointermove", (event) => {
    emitMove(event.clientX, event.clientY, event.pointerId);
  });

  window.addEventListener("pointerup", (event) => {
    emitEnd(event.clientX, event.clientY, event.pointerId);
  });

  window.addEventListener("pointercancel", (event) => {
    emitEnd(event.clientX, event.clientY, event.pointerId);
  });

  window.addEventListener("mousemove", (event) => {
    emitMove(event.clientX, event.clientY, null);
  });

  window.addEventListener("mouseup", (event) => {
    emitEnd(event.clientX, event.clientY, null);
  });

  listenersInstalled = true;
}

function beginDrag(handleElement, groupElement, orientation, dotNetRef, handleIndex, pointerId, clientX, clientY) {
  updateLastPointerPosition(clientX, clientY);

  activeDrag = {
    dotNetRef,
    groupElement,
    handleElement,
    handleIndex,
    orientation,
    pointerId,
    usePointerId: Number.isFinite(pointerId) && pointerId > 0,
  };

  if (activeDrag.usePointerId && typeof handleElement?.setPointerCapture === "function") {
    try {
      handleElement.setPointerCapture(pointerId);
    } catch {
      // Some browsers may reject synthetic capture attempts; global listeners still cover the drag.
    }
  }

  const metrics = getMetrics(groupElement, clientX, clientY, orientation);
  handleElement.dataset.resizableLastPercentage = String(metrics.percentage);
  invokeDotNet(handleElement, dotNetRef, "HandlePointerDragMove", handleIndex, metrics.percentage, metrics.size);
}

function disposeHandleRegistration(handleElement) {
  const registration = handleRegistrations.get(handleElement);
  if (!registration) {
    return;
  }

  handleElement.removeEventListener("pointerdown", registration.onPointerDown);
  handleElement.removeEventListener("mousedown", registration.onMouseDown);
  delete handleElement.dataset.resizableReady;
  delete handleElement.dataset.resizableLastPercentage;
  delete handleElement.dataset.resizableDotnet;
  delete handleElement.dataset.resizableDotnetError;
  delete handleElement.dataset.resizableDown;
  handleRegistrations.delete(handleElement);
}

export function initialize() {
  installListeners();
}

export function registerHandle(handleElement, groupElement, orientation, dotNetRef, handleIndex) {
  installListeners();

  disposeHandleRegistration(handleElement);

  const onPointerDown = (event) => {
    if (event.button !== 0) {
      return;
    }

    event.preventDefault();
    handleElement.dataset.resizableDown = "pointer";
    beginDrag(handleElement, groupElement, orientation, dotNetRef, handleIndex, event.pointerId, event.clientX, event.clientY);
  };

  const onMouseDown = (event) => {
    if (event.button !== 0) {
      return;
    }

    event.preventDefault();
    handleElement.dataset.resizableDown = "mouse";

    if (activeDrag?.handleElement === handleElement && activeDrag.usePointerId) {
      return;
    }

    beginDrag(handleElement, groupElement, orientation, dotNetRef, handleIndex, null, event.clientX, event.clientY);
  };

  handleElement.addEventListener("pointerdown", onPointerDown);
  handleElement.addEventListener("mousedown", onMouseDown);
  handleElement.dataset.resizableReady = "true";

  handleRegistrations.set(handleElement, {
    onPointerDown,
    onMouseDown,
  });
}

export function unregisterHandle(handleElement) {
  disposeHandleRegistration(handleElement);

  if (activeDrag?.handleElement === handleElement) {
    activeDrag = null;
  }
}

export function stopDrag() {
  activeDrag = null;
}
