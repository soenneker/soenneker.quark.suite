const { initialize: observe, destroy: stopObserving } = await import(new URL(import.meta.url).pathname.endsWith(".min.js") ? "./scrollrevealinterop.min.js" : "./scrollrevealinterop.js");

// Chart rows reveal once; CSS sequences the bars, markers, and values.
export function initialize(element) {
  observe(element, {
    once: true,
    threshold: 0.15,
    rootMargin: "0px 0px -24px 0px"
  });
}

export function destroy(element) {
  stopObserving(element);
  element?.removeAttribute("data-scroll-reveal-state");
}
