export function getItems(options) {
  const root = document.querySelector(options?.rootSelector || "[data-docs-content]");
  if (!root) {
    return [];
  }

  const headingSelector = options?.headingSelector || "h2[id], h3[id]";
  const ignoreSelector = options?.ignoreSelector || "[data-docs-ignore-toc]";

    const items = [];
    for (const heading of root.querySelectorAll(headingSelector)) {
        if (ignoreSelector && heading.closest(ignoreSelector)) continue;
        const id = heading.id;
        const title = heading.textContent?.trim();
        const level = Number(heading.tagName.substring(1));
        if (id && title && Number.isFinite(level)) items.push({ id, title, level });
    }
    return items;
}
