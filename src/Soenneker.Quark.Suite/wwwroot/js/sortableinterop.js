import { getViewport, setDragging, scrollToIndex } from "./virtualviewport.js";

const sortableInstances = new WeakMap();
const keyboardRegistrations = new WeakMap();
const virtualRoots = new WeakMap();
const dragIds = new WeakMap();

export function ensureAvailable() {
  if (!window.Sortable) {
    throw new Error("SortableJS failed to load.");
  }
}

export async function waitForAvailable(timeoutMs = 5000) {
  const startedAt = performance.now();

  while (!window.Sortable) {
    if (performance.now() - startedAt >= timeoutMs) {
      throw new Error("SortableJS failed to load.");
    }

    await new Promise(resolve => window.setTimeout(resolve, 25));
  }
}

export function initializeList(element, disabled, sort, animation, forceFallback, itemSelector, handleSelector, filterSelector, group, notifyOnReorder, dotNetRef) {
  if (!element) {
    return;
  }

  ensureAvailable();
  destroy(element);

  const original = element;
  const virtual = element.querySelector('[data-virtual-sortable="true"]');
  if (virtual) {
    element = virtual;
    virtualRoots.set(original, virtual);
    itemSelector = '[data-virtual-key]';
    if (!notifyOnReorder) throw new Error('Virtual sortable lists require an OnReorder handler.');
  }
  element.__quarkItemSelector = itemSelector;
  const options = {
    animation: animation ?? 150,
    disabled: !!disabled,
    sort: sort !== false,
    forceFallback: forceFallback !== false,
    draggable: itemSelector || "[data-sortable-item]",
    onStart: (evt) => {
      dragIds.set(evt.from, getViewport(evt.from)?.keys.slice() ?? getDirectItemIds(evt.from, itemSelector));
      document.querySelectorAll('[data-virtual-sortable="true"]').forEach(host => setDragging(host, true));
    },
    onEnd: async (evt) => {
      document.querySelectorAll('[data-virtual-sortable="true"]').forEach(host => setDragging(host, false));
      if (!notifyOnReorder) {
        return;
      }

      const fromState = getViewport(evt.from), toState = getViewport(evt.to);
      if (fromState || toState) {
        const args = virtualPointerArgs(evt, fromState, toState, itemSelector);
        // Restore Blazor's DOM before the owner applies the model mutation.
        evt.item.remove();
        evt.from.insertBefore(evt.item, evt.from.children[evt.oldIndex] ?? null);
        await notifyReorder(dotNetRef, args);
        const state = getViewport(evt.to);
        if (state) scrollToIndex(evt.to, args.newIndex, handleSelector || '[data-virtual-key]');
      } else {
        await notifyReorder(dotNetRef, createPointerReorderArgs(evt, itemSelector || "[data-sortable-item]"));
      }
    },
  };

  if (handleSelector) {
    options.handle = handleSelector;
  }

  if (filterSelector || virtual) {
    options.filter = virtual ? [filterSelector, '[data-disabled="true"]'].filter(Boolean).join(',') : filterSelector;
  }

  if (group) {
    options.group = group;
  }

  const instance = new window.Sortable(element, options);
  element.__quarkSortable = instance;
  sortableInstances.set(element, instance);
  registerKeyboardReorder(element, !!disabled || sort === false, itemSelector || "[data-sortable-item]", handleSelector, notifyOnReorder, dotNetRef);
}

export function destroy(element) {
  if (!element) {
    return;
  }

  const original = element;
  element = virtualRoots.get(element) ?? element;
  virtualRoots.delete(original);
  const instance = sortableInstances.get(element);

  if (!instance) {
    unregisterKeyboardReorder(element);
    return;
  }

  instance.destroy();
  delete element.__quarkSortable;
  sortableInstances.delete(element);
  unregisterKeyboardReorder(element);
}

