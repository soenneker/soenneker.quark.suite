const instances = new WeakMap();

export function initialize(element, options, dotNetRef) {
  destroy(element);

  if (!element) {
    return;
  }

  const dataAttribute = options?.dataAttribute || "scrollspy";
  const offset = Number.isFinite(options?.offset) ? options.offset : 0;
  const smooth = options?.smooth !== false;
  const history = options?.history !== false;
  const throttleTime = Number.isFinite(options?.throttleTime) ? options.throttleTime : 0;
  const anchorSelector = `[data-${dataAttribute}-anchor]`;
  let anchors = Array.from(element.querySelectorAll(anchorSelector));
  let previousId = null;
  let timeout = 0;
  let frame = 0;
  let initialTimeout = 0;
  element.setAttribute("data-scrollspy-initialized", "true");

  const getScrollElement = () => {
    let target = options?.targetSelector ? document.querySelector(options.targetSelector) : document.documentElement;

    if (!target) {
      target = document.documentElement;
    }

    const viewport = target.querySelector?.('[data-slot="scroll-area-viewport"]');
    return viewport instanceof HTMLElement ? viewport : target;
  };

  const getScrollTop = (scrollElement) => {
    return scrollElement === document.documentElement || scrollElement === document.body
      ? window.scrollY || document.documentElement.scrollTop || 0
      : scrollElement.scrollTop;
  };

  const setActiveSection = (sectionId, force = false) => {
    if (!sectionId) {
      return;
    }

    if (!force && previousId === sectionId) {
      return;
    }

    anchors.forEach((anchor) => {
      const id = anchor.getAttribute(`data-${dataAttribute}-anchor`)?.replace("#", "");

      if (id === sectionId) {
        anchor.setAttribute("data-active", "true");
        anchor.setAttribute("aria-current", "location");
      } else {
        anchor.removeAttribute("data-active");
        anchor.removeAttribute("aria-current");
      }
    });

    if (dotNetRef) {
      dotNetRef.invokeMethodAsync("HandleUpdate", sectionId);
    }

    if (history && (force || previousId !== sectionId)) {
      const url = new URL(window.location.href);
      url.hash = sectionId;
      window.history.replaceState({}, "", `${url.pathname}${url.search}${url.hash}`);
    }

    previousId = sectionId;
  };

  const getAnchorOffset = (anchor) => {
    const custom = anchor.getAttribute(`data-${dataAttribute}-offset`);
    const parsed = custom == null ? NaN : Number.parseInt(custom, 10);
    return Number.isFinite(parsed) ? parsed : offset;
  };

  const handleScroll = () => {
    anchors = Array.from(element.querySelectorAll(anchorSelector));

    if (!anchors.length) {
      return;
    }

    const scrollElement = getScrollElement();
    const scrollTop = getScrollTop(scrollElement);
    let activeIndex = 0;
    let minDelta = Number.POSITIVE_INFINITY;

    anchors.forEach((anchor, index) => {
      const sectionId = anchor.getAttribute(`data-${dataAttribute}-anchor`)?.replace("#", "");
      const section = sectionId ? document.getElementById(sectionId) : null;

      if (!section) {
        return;
      }

      const customOffset = getAnchorOffset(anchor);
      const top = section.offsetTop - customOffset;
      const delta = Math.abs(top - scrollTop);

      if (top <= scrollTop && delta < minDelta) {
        minDelta = delta;
        activeIndex = index;
      }
    });

    const scrollHeight = scrollElement.scrollHeight || document.documentElement.scrollHeight;
    const clientHeight = scrollElement.clientHeight || window.innerHeight;

    if (scrollTop + clientHeight >= scrollHeight - 2) {
      activeIndex = anchors.length - 1;
    }

    const sectionId = anchors[activeIndex]?.getAttribute(`data-${dataAttribute}-anchor`)?.replace("#", "");
    setActiveSection(sectionId);
  };

  const scheduleScroll = () => {
    if (throttleTime > 0) {
      if (timeout) {
        return;
      }

      timeout = window.setTimeout(() => {
        timeout = 0;
        handleScroll();
      }, throttleTime);

      return;
    }

    if (frame) {
      return;
    }

    frame = window.requestAnimationFrame(() => {
      frame = 0;
      handleScroll();
    });
  };

  const scrollToAnchor = (anchor, event) => {
    if (event) {
      event.preventDefault();

      if (initialTimeout) {
        window.clearTimeout(initialTimeout);
        initialTimeout = 0;
      }
    }

    const sectionId = anchor.getAttribute(`data-${dataAttribute}-anchor`)?.replace("#", "");
    const section = sectionId ? document.getElementById(sectionId) : null;

    if (!section) {
      return;
    }

    const scrollElement = getScrollElement();
    const top = section.offsetTop - getAnchorOffset(anchor);

    if (scrollElement === document.documentElement || scrollElement === document.body) {
      window.scrollTo({ top, left: 0, behavior: smooth ? "smooth" : "auto" });
    } else {
      scrollElement.scrollTo({ top, left: 0, behavior: smooth ? "smooth" : "auto" });
    }

    setActiveSection(sectionId, true);
  };

  const handleClick = (event) => {
    const anchor = event.target?.closest?.(anchorSelector);

    if (!anchor || !element.contains(anchor)) {
      return;
    }

    scrollToAnchor(anchor, event);
  };

  const scrollToHashSection = () => {
    const hash = window.location.hash.replace("#", "");
    if (!hash) {
      return;
    }

    const escaped = window.CSS?.escape ? CSS.escape(hash) : hash.replace(/"/g, '\\"');
    const anchor = element.querySelector(`[data-${dataAttribute}-anchor="${escaped}"]`);

    if (anchor) {
      scrollToAnchor(anchor);
    }
  };

  window.addEventListener("scroll", scheduleScroll, true);
  window.addEventListener("resize", scheduleScroll);
  element.addEventListener("click", handleClick);

  initialTimeout = window.setTimeout(() => {
    initialTimeout = 0;
    scrollToHashSection();
    handleScroll();
  }, 100);

  instances.set(element, {
    dispose: () => {
      window.removeEventListener("scroll", scheduleScroll, true);
      window.removeEventListener("resize", scheduleScroll);
      window.clearTimeout(initialTimeout);

      if (timeout) {
        window.clearTimeout(timeout);
      }

      if (frame) {
        window.cancelAnimationFrame(frame);
      }

      element.removeEventListener("click", handleClick);
      element.removeAttribute("data-scrollspy-initialized");
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
