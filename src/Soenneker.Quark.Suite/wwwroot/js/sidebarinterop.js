const MOBILE_BREAKPOINT = 768;

let dotNetRef = null;
let mediaQueryList = null;
let mediaQueryHandler = null;
let keyboardHandler = null;

export function initializeSidebar(componentRef, shortcutKey) {
  cleanup();

  dotNetRef = componentRef;
  setupMobileDetection();
  setupKeyboardShortcut(shortcutKey);
}

export function getSidebarState(cookieKey) {
  const value = getCookie(cookieKey);

  if (value === "true") {
    return true;
  }

  if (value === "false") {
    return false;
  }

  return null;
}

export function saveSidebarState(cookieKey, value) {
  setCookie(cookieKey, value ? "true" : "false", 7);
}

function setupMobileDetection() {
  if (!dotNetRef || typeof window === "undefined") {
    return;
  }

  mediaQueryList = window.matchMedia(`(max-width: ${MOBILE_BREAKPOINT - 1}px)`);
  mediaQueryHandler = (event) => {
    dotNetRef.invokeMethodAsync("OnMobileChange", event.matches);
  };

  dotNetRef.invokeMethodAsync("OnMobileChange", mediaQueryList.matches);

  if (typeof mediaQueryList.addEventListener === "function") {
    mediaQueryList.addEventListener("change", mediaQueryHandler);
  } else if (typeof mediaQueryList.addListener === "function") {
    mediaQueryList.addListener(mediaQueryHandler);
  }
}

function setupKeyboardShortcut(shortcutKey) {
  if (!dotNetRef || typeof document === "undefined") {
    return;
  }

  const normalizedKey = (shortcutKey || "b").toLowerCase();

  keyboardHandler = (event) => {
    if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === normalizedKey) {
      event.preventDefault();
      dotNetRef.invokeMethodAsync("OnToggleShortcut");
    }
  };

  document.addEventListener("keydown", keyboardHandler);
}

function getCookie(name) {
  const nameEquals = `${name}=`;
  const values = document.cookie.split(";");

  for (let i = 0; i < values.length; i += 1) {
    let current = values[i];

    while (current.charAt(0) === " ") {
      current = current.substring(1, current.length);
    }

    if (current.indexOf(nameEquals) === 0) {
      return current.substring(nameEquals.length, current.length);
    }
  }

  return null;
}

function setCookie(name, value, days) {
  let expires = "";

  if (days) {
    const date = new Date();
    date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
    expires = `; expires=${date.toUTCString()}`;
  }

  document.cookie = `${name}=${value || ""}${expires}; path=/; SameSite=Lax`;
}

export function cleanup() {
  if (mediaQueryList && mediaQueryHandler) {
    if (typeof mediaQueryList.removeEventListener === "function") {
      mediaQueryList.removeEventListener("change", mediaQueryHandler);
    } else if (typeof mediaQueryList.removeListener === "function") {
      mediaQueryList.removeListener(mediaQueryHandler);
    }
  }

  if (keyboardHandler && typeof document !== "undefined") {
    document.removeEventListener("keydown", keyboardHandler);
  }

  mediaQueryList = null;
  mediaQueryHandler = null;
  keyboardHandler = null;
  dotNetRef = null;
}
// Each handle owns its listeners; provider cleanup must not affect other sidebars.
const resizeHandles = new WeakMap();

