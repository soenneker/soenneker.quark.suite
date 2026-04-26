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

function notify(instance) {
  const next = isAtBottom(instance.element);

  if (next === instance.lastIsAtBottom) {
    return;
  }

  instance.lastIsAtBottom = next;
  instance.dotNetRef.invokeMethodAsync("SetIsAtBottom", next);
}

export function initialize(element, dotNetRef, initial = "smooth", resize = "smooth", stickToBottom = true) {
  if (!element) {
    return;
  }

  dispose(element);

  const instance = {
    element,
    dotNetRef,
    lastIsAtBottom: true,
    onScroll: null,
    mutationObserver: null,
    resizeObserver: null,
  };

  instance.onScroll = () => notify(instance);
  element.addEventListener("scroll", instance.onScroll, { passive: true });

  instance.mutationObserver = new MutationObserver(() => {
    if (stickToBottom && instance.lastIsAtBottom) {
      scrollToBottomInternal(element, resize);
    }

    notify(instance);
  });
  instance.mutationObserver.observe(element, { childList: true, subtree: true, characterData: true });

  if ("ResizeObserver" in window) {
    instance.resizeObserver = new ResizeObserver(() => {
      if (stickToBottom && instance.lastIsAtBottom) {
        scrollToBottomInternal(element, resize);
      }

      notify(instance);
    });
    instance.resizeObserver.observe(element);
  }

  instances.set(element, instance);

  requestAnimationFrame(() => {
    scrollToBottomInternal(element, initial);
    notify(instance);
  });
}

export function scrollToBottom(element, behavior = "smooth") {
  if (!element) {
    return;
  }

  scrollToBottomInternal(element, behavior);
}

export function dispose(element) {
  const instance = instances.get(element);

  if (!instance) {
    return;
  }

  element.removeEventListener("scroll", instance.onScroll);
  instance.mutationObserver?.disconnect();
  instance.resizeObserver?.disconnect();
  instances.delete(element);
}
