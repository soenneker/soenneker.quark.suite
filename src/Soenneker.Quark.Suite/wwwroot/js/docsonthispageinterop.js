const observers = new Map();
let nextObserverId = 0;

export function getItems(options) {
  const root = document.querySelector(options?.rootSelector || "[data-docs-content]");
  if (!root) {
    return [];
  }

  const headingSelector = options?.headingSelector || "h2[id], h3[id]";
  const ignoreSelector = options?.ignoreSelector || "[data-docs-ignore-toc]";

  return Array.from(root.querySelectorAll(headingSelector))
    .filter((heading) => !ignoreSelector || !heading.closest(ignoreSelector))
    .map((heading) => {
      const id = heading.id;
      const title = heading.textContent?.trim();
      const level = Number(heading.tagName.substring(1));

      return { id, title, level };
    })
    .filter((item) => item.id && item.title && Number.isFinite(item.level));
}

export function createActiveObserver(options, dotNetRef) {
  const items = getItems(options);
  const headings = items
    .map((item) => document.getElementById(item.id))
    .filter(Boolean);

  const id = `docs-on-this-page-${++nextObserverId}`;

  if (!headings.length) {
    observers.set(id, { dispose: () => {} });
    return id;
  }

  let activeId = null;
  let frame = 0;

  const setActive = (nextId) => {
    if (activeId === nextId) {
      return;
    }

    activeId = nextId;
    dotNetRef.invokeMethodAsync("SetActiveId", nextId);
  };

  const getOffset = () => {
    const headerHeight = getComputedStyle(document.documentElement).getPropertyValue("--header-height");
    const parsedHeaderHeight = Number.parseFloat(headerHeight);

    return (Number.isFinite(parsedHeaderHeight) ? parsedHeaderHeight : 72) + 24;
  };

  const isScrolledToBottom = () => {
    const scrollTop = window.scrollY || document.documentElement.scrollTop || 0;
    const viewportHeight = window.innerHeight || document.documentElement.clientHeight || 0;
    const scrollHeight = document.documentElement.scrollHeight || document.body.scrollHeight || 0;

    return scrollTop + viewportHeight >= scrollHeight - 2;
  };

  const findActiveHeading = () => {
    if (isScrolledToBottom()) {
      return headings[headings.length - 1]?.id || null;
    }

    const offset = getOffset();
    let active = headings[0]?.id || null;

    for (const heading of headings) {
      if (heading.getBoundingClientRect().top <= offset) {
        active = heading.id;
      } else {
        break;
      }
    }

    return active;
  };

  const updateActiveHeading = () => {
    frame = 0;
    setActive(findActiveHeading());
  };

  const scheduleUpdate = () => {
    if (frame) {
      return;
    }

    frame = window.requestAnimationFrame(updateActiveHeading);
  };

  window.addEventListener("scroll", scheduleUpdate, { passive: true });
  window.addEventListener("resize", scheduleUpdate);
  window.addEventListener("hashchange", scheduleUpdate);

  updateActiveHeading();

  observers.set(id, {
    dispose: () => {
      if (frame) {
        window.cancelAnimationFrame(frame);
      }

      window.removeEventListener("scroll", scheduleUpdate);
      window.removeEventListener("resize", scheduleUpdate);
      window.removeEventListener("hashchange", scheduleUpdate);
    }
  });

  return id;
}

export function disposeObserver(id) {
  const entry = observers.get(id);
  if (!entry) {
    return;
  }

  entry.dispose();
  observers.delete(id);
}
