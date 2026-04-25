export function focusInputFromAddon(addon) {
  if (!addon) {
    return;
  }

  const target = document.activeElement instanceof HTMLElement ? document.activeElement : null;
  if (target?.closest("button")) {
    return;
  }

  addon.parentElement?.querySelector("input")?.focus();
}
