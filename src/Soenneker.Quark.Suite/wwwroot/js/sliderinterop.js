class SliderInterop {
  constructor() {
    this._activeDrag = null
  }

  _clamp(value, min, max) {
    return Math.min(Math.max(value, min), max)
  }

  _getRatio(trackElement, clientX, clientY, orientation) {
    if (!trackElement) {
      return 0
    }

    const rect = trackElement.getBoundingClientRect()

    if (orientation === "vertical") {
      if (rect.height <= 0) {
        return 0
      }

      return this._clamp((rect.bottom - clientY) / rect.height, 0, 1)
    }

    if (rect.width <= 0) {
      return 0
    }

    return this._clamp((clientX - rect.left) / rect.width, 0, 1)
  }

  getValueFromPointer(trackElement, clientX, clientY, min, max, orientation) {
    if (!trackElement || max <= min) {
      return min
    }

    const ratio = this._getRatio(trackElement, clientX, clientY, orientation)
    return min + (max - min) * ratio
  }

  _removeActiveDrag() {
    if (!this._activeDrag) {
      return
    }

    window.removeEventListener("pointermove", this._activeDrag.handlePointerMove)
    window.removeEventListener("pointerup", this._activeDrag.handlePointerEnd)
    window.removeEventListener("pointercancel", this._activeDrag.handlePointerEnd)
    this._activeDrag = null
  }

  stopDrag() {
    this._removeActiveDrag()
  }

  startDrag(trackElement, pointerId, clientX, clientY, min, max, orientation, dotNetRef, thumbIndex) {
    this._removeActiveDrag()

    const handlePointerMove = (event) => {
      if (pointerId != null && event.pointerId !== pointerId) {
        return
      }

      const value = this.getValueFromPointer(trackElement, event.clientX, event.clientY, min, max, orientation)
      dotNetRef.invokeMethodAsync("HandlePointerDragMove", thumbIndex, value)
    }

    const handlePointerEnd = (event) => {
      if (pointerId != null && event.pointerId !== pointerId) {
        return
      }

      const value = this.getValueFromPointer(trackElement, event.clientX, event.clientY, min, max, orientation)
      this._removeActiveDrag()
      dotNetRef.invokeMethodAsync("HandlePointerDragEnd", thumbIndex, value)
    }

    this._activeDrag = {
      handlePointerMove,
      handlePointerEnd,
    }

    window.addEventListener("pointermove", handlePointerMove)
    window.addEventListener("pointerup", handlePointerEnd)
    window.addEventListener("pointercancel", handlePointerEnd)

    return this.getValueFromPointer(trackElement, clientX, clientY, min, max, orientation)
  }
}

window.SliderInterop = new SliderInterop()
