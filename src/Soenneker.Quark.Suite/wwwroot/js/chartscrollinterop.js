const controllers = new WeakMap();

export function initialize(root) {
    if (controllers.has(root)) return;
    const reducedMotion = matchMedia('(prefers-reduced-motion: reduce)');
    let previous = snapshot();
    let scrollFrame = null;
    let scrollElements = [];
    let scroll = null;
    let radialFrame = null;
    const slices = new Map();

    function radialPath([cx, cy, outer, start, sweep, inner]) {
        sweep = Math.min(359.999, Math.max(0, sweep));
        const polar = (radius, angle) => {
            const radians = angle * Math.PI / 180;
            return `${cx + radius * Math.cos(radians)},${cy + radius * Math.sin(radians)}`;
        };
        const large = sweep > 180 ? 1 : 0;
        const arc = `A${outer},${outer} 0 ${large} 1 ${polar(outer, start + sweep)}`;
        return inner <= 0
            ? `M${cx},${cy}L${polar(outer, start)}${arc}Z`
            : `M${polar(outer, start)}${arc}L${polar(inner, start + sweep)}A${inner},${inner} 0 ${large} 0 ${polar(inner, start)}Z`;
    }

    function stopRadial() {
        if (radialFrame !== null) cancelAnimationFrame(radialFrame);
        radialFrame = null;
        for (const element of slices.keys()) element.style.removeProperty('d');
        slices.clear();
    }

    function drawRadial(now) {
        radialFrame = null;
        let pending = false;
        for (const [element, state] of slices) {
            const progress = Math.min(1, Math.max(0, (now - state.started) / 500));
            const eased = progress * progress * (3 - 2 * progress);
            state.current = state.to.map((value, index) => state.from[index] + (value - state.from[index]) * eased);
            if (progress < 1) {
                // Override presentation only; Blazor retains ownership of the final d attribute.
                element.style.setProperty('d', `path("${radialPath(state.current)}")`);
                pending = true;
            } else {
                element.style.removeProperty('d');
            }
        }
        if (pending) radialFrame = requestAnimationFrame(drawRadial);
    }

    function updateRadial() {
        if (root.dataset.radialAnimated !== 'true' || reducedMotion.matches) {
            stopRadial();
            return;
        }
        const elements = new Set(root.querySelectorAll('[data-radial-geometry]'));
        for (const element of slices.keys()) {
            if (!elements.has(element)) {
                element.style.removeProperty('d');
                slices.delete(element);
            }
        }
        const now = performance.now();
        let changed = false;
        for (const element of elements) {
            const to = element.dataset.radialGeometry.split(' ').map(Number);
            const old = slices.get(element);
            if (old && to.every((value, index) => value === old.to[index])) continue;
            slices.set(element, { from: old?.current ?? to, current: old?.current ?? to, to, started: old ? now : now - 500 });
            changed = true;
        }
        if (changed) {
            if (radialFrame !== null) cancelAnimationFrame(radialFrame);
            drawRadial(now);
        }
    }
    updateRadial();

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
        if (scrollFrame !== null) cancelAnimationFrame(scrollFrame);
        scrollFrame = null;
        for (const element of scrollElements) element.style.removeProperty('transform');
        scrollElements = [];
        scroll = null;
    }

    function position(now) {
        if (!scroll) return 0;
        const elapsed = (scroll.pausedAt ?? now) - scroll.started;
        const fraction = Math.max(0, Math.min(1, elapsed / scroll.duration));
        return scroll.from + (scroll.to - scroll.from) * fraction;
    }

    function drawScroll(now) {
        scrollFrame = null;
        if (!scroll) return;
        const transform = `translateX(${position(now)}px)`;
        for (const element of scrollElements) element.style.setProperty('transform', transform);
        if (scroll.pausedAt === null && now < scroll.started + scroll.duration)
            scrollFrame = requestAnimationFrame(drawScroll);
    }

    function freeze(event) {
        if (event && !event.target.closest('[data-slot="chart-plot"]')) return;
        if (!scroll || scroll.pausedAt !== null) return;
        scroll.pausedAt = document.timeline.currentTime;
        if (scrollFrame !== null) cancelAnimationFrame(scrollFrame);
        drawScroll(scroll.pausedAt);
    }

    function update() {
        updateRadial();
        const next = snapshot();
        const old = previous;
        previous = next;
        if (!next.enabled || reducedMotion.matches) {
            cancel();
            return;
        }
        if (next.paused) freeze();
        else if (old.paused && scroll && scroll.pausedAt !== null) {
            const now = document.timeline.currentTime;
            scroll.started += now - scroll.pausedAt;
            scroll.pausedAt = null;
            drawScroll(now);
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
        // Reading computed SVG transforms after a Blazor update forces style/layout
        // work at every sample boundary. The animation clock already gives the offset.
        const now = document.timeline.currentTime;
        const remaining = position(now);
        // Changed scales, resets, and wholly replaced windows cannot be translated faithfully.
        if (!plot || !stableScale || delta <= 0 || delta >= span) {
            cancel();
            return;
        }
        const step = delta / span * next.width;
        const distance = step + remaining;
        // Keep scrolling for one extra interval so delivery/render jitter does not leave
        // the plot stationary between samples. Only translate recorded data; a delayed
        // feed leaves empty space at the right edge instead of inventing new values.
        // Bound the continuation so a disconnected feed eventually stops moving.
        const end = Math.min(0, distance) - step;
        const duration = next.duration * (distance - end) / step;
        const elements = [plot, overlay].filter(Boolean);
        if (scrollElements.length !== elements.length || scrollElements.some((element, index) => element !== elements[index]))
            cancel();
        scrollElements = elements;
        if (scrollFrame !== null) cancelAnimationFrame(scrollFrame);
        // SVG geometry and its compensating translation must reach the same paint.
        // A separate Web Animation can advance independently of Blazor's SVG updates,
        // briefly displaying a new path with the previous window's translation.
        scroll = { from: distance, to: end, duration, started: now, pausedAt: next.paused ? now : null };
        drawScroll(now);
    }

    function motionChanged() {
        if (reducedMotion.matches) cancel();
        updateRadial();
    }

    root.addEventListener('pointerdown', freeze, true);
    root.addEventListener('focusin', freeze, true);
    reducedMotion.addEventListener('change', motionChanged);
    const observer = new MutationObserver(update);
    observer.observe(root, { attributes: true, attributeFilter: [
        'data-scroll-version', 'data-scroll-enabled', 'data-scroll-paused', 'data-scroll-duration', 'data-radial-animated'
    ] });
    controllers.set(root, () => {
        observer.disconnect();
        root.removeEventListener('pointerdown', freeze, true);
        root.removeEventListener('focusin', freeze, true);
        reducedMotion.removeEventListener('change', motionChanged);
        cancel();
        stopRadial();
    });
}

export function destroy(root) {
    controllers.get(root)?.();
    controllers.delete(root);
}
