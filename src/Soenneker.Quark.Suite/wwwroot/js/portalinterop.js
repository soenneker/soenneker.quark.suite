const mounts = new Map();
let automaticCount = 0;

function belongsToFullscreen(origin, fullscreen, visited = new Set()) {
    if (!origin || visited.has(origin)) return false;
    if (fullscreen.contains(origin)) return true;
    visited.add(origin);
    // A nested popup's original parent may itself have been portaled to body.
    for (const [element, mount] of mounts) {
        if (element.contains(origin) && belongsToFullscreen(mount.parent, fullscreen, visited)) return true;
    }
    return false;
}

function placeAutomaticPortal(element, mount) {
    const fullscreen = document.fullscreenElement;
    // Never move an ancestor of the fullscreen element into its own descendant.
    if (fullscreen && element.contains(fullscreen)) return;
    const target = fullscreen && belongsToFullscreen(mount.parent, fullscreen) ? fullscreen : document.body;
    if (!target || element.parentNode === target) return;
    const active = document.activeElement;
    const restoreFocus = active && element.contains(active);
    target.appendChild(element);
    if (restoreFocus && active.isConnected && document.activeElement !== active) active.focus({ preventScroll: true });
}

function fullscreenChanged() {
    for (const [element, mount] of mounts) {
        if (mount.automatic) placeAutomaticPortal(element, mount);
    }
}

export function mountPortal(element, containerSelector, containerElement) {
    if (!(element instanceof Node)) return;
    unmountPortal(element);
    const automatic = !(containerElement instanceof HTMLElement) && !containerSelector;
    const target = containerElement instanceof HTMLElement ? containerElement
        : containerSelector ? document.querySelector(containerSelector) : document.body;
    if (!(target instanceof Node) || element === target || element.contains(target)) return;
    const mount = { parent: element.parentNode, nextSibling: element.nextSibling, automatic };
    mounts.set(element, mount);
    if (automatic) {
        if (automaticCount++ === 0) document.addEventListener('fullscreenchange', fullscreenChanged);
        placeAutomaticPortal(element, mount);
    } else {
        target.appendChild(element);
    }
}

export function unmountPortal(element) {
    const mount = mounts.get(element);
    if (!mount) return;
    mounts.delete(element);
    if (mount.automatic && --automaticCount === 0) document.removeEventListener('fullscreenchange', fullscreenChanged);
    if (mount.parent) {
        if (mount.nextSibling?.parentNode === mount.parent) mount.parent.insertBefore(element, mount.nextSibling);
        else mount.parent.appendChild(element);
    }
}
