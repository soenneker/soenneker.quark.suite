const storageKey = "quark-theme";
const root = document.documentElement;
const media = window.matchMedia("(prefers-color-scheme: dark)");
const themeChangedRefs = new Map();
let themeChangedListener = null;

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
            Promise.resolve(ref.invokeMethodAsync("OnThemeChanged", e.detail.isDark))
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
    localStorage.setItem(storageKey, isDark ? "dark" : "light");
    window.dispatchEvent(new CustomEvent("quark-theme-changed", { detail: { isDark } }));
    return isDark;
}

export function resolveIsDark() {
    const stored = localStorage.getItem(storageKey);
    if (stored === "dark") return true;
    if (stored === "light") return false;
    return media.matches;
}

export function initialize() {
    return applyTheme(resolveIsDark());
}

export function toggle() {
    return applyTheme(!root.classList.contains("dark"));
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
