import "./picture.js";

const storageKey = "quark-theme";
const root = document.documentElement;
const themeChangedRefs = new Map();
let themeChangedListener = null;
const systemTheme = window.matchMedia("(prefers-color-scheme: dark)");
let initialized = false;
let preference;
let useMemoryPreference = false;

function readPreference() {
    if (useMemoryPreference) return preference;
    try {
        return localStorage.getItem(storageKey);
    } catch {
        return preference;
    }
}

function savePreference(value) {
    preference = value;
    try {
        if (value == null) localStorage.removeItem(storageKey);
        else localStorage.setItem(storageKey, value);
        useMemoryPreference = false;
    } catch {
        // Theme selection still works when browser storage is unavailable.
        useMemoryPreference = true;
    }
}

function getThemeChangedRefId(dotNetRef) {
    if (!dotNetRef) {
        return null;
    }

    return dotNetRef._id ?? dotNetRef._dotNetObjectId ?? dotNetRef.__dotNetObject ?? null;
}

function removeThemeChangedListenerIfUnused() {
    if (themeChangedRefs.size === 0 && themeChangedListener) {
        window.removeEventListener("quark-theme-changed", themeChangedListener);
        themeChangedListener = null;
    }
}

function ensureThemeChangedListener() {
    if (themeChangedListener) {
        return;
    }

    themeChangedListener = (e) => {
        const refs = Array.from(themeChangedRefs.entries());

        refs.forEach(([id, ref]) => {
            Promise.resolve(ref.invokeMethodAsync("OnThemeChanged", e.detail.isDark, getMode()))
                .catch(() => {
                    themeChangedRefs.delete(id);
                    removeThemeChangedListenerIfUnused();
                });
        });
    };

    window.addEventListener("quark-theme-changed", themeChangedListener);
}

function applyTheme(isDark) {
    root.classList.toggle("dark", isDark);
    root.style.colorScheme = isDark ? "dark" : "light";
    window.dispatchEvent(new CustomEvent("quark-theme-changed", { detail: { isDark } }));
    return isDark;
}

export function getMode() {
    const stored = readPreference();
    return stored === "light" || stored === "dark" ? stored : "system";
}

export function setMode(mode) {
    if (!["system", "light", "dark"].includes(mode)) throw new Error("Invalid theme mode");
    savePreference(mode === "system" ? null : mode);
    return initialize();
}

export function resolveIsDark() {
    const stored = readPreference();
    if (stored === "dark") return true;
    if (stored === "light") return false;
    return systemTheme.matches;
}

export function initialize() {
    if (!initialized) {
        initialized = true;
        systemTheme.addEventListener("change", () => {
            const stored = readPreference();
            if (stored !== "dark" && stored !== "light") applyTheme(resolveIsDark());
        });
        window.addEventListener("storage", event => {
            if (event.key === storageKey || event.key === null) {
                useMemoryPreference = false;
                applyTheme(resolveIsDark());
            }
        });
    }
    return applyTheme(resolveIsDark());
}

export function toggle() {
    const isDark = !resolveIsDark();
    savePreference(isDark ? "dark" : "light");
    return initialize();
}

export function useSystem() {
    savePreference(null);
    return initialize();
}

export function registerThemeChangedCallback(dotNetRef) {
    const id = getThemeChangedRefId(dotNetRef);

    if (id == null) {
        return;
    }

    themeChangedRefs.set(id, dotNetRef);
    ensureThemeChangedListener();
}

export function unregisterThemeChangedCallback(dotNetRef) {
    const id = getThemeChangedRefId(dotNetRef);

    if (id != null) {
        themeChangedRefs.delete(id);
    }

    removeThemeChangedListenerIfUnused();
}
