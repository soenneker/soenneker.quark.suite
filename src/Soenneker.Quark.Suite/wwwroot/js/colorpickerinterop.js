const registrations = new WeakMap();

export async function pickColor() {
  if (!("EyeDropper" in window)) {
    return null;
  }

  try {
    const result = await new window.EyeDropper().open();
    return result?.sRGBHex ?? null;
  } catch (error) {
    if (error?.name === "AbortError") {
      return null;
    }

    throw error;
  }
}

export function registerCanvas(root, dotNetRef, disabled) {
  if (!root?.querySelector) {
    return false;
  }

  unregisterCanvas(root);

  if (disabled) {
    return true;
  }

  let detachCanvas = null;
  let currentCanvas = null;

  const attachCanvas = () => {
    const canvas = root.querySelector("[data-slot='color-picker-canvas']");

    if (canvas === currentCanvas) {
      return;
    }

    detachCanvas?.();
    currentCanvas = canvas;
    detachCanvas = canvas ? wireCanvas(canvas, dotNetRef) : null;
  };

  attachCanvas();

  const observer = new MutationObserver(attachCanvas);
  observer.observe(root, { childList: true, subtree: true });
  registrations.set(root, {
    dispose: () => {
      observer.disconnect();
      detachCanvas?.();
    }
  });
  return true;
}

export function unregisterCanvas(root) {
  const registration = registrations.get(root);

  if (!registration) {
    return;
  }

  registration.dispose();
  registrations.delete(root);
}

function wireCanvas(canvas, dotNetRef) {

  let dragging = false;
  let pointerId = null;
  let latest = null;
  let suppressClick = false;

  const updateFromEvent = (event, requireInside = false) => {
    const rect = canvas.getBoundingClientRect();

    if (!rect.width || !rect.height) {
      return;
    }

    const inside =
      event.clientX >= rect.left &&
      event.clientX <= rect.right &&
      event.clientY >= rect.top &&
      event.clientY <= rect.bottom;

    if (requireInside && !inside) {
      return;
    }

    const saturation = clamp(((event.clientX - rect.left) / rect.width) * 100, 0, 100);
    const lightness = clamp(100 - (((event.clientY - rect.top) / rect.height) * 100), 0, 100);
    const hue = Number.parseFloat(canvas.getAttribute("data-hue") || "0");
    const alpha = Number.parseFloat(canvas.getAttribute("data-alpha") || "1");

    const thumb = canvas.querySelector("[data-slot='color-picker-thumb']");
    if (thumb) {
      thumb.style.left = `clamp(0.5rem, ${saturation}%, calc(100% - 0.5rem))`;
      thumb.style.top = `clamp(0.5rem, ${100 - lightness}%, calc(100% - 0.5rem))`;
      thumb.style.background = `hsl(${hue} ${saturation}% ${lightness}% / ${alpha})`;
    }

    latest = { saturation, lightness };
  };

  const pointerDown = event => {
    if (event.button !== undefined && event.button !== 0) {
      return;
    }

    dragging = true;
    pointerId = event.pointerId;
    suppressClick = true;
    canvas.setPointerCapture?.(pointerId);
    event.preventDefault();
    updateFromEvent(event);
  };

  const pointerMove = event => {
    if (!dragging || event.pointerId !== pointerId) {
      return;
    }

    event.preventDefault();
    updateFromEvent(event, true);
  };

  const stopDragging = event => {
    if (!dragging || event.pointerId !== pointerId) {
      return;
    }

    dragging = false;
    canvas.releasePointerCapture?.(pointerId);
    pointerId = null;
    event.preventDefault();

    if (latest) {
      dotNetRef.invokeMethodAsync("SetCanvasColor", latest.saturation, latest.lightness);
    }
  };

  const click = event => {
    if (suppressClick) {
      suppressClick = false;
      event.preventDefault();
      event.stopPropagation();
    }
  };

  canvas.addEventListener("pointerdown", pointerDown);
  canvas.addEventListener("pointermove", pointerMove);
  canvas.addEventListener("pointerup", stopDragging);
  canvas.addEventListener("pointercancel", stopDragging);
  canvas.addEventListener("click", click, true);
  return () => {
    canvas.removeEventListener("pointerdown", pointerDown);
    canvas.removeEventListener("pointermove", pointerMove);
    canvas.removeEventListener("pointerup", stopDragging);
    canvas.removeEventListener("pointercancel", stopDragging);
    canvas.removeEventListener("click", click, true);
  };
}

function clamp(value, min, max) {
  return Math.min(max, Math.max(min, value));
}
