const registrations = new WeakMap();

function visibleTreeItems(tree) {
  return Array.from(tree.querySelectorAll('[role="treeitem"]')).filter((item) => {
    if (item.getAttribute("aria-disabled") === "true") {
      return false;
    }

    return item.offsetWidth > 0 || item.offsetHeight > 0 || item.getClientRects().length > 0;
  });
}

function setCurrent(items, item, focus = true) {
  for (const candidate of items) {
    candidate.tabIndex = candidate === item ? 0 : -1;
  }

  if (focus && item && typeof item.focus === "function") {
    item.focus({ preventScroll: true });
  }
}

function currentTreeItem(tree, items) {
  const active = document.activeElement?.closest?.('[role="treeitem"]');
  if (active && tree.contains(active) && items.includes(active)) {
    return active;
  }

  const tabStop = items.find((item) => item.tabIndex === 0);
  if (tabStop) {
    return tabStop;
  }

  return items.find((item) => item.getAttribute("aria-selected") === "true") ?? items[0] ?? null;
}

function normalizeTree(tree) {
  const items = visibleTreeItems(tree);
  if (items.length === 0) {
    return;
  }

  setCurrent(items, currentTreeItem(tree, items), false);
}

function parentTreeItem(item) {
  const group = item.parentElement?.closest('[role="group"]');
  return group?.parentElement?.closest('[role="treeitem"]') ?? null;
}

function firstChildTreeItem(item) {
  const group = Array.from(item.children).find((child) => child.getAttribute?.("role") === "group");
  return group?.querySelector('[role="treeitem"]:not([aria-disabled="true"])') ?? null;
}

function textValue(item) {
  return item.querySelector('[data-slot="tree-view-row"]')?.textContent?.trim().toLowerCase() ?? item.textContent?.trim().toLowerCase() ?? "";
}

function repeatedCharacterSearch(search) {
  return search.length > 1 && Array.from(search).every((char) => char === search[0]);
}

function typeaheadMatch(items, current, search) {
  if (!search) {
    return null;
  }

  const currentIndex = Math.max(items.indexOf(current), 0);
  const normalizedSearch = repeatedCharacterSearch(search) ? search[0] : search;
  const orderedItems = items.slice(currentIndex + 1).concat(items.slice(0, currentIndex + 1));

  return orderedItems.find((item) => textValue(item).startsWith(normalizedSearch)) ?? null;
}

function handleTypeahead(event, tree, current, items) {
  if (event.key.length !== 1 || event.altKey || event.ctrlKey || event.metaKey || event.key === " ") {
    return false;
  }

  const registration = registrations.get(tree);
  if (!registration) {
    return false;
  }

  window.clearTimeout(registration.typeaheadTimer);
  registration.typeaheadSearch = `${registration.typeaheadSearch ?? ""}${event.key.toLowerCase()}`;
  registration.typeaheadTimer = window.setTimeout(() => {
    registration.typeaheadSearch = "";
    registration.typeaheadTimer = 0;
  }, 1000);

  const next = typeaheadMatch(items, current, registration.typeaheadSearch);
  if (!next || next === current) {
    return false;
  }

  event.preventDefault();
  event.stopPropagation();
  setCurrent(items, next);
  return true;
}

function handleKeyDown(event) {
  const tree = event.currentTarget;
  const current = event.target?.closest?.('[role="treeitem"]');

  if (!current || !tree.contains(current)) {
    return;
  }

  const items = visibleTreeItems(tree);
  if (items.length === 0) {
    return;
  }

  if (handleTypeahead(event, tree, current, items)) {
    return;
  }

  let next = null;
  const index = items.indexOf(current);

  switch (event.key) {
    case "ArrowDown":
      next = items[Math.min(index + 1, items.length - 1)];
      break;
    case "ArrowUp":
      next = items[Math.max(index - 1, 0)];
      break;
    case "Home":
      next = items[0];
      break;
    case "End":
      next = items[items.length - 1];
      break;
    case "ArrowLeft":
      if (current.getAttribute("aria-expanded") !== "true") {
        next = parentTreeItem(current);
      }
      break;
    case "ArrowRight":
      if (current.getAttribute("aria-expanded") === "true") {
        next = firstChildTreeItem(current);
      }
      break;
    default:
      return;
  }

  if (!next || next === current) {
    return;
  }

  event.preventDefault();
  event.stopPropagation();
  setCurrent(items, next);
}

function handleFocusIn(event) {
  const tree = event.currentTarget;
  const current = event.target?.closest?.('[role="treeitem"]');

  if (!current || !tree.contains(current)) {
    return;
  }

  setCurrent(visibleTreeItems(tree), current, false);
}

export function initializeTree(tree) {
  if (!tree || registrations.has(tree)) {
    return;
  }

  const onKeyDown = (event) => handleKeyDown(event);
  const onFocusIn = (event) => handleFocusIn(event);
  let pendingNormalize = false;

  const observer = new MutationObserver(() => {
    if (pendingNormalize) {
      return;
    }

    pendingNormalize = true;
    queueMicrotask(() => {
      pendingNormalize = false;
      normalizeTree(tree);
    });
  });

  tree.addEventListener("keydown", onKeyDown, true);
  tree.addEventListener("focusin", onFocusIn);
  observer.observe(tree, { childList: true, subtree: true });

  normalizeTree(tree);

  registrations.set(tree, { onKeyDown, onFocusIn, observer });
}

export function destroyTree(tree) {
  const registration = registrations.get(tree);
  if (!tree || !registration) {
    return;
  }

  tree.removeEventListener("keydown", registration.onKeyDown, true);
  tree.removeEventListener("focusin", registration.onFocusIn);
  window.clearTimeout(registration.typeaheadTimer);
  registration.observer.disconnect();
  registrations.delete(tree);
}
