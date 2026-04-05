class ThemeInterop {
    constructor() {
        this.storageKey = "quark-theme";
        this.root = document.documentElement;
        this.media = window.matchMedia("(prefers-color-scheme: dark)");
        this._themeChangedRefs = new Map();
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

    getThemeChangedRefId(dotNetRef) {
        if (!dotNetRef) {
            return null;
        }

        // Blazor runtime internals may vary by version, so check known id shapes.
        return dotNetRef._id ?? dotNetRef._dotNetObjectId ?? dotNetRef.__dotNetObject ?? null;
    }

    ensureThemeChangedListener() {
        if (this._themeChangedListener) {
            return;
        }

        this._themeChangedListener = (e) => {
            const refs = Array.from(this._themeChangedRefs.entries());

            refs.forEach(([id, ref]) => {
                Promise.resolve(ref.invokeMethodAsync("OnThemeChanged", e.detail.isDark))
                    .catch(() => {
                        // Reference was likely disposed; stop calling it.
                        this._themeChangedRefs.delete(id);
                        this.removeThemeChangedListenerIfUnused();
                    });
            });
        };

        window.addEventListener("quark-theme-changed", this._themeChangedListener);
    }

    removeThemeChangedListenerIfUnused() {
        if (this._themeChangedRefs.size === 0 && this._themeChangedListener) {
            window.removeEventListener("quark-theme-changed", this._themeChangedListener);
            this._themeChangedListener = null;
        }
    }

    registerThemeChangedCallback(dotNetRef) {
        const id = this.getThemeChangedRefId(dotNetRef);

        if (id == null) {
            return;
        }

        this._themeChangedRefs.set(id, dotNetRef);
        this.ensureThemeChangedListener();
    }

    unregisterThemeChangedCallback(dotNetRef) {
        const id = this.getThemeChangedRefId(dotNetRef);

        if (id != null) {
            this._themeChangedRefs.delete(id);
        }

        this.removeThemeChangedListenerIfUnused();
    }
}

const themeInterop = window.ThemeInterop ?? new ThemeInterop();

window.ThemeInterop = themeInterop;

if (document.currentScript?.dataset?.quarkThemeBootstrap === "true") {
    themeInterop.initialize();
}
