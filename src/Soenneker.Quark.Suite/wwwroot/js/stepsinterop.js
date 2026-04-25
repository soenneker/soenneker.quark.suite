export function focusById(id) {
  if (!id) {
    return;
  }

  const element = document.getElementById(id);
  if (element && typeof element.focus === "function") {
    element.focus({ preventScroll: true });
  }
}
