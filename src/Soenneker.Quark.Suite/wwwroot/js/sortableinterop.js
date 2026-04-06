class SortableInterop {
  constructor() {
    this._instances = new WeakMap()
  }

  ensureAvailable() {
    if (!window.Sortable) {
      throw new Error("SortableJS failed to load.")
    }
  }

  initializeList(element, disabled, sort, animation, itemSelector, handleSelector, filterSelector, group, dotNetRef) {
    if (!element) {
      return
    }

    this.ensureAvailable()
    this.destroy(element)

    const options = {
      animation: animation ?? 150,
      disabled: !!disabled,
      sort: sort !== false,
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
        })
      },
    }

    if (handleSelector) {
      options.handle = handleSelector
    }

    if (filterSelector) {
      options.filter = filterSelector
    }

    if (group) {
      options.group = group
    }

    const instance = new window.Sortable(element, options)
    this._instances.set(element, instance)
  }

  destroy(element) {
    if (!element) {
      return
    }

    const instance = this._instances.get(element)

    if (!instance) {
      return
    }

    instance.destroy()
    this._instances.delete(element)
  }
}

window.SortableInterop = new SortableInterop()
