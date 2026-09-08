const controllers = new WeakMap();

export function initialize(root) {
    if (controllers.has(root)) return;
    const reducedMotion = matchMedia('(prefers-reduced-motion: reduce)');
    let previous = snapshot();
    let animations = [];

    function snapshot() {
        const data = root.dataset;
        return {
            version: data.scrollVersion,
            enabled: data.scrollEnabled === 'true',
            paused: data.scrollPaused === 'true',
            duration: Number(data.scrollDuration),
            min: Number(data.scrollXMin), max: Number(data.scrollXMax),
            yMin: Number(data.scrollYMin), yMax: Number(data.scrollYMax),
            width: Number(data.scrollWidth), height: Number(data.scrollHeight)
        };
    }

    function cancel() {
        for (const animation of animations) animation.cancel();
        animations = [];
    }

    function freeze(event) {
        if (event && !event.target.closest('[data-slot="chart-plot"]')) return;
        for (const animation of animations) animation.pause();
    }

    function update() {
        const next = snapshot();
        const old = previous;
        previous = next;
        if (!next.enabled || reducedMotion.matches) {
            cancel();
            return;
        }
        if (next.paused) freeze();
        else if (old.paused) {
            for (const animation of animations) animation.play();
        }
        if (next.version === old.version) return;

        const span = next.max - next.min;
        const delta = next.min - old.min;
        const stableScale = old.enabled && span > 0 &&
            Math.abs(span - (old.max - old.min)) <= Math.max(1, span) * 1e-9 &&
            next.yMin === old.yMin && next.yMax === old.yMax &&
            next.width === old.width && next.height === old.height;

        // Interaction-only geometry updates should not disturb a frozen selection.
        if (stableScale && delta === 0) return;
        const plot = root.querySelector('[data-slot="chart-plot"]');
        const overlay = root.querySelector('[data-slot="chart-plot-overlay"]');
        const remaining = plot ? new DOMMatrix(getComputedStyle(plot).transform).m41 : 0;
        cancel();
        // Changed scales, resets, and wholly replaced windows cannot be translated faithfully.
        if (!plot || !stableScale || delta <= 0 || delta >= span) return;
        const step = delta / span * next.width;
        const distance = step + remaining;
        // Keep the same velocity when an update arrives before the previous step has finished.
        const duration = next.duration * distance / step;
        const startTime = document.timeline.currentTime;
        for (const element of [plot, overlay].filter(Boolean)) {
            const animation = element.animate([
                { transform: `translateX(${distance}px)` },
                { transform: 'translateX(0px)' }
            ], { duration, easing: 'linear', fill: 'forwards' });
            // Match the timeline used to sample the old transform; a pending play would hold
            // that sampled position for another frame on every data update.
            animation.startTime = startTime;
            animations.push(animation);
        }
        if (next.paused) freeze();
    }

    function motionChanged() {
        if (reducedMotion.matches) cancel();
    }

    root.addEventListener('pointerdown', freeze, true);
    root.addEventListener('focusin', freeze, true);
    reducedMotion.addEventListener('change', motionChanged);
    const observer = new MutationObserver(update);
    observer.observe(root, { attributes: true, attributeFilter: [
        'data-scroll-version', 'data-scroll-enabled', 'data-scroll-paused', 'data-scroll-duration'
    ] });
    controllers.set(root, () => {
        observer.disconnect();
        root.removeEventListener('pointerdown', freeze, true);
        root.removeEventListener('focusin', freeze, true);
        reducedMotion.removeEventListener('change', motionChanged);
        cancel();
    });
}

export function destroy(root) {
    controllers.get(root)?.();
    controllers.delete(root);
}
