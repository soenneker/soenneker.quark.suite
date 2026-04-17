const sortableInstances = new WeakMap();

export function ensureAvailable() {
  if (!window.Sortable) {
    throw new Error("SortableJS failed to load.");
  }
}

export function initializeList(element, disabled, sort, animation, forceFallback, itemSelector, handleSelector, filterSelector, group, dotNetRef) {
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
      dotNetRef.invokeMethodAsync("HandleReorder", {
        oldIndex: evt.oldIndex ?? -1,
        newIndex: evt.newIndex ?? -1,
        oldDraggableIndex: evt.oldDraggableIndex ?? -1,
        newDraggableIndex: evt.newDraggableIndex ?? -1,
        itemId: evt.item?.getAttribute("data-sortable-id"),
        fromListId: evt.from?.getAttribute("data-sortable-list-id"),
        toListId: evt.to?.getAttribute("data-sortable-list-id"),
      });
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
}

export function destroy(element) {
  if (!element) {
    return;
  }

  const instance = sortableInstances.get(element);

  if (!instance) {
    return;
  }

  instance.destroy();
  delete element.__quarkSortable;
  sortableInstances.delete(element);
}
