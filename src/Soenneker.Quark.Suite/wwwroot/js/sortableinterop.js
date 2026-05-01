const sortableInstances = new WeakMap();
const keyboardRegistrations = new WeakMap();

export function ensureAvailable() {
  if (!window.Sortable) {
    throw new Error("SortableJS failed to load.");
  }
}

export function initializeList(element, disabled, sort, animation, forceFallback, itemSelector, handleSelector, filterSelector, group, notifyOnReorder, dotNetRef) {
  if (!element) {
    return;
  }

  ensureAvailable();
  destroy(element);

  const options = {
    animation: animation ?? 150,
    disabled: !!disabled,
    sort: sort !== false,
    forceFallback: forceFallback !== false,
    draggable: itemSelector || "[data-sortable-item]",
    onEnd: (evt) => {
      if (!notifyOnReorder) {
        return;
      }

      const reorderArgs = {
        oldIndex: evt.oldIndex ?? -1,
        newIndex: evt.newIndex ?? -1,
        oldDraggableIndex: evt.oldDraggableIndex ?? -1,
        newDraggableIndex: evt.newDraggableIndex ?? -1,
        itemId: evt.item?.getAttribute("data-sortable-id"),
        fromListId: evt.from?.getAttribute("data-sortable-list-id"),
        toListId: evt.to?.getAttribute("data-sortable-list-id"),
      };

      window.setTimeout(() => dotNetRef.invokeMethodAsync("HandleReorder", reorderArgs), Math.max(0, animation ?? 0));
    },
  };

  if (handleSelector) {
    options.handle = handleSelector;
  }

  if (filterSelector) {
    options.filter = filterSelector;
  }

  if (group) {
    options.group = group;
  }

  const instance = new window.Sortable(element, options);
  element.__quarkSortable = instance;
  sortableInstances.set(element, instance);
  registerKeyboardReorder(element, !!disabled, itemSelector || "[data-sortable-item]", handleSelector, notifyOnReorder, dotNetRef);
}

export function destroy(element) {
  if (!element) {
    return;
  }

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
    const activeItem = target?.closest?.(itemSelector);
    if (!activeItem || !element.contains(activeItem) || isDisabledItem(activeItem)) {
      return;
    }

    if (handleSelector && !target.closest(handleSelector)) {
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
      await dotNetRef.invokeMethodAsync("HandleReorder", {
        oldIndex,
        newIndex,
        oldDraggableIndex: oldIndex,
        newDraggableIndex: newIndex,
        itemId,
        fromListId: listId,
        toListId: listId,
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
  return Array.from(element.querySelectorAll(itemSelector)).filter(item => !isDisabledItem(item));
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
