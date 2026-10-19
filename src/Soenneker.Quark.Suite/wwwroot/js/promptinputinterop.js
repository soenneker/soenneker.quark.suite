const registrations = new WeakMap();
const attachmentRegistrations = new WeakMap();

export function registerTextarea(textarea) {
    if (!textarea) {
        return;
    }

    unregisterTextarea(textarea);

    const keydown = event => {
        if (event.isComposing || event.keyCode === 229) {
            event.stopPropagation();
            return;
        }

        if (event.key !== "Enter" || event.shiftKey || event.altKey || event.ctrlKey || event.metaKey || event.defaultPrevented) {
            return;
        }

        event.preventDefault();
    };

    textarea.addEventListener("keydown", keydown);
    registrations.set(textarea, keydown);
}

export function unregisterTextarea(textarea) {
    if (!textarea) {
        return;
    }

    const keydown = registrations.get(textarea);

    if (!keydown) {
        return;
    }

    textarea.removeEventListener("keydown", keydown);
    registrations.delete(textarea);
}

export function openFileDialog(input) {
    if (!input) {
        return;
    }

    input.click();
}

export function openFileDialogById(id) {
    openFileDialog(document.getElementById(id));
}

export function registerAttachmentsById(id, dotNetReference, globalDrop) {
    registerAttachments(document.getElementById(id), dotNetReference, globalDrop);
}

export function unregisterAttachmentsById(id) {
    unregisterAttachments(document.getElementById(id));
}

export function registerAttachments(input, dotNetReference, globalDrop) {
    if (!input) {
        return;
    }

    unregisterAttachments(input);
    const promptInput = input.closest("[data-slot='prompt-input']");
    const useGlobalDrop = globalDrop || promptInput?.dataset.globalDrop === "true";

    const addFiles = files => {
        if (input.disabled) return;

        const attachments = Array.from(files || [], file => ({
            name: file.name,
            size: file.size,
            type: file.type || null,
            lastModified: file.lastModified || 0
        }));

        if (attachments.length === 0) {
            return;
        }

        dotNetReference.invokeMethodAsync("AddAttachments", attachments).catch(console.error);
    };

    const dragOver = event => {
        if (input.disabled || !hasFiles(event)) {
            return;
        }

        event.preventDefault();
    };

    const drop = event => {
        if (input.disabled || !hasFiles(event)) {
            return;
        }

        event.preventDefault();
        addFiles(event.dataTransfer.files);
    };

    const dropTarget = useGlobalDrop ? document : promptInput;
    dropTarget?.addEventListener("dragover", dragOver);
    dropTarget?.addEventListener("drop", drop);

    attachmentRegistrations.set(input, { dragOver, drop, dropTarget });
}

export function unregisterAttachments(input) {
    if (!input) {
        return;
    }

    const registration = attachmentRegistrations.get(input);

    if (!registration) {
        return;
    }

    registration.dropTarget?.removeEventListener("dragover", registration.dragOver);
    registration.dropTarget?.removeEventListener("drop", registration.drop);
    attachmentRegistrations.delete(input);
}

function hasFiles(event) {
    if (!event?.dataTransfer) {
        return false;
    }

    const types = event.dataTransfer.types;
    if (!types) return false;
    for (let index = 0; index < types.length; index++) if (types[index] === "Files") return true;
    return false;
}
