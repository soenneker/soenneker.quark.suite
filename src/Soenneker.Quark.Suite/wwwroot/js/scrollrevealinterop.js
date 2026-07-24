const instances = new WeakMap();

export function initialize(element, options) {
  destroy(element);

  if (!element) {
    return;
  }

  const show = () => element.setAttribute("data-scroll-reveal-state", "visible");
  const hide = () => element.setAttribute("data-scroll-reveal-state", "hidden");
  const reducedMotion = window.matchMedia?.("(prefers-reduced-motion: reduce)").matches === true;

  if (options?.disabled === true || reducedMotion || !("IntersectionObserver" in window)) {
    show();
    return;
  }

  hide();

  const observer = new IntersectionObserver((entries) => {
    const entry = entries[entries.length - 1];

    if (entry.isIntersecting) {
      show();

      if (options?.once !== false) {
        observer.unobserve(element);
      }
    } else if (options?.once === false) {
      hide();
    }
  }, {
    threshold: Number.isFinite(options?.threshold) ? options.threshold : 0.15,
    rootMargin: options?.rootMargin || "0px 0px -10% 0px"
  });

  observer.observe(element);
  instances.set(element, {
    dispose: () => {
      observer.disconnect();
      element.removeAttribute("data-scroll-reveal-state");
    }
  });
}

export function destroy(element) {
  const instance = instances.get(element);

  if (!instance) {
    return;
  }

  instance.dispose();
  instances.delete(element);
}
