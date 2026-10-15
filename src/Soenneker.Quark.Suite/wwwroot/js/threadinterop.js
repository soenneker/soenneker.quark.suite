const instances = new WeakMap();

function isAtBottom(element) {
  return element.scrollHeight - element.scrollTop - element.clientHeight <= 2;
}

function behaviorOrAuto(value) {
  return value === "smooth" || value === "instant" || value === "auto" ? value : "auto";
}

function scrollToBottomInternal(element, behavior) {
  element.scrollTo({
    top: element.scrollHeight,
    behavior: behaviorOrAuto(behavior),
  });
}

function notify(instance, next = isAtBottom(instance.element)) {
  if (next === instance.lastIsAtBottom) {
    return;
  }

  instance.lastIsAtBottom = next;
  instance.dotNetRef.invokeMethodAsync("SetIsAtBottom", next).catch(() => {});
}

export function initialize(element, dotNetRef, initial = "smooth", resize = "smooth", stickToBottom = true) {
  if (!element) {
    return;
  }

  const existing = instances.get(element);
  if (existing) {
    existing.dotNetRef = dotNetRef;
    existing.initial = initial;
    existing.resize = resize;
    existing.stickToBottom = stickToBottom;
    return;
  }

  const instance = {
    element,
    dotNetRef,
    lastIsAtBottom: true,
    onScroll: null,
    mutationObserver: null,
    resizeObserver: null,
    frame: 0,
    initialPending: true,
    initial,
    resize,
    stickToBottom,
    following: true,
    lastScrollTop: element.scrollTop,
    observedChildren: new Set(),
  };

  const update = () => {
    instance.frame = 0;
    if (instances.get(element) !== instance) return;
    if (instance.initialPending || (instance.stickToBottom && instance.following)) {
      scrollToBottomInternal(element, instance.initialPending ? instance.initial : instance.resize);
    }
    instance.initialPending = false;
    notify(instance);
  };
  const scheduleUpdate = () => {
    if (!instance.frame) instance.frame = requestAnimationFrame(update);
  };

  instance.onScroll = () => {
    const scrollTop = element.scrollTop;
    const atBottom = element.scrollHeight - scrollTop - element.clientHeight <= 2;
    if (scrollTop < instance.lastScrollTop - 1) instance.following = false;
    if (atBottom) instance.following = true;
    instance.lastScrollTop = scrollTop;
    notify(instance, atBottom);
  };
  element.addEventListener("scroll", instance.onScroll, { passive: true });

  const observeChildren = () => {
    if (!instance.resizeObserver) return;
    for (const child of instance.observedChildren) {
      if (child.parentElement !== element) {
        instance.resizeObserver.unobserve(child);
        instance.observedChildren.delete(child);
      }
    }
    for (const child of element.children || []) {
      if (!instance.observedChildren.has(child)) {
        instance.observedChildren.add(child);
        instance.resizeObserver.observe(child);
      }
    }
  };
  instance.mutationObserver = new MutationObserver(records => {
    // Streaming text and nested markup cannot change the direct children being observed.
    for (const record of records) {
      if (record.type === "childList" && record.target === element) {
        observeChildren();
        break;
      }
    }
    scheduleUpdate();
  });
  instance.mutationObserver.observe(element, { childList: true, subtree: true, characterData: true });

  if ("ResizeObserver" in window) {
    instance.resizeObserver = new ResizeObserver(scheduleUpdate);
    instance.resizeObserver.observe(element);
    observeChildren();
  }

  instances.set(element, instance);

  scheduleUpdate();
}

export function scrollToBottom(element, behavior = "smooth") {
  if (!element) {
    return;
  }

  scrollToBottomInternal(element, behavior);
  const instance = instances.get(element);
  if (instance) instance.following = true;
}

export function dispose(element) {
  const instance = instances.get(element);

  if (!instance) {
    return;
  }

  element.removeEventListener("scroll", instance.onScroll);
  instance.mutationObserver?.disconnect();
  instance.resizeObserver?.disconnect();
  if (instance.frame) {
    cancelAnimationFrame(instance.frame);
  }
  instances.delete(element);
}
