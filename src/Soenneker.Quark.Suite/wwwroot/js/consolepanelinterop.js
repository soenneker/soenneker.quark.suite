export { updateViewport, jumpToLatest, initializeAutoScroll, destroyAutoScroll } from "./virtualviewport.js";

export function download(fileName, contentType, content) {
    const blob = new Blob([content ?? ""], { type: contentType || "text/plain;charset=utf-8" });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement("a");

    anchor.href = url;
    anchor.download = fileName || "console.log";
    anchor.style.display = "none";

    document.body.appendChild(anchor);
    anchor.click();
    anchor.remove();

    window.setTimeout(() => URL.revokeObjectURL(url), 1000);
}