function registerKeyboardReorder(element, disabled, itemSelector, handleSelector, notifyOnReorder, dotNetRef) {
  unregisterKeyboardReorder(element);

  if (!element || disabled) {
    return;
  }

  const liveRegion = document.createElement("div");
  liveRegion.setAttribute("aria-live", "polite");
  liveRegion.setAttribute("aria-atomic", "true");
  liveRegion.dataset.slot = "sortable-live-region";
  liveRegion.style.position = "absolute";
  liveRegion.style.width = "1px";
  liveRegion.style.height = "1px";
  liveRegion.style.padding = "0";
  liveRegion.style.margin = "-1px";
  liveRegion.style.overflow = "hidden";
  liveRegion.style.clip = "rect(0, 0, 0, 0)";
  liveRegion.style.whiteSpace = "nowrap";
  liveRegion.style.border = "0";
  element.after(liveRegion);

  const normalizeFocusableItems = () => {
    if (handleSelector) {
      return;
    }

    getEnabledItems(element, itemSelector).forEach(item => {
      if (!item.hasAttribute("tabindex")) {
        item.setAttribute("tabindex", "0");
      }

      item.setAttribute("aria-keyshortcuts", "Space Enter ArrowUp ArrowDown Home End");
    });
  };

  normalizeFocusableItems();

  const observer = new MutationObserver(normalizeFocusableItems);
  observer.observe(element, { childList: true, subtree: true });

  const keydown = async event => {
    if (event.defaultPrevented || event.altKey || event.ctrlKey || event.metaKey) {
      return;
    }

    const key = event.key;
    if (key !== "ArrowUp" && key !== "ArrowDown" && key !== "Home" && key !== "End") {
      return;
    }

    const target = event.target;
    if (!handleSelector && target?.closest?.("input, textarea, select, [contenteditable=true]")) return;
    let activeItem = target?.closest?.(itemSelector);
    while (activeItem && activeItem.parentElement !== element) activeItem = activeItem.parentElement?.closest(itemSelector);
    if (!activeItem || !element.contains(activeItem) || isDisabledItem(activeItem)) {
      return;
    }

    if (handleSelector && !target.closest(handleSelector)) {
      return;
    }

    const viewport = getViewport(element);
    if (viewport) {
      const itemId = activeItem.getAttribute('data-sortable-id');
      const oldIndex = viewport.keys.indexOf(itemId);
      let newIndex = key === 'Home' ? 0 : key === 'End' ? viewport.keys.length - 1 :
        Math.max(0, Math.min(viewport.keys.length - 1, oldIndex + (key === 'ArrowUp' ? -1 : 1)));
      const direction = key === 'Home' || key === 'ArrowDown' ? 1 : -1;
      while (newIndex >= 0 && newIndex < viewport.keys.length && viewport.disabledKeys?.has(viewport.keys[newIndex])) newIndex += direction;
      if (oldIndex < 0 || oldIndex === newIndex || newIndex < 0 || newIndex >= viewport.keys.length) return;
      event.preventDefault(); event.stopPropagation();
      const ids = [...viewport.keys];
      ids.splice(newIndex, 0, ...ids.splice(oldIndex, 1));
      const listId = element.getAttribute('data-sortable-list-id');
      await notifyReorder(dotNetRef, { oldIndex, newIndex, oldDraggableIndex: oldIndex, newDraggableIndex: newIndex,
        itemId, fromListId: listId, toListId: listId, fromItemIds: ids, toItemIds: ids });
      liveRegion.textContent = `${itemId} moved to position ${newIndex + 1}.`;
      scrollToIndex(element, newIndex, handleSelector || '[data-virtual-key]');
      return;
    }
    const items = getEnabledItems(element, itemSelector);
    const oldIndex = items.indexOf(activeItem);
    if (oldIndex < 0) {
      return;
    }

    let newIndex = oldIndex;
    if (key === "ArrowUp") {
      newIndex = Math.max(0, oldIndex - 1);
    } else if (key === "ArrowDown") {
      newIndex = Math.min(items.length - 1, oldIndex + 1);
    } else if (key === "Home") {
      newIndex = 0;
    } else if (key === "End") {
      newIndex = items.length - 1;
    }

    if (newIndex === oldIndex) {
      return;
    }

    event.preventDefault();
    event.stopPropagation();

    const itemId = activeItem.getAttribute("data-sortable-id");
    const listId = element.getAttribute("data-sortable-list-id");
    liveRegion.textContent = `${itemId || "Item"} moved from position ${oldIndex + 1} to ${newIndex + 1}.`;

    if (notifyOnReorder) {
      const orderedItems = reorderItems(items, oldIndex, newIndex);
      const orderedItemIds = getItemIds(orderedItems);

      await notifyReorder(dotNetRef, {
        oldIndex,
        newIndex,
        oldDraggableIndex: oldIndex,
        newDraggableIndex: newIndex,
        itemId,
        fromListId: listId,
        toListId: listId,
        fromItemIds: orderedItemIds,
        toItemIds: orderedItemIds,
      });
    } else {
      const reference = newIndex > oldIndex ? items[newIndex].nextSibling : items[newIndex];
      element.insertBefore(activeItem, reference);
    }

    requestAnimationFrame(() => {
      const selector = itemId ? `[data-sortable-id="${cssEscape(itemId)}"]` : itemSelector;
      const movedItem = element.querySelector(selector);
      const focusTarget = handleSelector ? movedItem?.querySelector(handleSelector) : movedItem;
      focusTarget?.focus?.({ preventScroll: true });
    });
  };

  element.addEventListener("keydown", keydown);
  keyboardRegistrations.set(element, { keydown, observer, liveRegion });
}

