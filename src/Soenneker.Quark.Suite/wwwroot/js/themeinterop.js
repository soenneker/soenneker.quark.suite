class ThemeInterop {
    constructor() {
        this.storageKey = "quark-theme";
        this.root = document.documentElement;
        this.media = window.matchMedia("(prefers-color-scheme: dark)");
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
        return isDark;
    }

    initialize() {
        return this.applyTheme(this.resolveIsDark());
    }

    toggle() {
        return this.applyTheme(!this.root.classList.contains("dark"));
    }
}

window.ThemeInterop = new ThemeInterop();