export function registerResizeHandle(handle, componentRef, minWidth, maxWidth, rightSide) {
  unregisterResizeHandle(handle);
  const root = handle.closest('[data-sidebar-resize-root]');
  if (!root) return;
  const container = handle.closest('[data-slot="sidebar-container"]') || root;
  const gap = root.querySelector('[data-slot="sidebar-gap"]');
  const sign = rightSide ? -1 : 1;
  const clamp = value => Math.min(maxWidth, Math.max(minWidth, value));
  let drag = null;

  const readWidth = () => container.getBoundingClientRect().width;
  const updateAria = () => {
    const width = Math.round(readWidth());
    handle.setAttribute('aria-valuenow', String(width));
    handle.setAttribute('aria-valuetext', `${width} pixels`);
  };
  const apply = value => {
    const width = clamp(value);
    root.style.setProperty('--sidebar-width', `${width}px`);
    handle.setAttribute('aria-valuenow', String(Math.round(width)));
    handle.setAttribute('aria-valuetext', `${Math.round(width)} pixels`);
    return width;
  };
  const notify = width => componentRef.invokeMethodAsync('OnWidthChanged', width);
  const stop = (commit) => {
    if (!drag) return;
    const previous = drag;
    drag = null;
    if (!commit) {
      if (previous.inlineWidth) root.style.setProperty('--sidebar-width', previous.inlineWidth);
      else root.style.removeProperty('--sidebar-width');
    }
    container.style.transition = previous.containerTransition;
    if (gap) gap.style.transition = previous.gapTransition;
    document.removeEventListener('selectstart', preventSelection);
    if (handle.hasPointerCapture(previous.id)) handle.releasePointerCapture(previous.id);
    updateAria();
    if (commit && previous.width !== previous.startWidth) return notify(previous.width);
  };
  const preventSelection = event => event.preventDefault();
  const down = event => {
    if (!event.isPrimary || event.button !== 0 || drag) return;
    event.preventDefault();
    handle.focus({ preventScroll: true });
    const startWidth = readWidth();
    drag = {
      id: event.pointerId, x: event.clientX, startWidth, width: startWidth,
      inlineWidth: root.style.getPropertyValue('--sidebar-width'),
      containerTransition: container.style.transition,
      gapTransition: gap?.style.transition || ''
    };
    container.style.transition = 'none';
    if (gap) gap.style.transition = 'none';
    document.addEventListener('selectstart', preventSelection);
    handle.setPointerCapture(event.pointerId);
  };
  const move = event => {
    if (!drag || drag.id !== event.pointerId) return;
    drag.width = apply(drag.startWidth + sign * (event.clientX - drag.x));
  };
  const up = event => {
    if (!drag || drag.id !== event.pointerId) return;
    move(event);
    return stop(true);
  };
  const cancel = event => {
    if (drag?.id === event.pointerId) stop(false);
  };
  const keydown = event => {
    if (event.key === 'Escape' && drag) {
      event.preventDefault();
      stop(false);
      return;
    }
    if (drag || !['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return;
    event.preventDefault();
    const inlineWidth = root.style.getPropertyValue('--sidebar-width');
    const current = inlineWidth.endsWith('px') ? parseFloat(inlineWidth) : readWidth();
    const step = event.shiftKey ? 32 : 8;
    const next = event.key === 'Home' ? minWidth : event.key === 'End' ? maxWidth :
      current + (event.key === 'ArrowRight' ? 1 : -1) * sign * step;
    const width = apply(next);
    if (width !== current) return notify(width);
  };
  handle.addEventListener('pointerdown', down);
  handle.addEventListener('pointermove', move);
  handle.addEventListener('pointerup', up);
  handle.addEventListener('pointercancel', cancel);
  handle.addEventListener('lostpointercapture', cancel);
  handle.addEventListener('keydown', keydown);
  const observer = new ResizeObserver(updateAria);
  observer.observe(container);
  updateAria();
  resizeHandles.set(handle, () => {
    stop(false);
    observer.disconnect();
    handle.removeEventListener('pointerdown', down);
    handle.removeEventListener('pointermove', move);
    handle.removeEventListener('pointerup', up);
    handle.removeEventListener('pointercancel', cancel);
    handle.removeEventListener('lostpointercapture', cancel);
    handle.removeEventListener('keydown', keydown);
  });


}

export function unregisterResizeHandle(handle) {
  resizeHandles.get(handle)?.();
  resizeHandles.delete(handle);
}
