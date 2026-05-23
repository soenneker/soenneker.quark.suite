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
