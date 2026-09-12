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
    frame: 0,
    initialPending: true,
  };

  const scheduleUpdate = () => {
    if (instance.frame) {
      return;
    }

    instance.frame = requestAnimationFrame(() => {
      instance.frame = 0;
      if (instances.get(element) !== instance) {
        return;
      }

      if (instance.initialPending || (stickToBottom && instance.lastIsAtBottom)) {
        scrollToBottomInternal(element, instance.initialPending ? initial : resize);
      }
      instance.initialPending = false;
      notify(instance);
    });
  };

  instance.onScroll = () => notify(instance);
  element.addEventListener("scroll", instance.onScroll, { passive: true });

  instance.mutationObserver = new MutationObserver(scheduleUpdate);
  instance.mutationObserver.observe(element, { childList: true, subtree: true, characterData: true });

  if ("ResizeObserver" in window) {
    instance.resizeObserver = new ResizeObserver(scheduleUpdate);
    instance.resizeObserver.observe(element);
  }

  instances.set(element, instance);

  scheduleUpdate();
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
  if (instance.frame) {
    cancelAnimationFrame(instance.frame);
  }
  instances.delete(element);
}
