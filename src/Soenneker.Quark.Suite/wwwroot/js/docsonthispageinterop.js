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

  if (!headings.length || !("IntersectionObserver" in window)) {
    observers.set(id, { dispose: () => {} });
    return id;
  }

  const visible = new Map();
  let activeId = null;

  const setActive = (nextId) => {
    if (activeId === nextId) {
      return;
    }

    activeId = nextId;
    dotNetRef.invokeMethodAsync("SetActiveId", nextId);
  };

  const findNearestHeading = () => {
    const sorted = headings
      .map((heading) => ({
        id: heading.id,
        top: heading.getBoundingClientRect().top
      }))
      .sort((a, b) => a.top - b.top);

    const above = sorted.filter((item) => item.top <= 96).pop();
    return above?.id || sorted[0]?.id || null;
  };

  const observer = new IntersectionObserver(
    (entries) => {
      for (const entry of entries) {
        if (entry.isIntersecting) {
          visible.set(entry.target.id, entry.boundingClientRect.top);
        } else {
          visible.delete(entry.target.id);
        }
      }

      if (visible.size) {
        const nextId = Array.from(visible.entries()).sort((a, b) => a[1] - b[1])[0][0];
        setActive(nextId);
        return;
      }

      setActive(findNearestHeading());
    },
    {
      root: null,
      rootMargin: options?.rootMargin || "-80px 0px -70% 0px",
      threshold: [0, 1]
    }
  );

  headings.forEach((heading) => observer.observe(heading));
  setActive(findNearestHeading());

  observers.set(id, {
    dispose: () => observer.disconnect()
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
