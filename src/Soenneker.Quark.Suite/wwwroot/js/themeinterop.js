class ThemeInterop {
    constructor() {
        this.storageKey = "quark-theme";
        this.root = document.documentElement;
        this.media = window.matchMedia("(prefers-color-scheme: dark)");
        this._themeChangedRefs = new Set();
        this._themeChangedListener = null;
    }

    resolveIsDark() {
        const stored = localStorage.getItem(this.storageKey);
        if (stored === "dark") return true;
        if (stored === "light") return false;
        return this.media.matches;
    }

    applyTheme(isDark) {
        this.root.classList.toggle("dark", isDark);
        localStorage.setItem(this.storageKey, isDark ? "dark" : "light");
        window.dispatchEvent(new CustomEvent("quark-theme-changed", { detail: { isDark } }));
        return isDark;
    }

    initialize() {
        return this.applyTheme(this.resolveIsDark());
    }

    toggle() {
        return this.applyTheme(!this.root.classList.contains("dark"));
    }

    registerThemeChangedCallback(dotNetRef) {
        this._themeChangedRefs.add(dotNetRef);
        if (!this._themeChangedListener) {
            this._themeChangedListener = (e) => {
                this._themeChangedRefs.forEach((r) => {
                    r.invokeMethodAsync("OnThemeChanged", e.detail.isDark);
                });
            };
            window.addEventListener("quark-theme-changed", this._themeChangedListener);
        }
    }

    unregisterThemeChangedCallback(dotNetRef) {
        this._themeChangedRefs.delete(dotNetRef);
        if (this._themeChangedRefs.size === 0 && this._themeChangedListener) {
            window.removeEventListener("quark-theme-changed", this._themeChangedListener);
            this._themeChangedListener = null;
        }
    }
}

window.ThemeInterop = new ThemeInterop();
