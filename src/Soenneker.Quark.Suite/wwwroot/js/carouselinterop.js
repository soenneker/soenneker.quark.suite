const observers = new WeakMap();
const states = new WeakMap();

export function initialize(root, dotNetRef, currentIndex = 0, isVertical = false, align = "start") {
  if (!root || observers.has(root)) {
    return;
  }

  states.set(root, { dotNetRef, currentIndex, isVertical, align });

  const observer = new ResizeObserver(() => {
    const state = states.get(root);
    dotNetRef.invokeMethodAsync("OnCarouselMeasured", measureOffset(root, state?.currentIndex ?? 0, state?.isVertical ?? false, state?.align ?? "start"));
  });

  observer.observe(root);

  const viewport = root.querySelector("[data-slot='carousel-content']");
  if (viewport) {
    observer.observe(viewport);
  }

  observers.set(root, observer);
}

export function measureOffset(root, currentIndex = 0, isVertical = false, align = "start") {
  if (!root) {
    return 0;
  }

  const state = states.get(root);
  const index = Number.isFinite(currentIndex) ? currentIndex : state?.currentIndex ?? 0;

  if (state) {
    state.currentIndex = index;
    state.isVertical = Boolean(isVertical);
    state.align = align || "start";
  }

  if (!Number.isFinite(index) || index <= 0) {
    return 0;
  }

  const items = root.querySelectorAll("[data-slot='carousel-item']");
  const first = items[0];
  const target = items[index];

  if (!first || !target) {
    return 0;
  }

  let targetOffset = isVertical
    ? target.offsetTop - first.offsetTop
    : target.offsetLeft - first.offsetLeft;

  const viewport = root.querySelector("[data-slot='carousel-content']");
  const viewportSize = isVertical ? viewport?.clientHeight : viewport?.clientWidth;
  const itemSize = isVertical ? target.offsetHeight : target.offsetWidth;
  const alignMode = align || state?.align || "start";

  if (alignMode === "center" && viewportSize && itemSize && itemSize * 2 <= viewportSize + 8 && itemSize * 3 > viewportSize + 8) {
    targetOffset -= itemSize / 2;
  }

  return Math.abs(targetOffset);
}

export function dispose(root) {
  const observer = observers.get(root);

  if (observer) {
    observer.disconnect();
    observers.delete(root);
  }

  states.delete(root);
}
