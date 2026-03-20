class SonnerInterop {
    measureToastHeights(section) {
        const heights = {};

        if (!section) {
            return heights;
        }

        const toasts = section.querySelectorAll("[data-sonner-toast][data-toast-id]");

        for (const toast of toasts) {
            const id = toast.getAttribute("data-toast-id");

            if (!id) {
                continue;
            }

            const originalHeight = toast.style.height;

            try {
                // Measure intrinsic layout height instead of the animated/stacked height.
                toast.style.height = "auto";
                heights[id] = toast.offsetHeight;
            } finally {
                toast.style.height = originalHeight;
            }
        }

        return heights;
    }
}

window.SonnerInterop = new SonnerInterop();