function unregisterKeyboardReorder(element) {
  const registration = keyboardRegistrations.get(element);
  if (!registration) {
    return;
  }

  element.removeEventListener("keydown", registration.keydown);
  registration.observer.disconnect();
  registration.liveRegion.remove();
  keyboardRegistrations.delete(element);
}

function getEnabledItems(element, itemSelector) {
    const items = [];
    for (const item of element.children) {
        if (item.matches(itemSelector) && !isDisabledItem(item)) items.push(item);
    }
    return items;
}

function createPointerReorderArgs(evt, itemSelector) {
  return {
    oldIndex: evt.oldIndex ?? -1,
    newIndex: evt.newIndex ?? -1,
    oldDraggableIndex: evt.oldDraggableIndex ?? -1,
    newDraggableIndex: evt.newDraggableIndex ?? -1,
    itemId: evt.item?.getAttribute("data-sortable-id"),
    fromListId: evt.from?.getAttribute("data-sortable-list-id"),
    toListId: evt.to?.getAttribute("data-sortable-list-id"),
    fromItemIds: getDirectItemIds(evt.from, itemSelector),
    toItemIds: getDirectItemIds(evt.to, itemSelector),
  };
}

function getDirectItemIds(element, itemSelector) {
  if (!element) {
    return [];
  }

    const ids = [];
    for (const item of element.children) {
        if (!item.matches(itemSelector) || isDisabledItem(item)) continue;
        const id = item.getAttribute("data-sortable-id");
        if (id) ids.push(id);
    }
    return ids;
}

function getItemIds(items) {
    const ids = [];
    for (const item of items) {
        const id = item.getAttribute("data-sortable-id");
        if (id) ids.push(id);
    }
    return ids;
}

function reorderItems(items, oldIndex, newIndex) {
    const item = items[oldIndex];
    if (newIndex < oldIndex) {
        for (let index = oldIndex; index > newIndex; index--) items[index] = items[index - 1];
    } else {
        for (let index = oldIndex; index < newIndex; index++) items[index] = items[index + 1];
    }
    items[newIndex] = item;
    return items;
}

function notifyReorder(dotNetRef, reorderArgs) {
  return dotNetRef.invokeMethodAsync("HandleReorder", reorderArgs);
}

function isDisabledItem(item) {
  return item.matches("[data-disabled='true'], [aria-disabled='true']");
}

function cssEscape(value) {
  if (window.CSS?.escape) {
    return window.CSS.escape(value);
  }

  return value.replace(/["\\]/g, "\\$&");
}

function virtualPointerArgs(evt, fromState, toState, itemSelector) {
    const itemId = evt.item.getAttribute('data-sortable-id');
    const from = fromState ? [...fromState.keys] : [...(dragIds.get(evt.from) ?? getDirectItemIds(evt.from, evt.from.__quarkItemSelector || itemSelector))];
    const to = evt.from === evt.to ? from : toState ? [...toState.keys] : getDirectItemIds(evt.to, evt.to.__quarkItemSelector || itemSelector).filter(id => id !== itemId);
    const oldIndex = from.indexOf(itemId);
    if (oldIndex >= 0) from.splice(oldIndex, 1);
    const destinationSelector = evt.to.__quarkItemSelector || itemSelector;
    const next = evt.item.nextElementSibling?.matches(destinationSelector) ? evt.item.nextElementSibling : null;
    const previous = evt.item.previousElementSibling?.matches(destinationSelector) ? evt.item.previousElementSibling : null;
    let newIndex = next ? to.indexOf(next.getAttribute('data-sortable-id')) : previous ? to.indexOf(previous.getAttribute('data-sortable-id')) + 1 : toState?.range?.[0] ?? 0;
    newIndex = Math.max(0, Math.min(to.length, newIndex));
    to.splice(newIndex, 0, itemId);
    return { oldIndex, newIndex, oldDraggableIndex: oldIndex, newDraggableIndex: newIndex, itemId,
        fromListId: evt.from.getAttribute('data-sortable-list-id'), toListId: evt.to.getAttribute('data-sortable-list-id'), fromItemIds: from, toItemIds: to };
}
