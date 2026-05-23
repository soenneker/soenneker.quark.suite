const registrations = new WeakMap();

export function registerCanvas(canvas, dotNetRef, disabled) {
  if (!canvas) {
    return false;
  }

  unregisterCanvas(canvas);

  if (disabled) {
    return true;
  }

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
  registrations.set(canvas, { pointerDown, pointerMove, stopDragging, click });
  return true;
}

export function unregisterCanvas(canvas) {
  const registration = registrations.get(canvas);

  if (!registration) {
    return;
  }

  canvas.removeEventListener("pointerdown", registration.pointerDown);
  canvas.removeEventListener("pointermove", registration.pointerMove);
  canvas.removeEventListener("pointerup", registration.stopDragging);
  canvas.removeEventListener("pointercancel", registration.stopDragging);
  canvas.removeEventListener("click", registration.click, true);
  registrations.delete(canvas);
}

function clamp(value, min, max) {
  return Math.min(max, Math.max(min, value));
}
