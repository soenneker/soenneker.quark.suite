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
