const registrations = new WeakMap();
const attachmentRegistrations = new WeakMap();

export function registerTextarea(textarea) {
    if (!textarea) {
        return;
    }

    unregisterTextarea(textarea);

    const keydown = event => {
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
        const attachments = Array.from(files || []).map(file => ({
            id: `${file.name}-${file.size}-${file.lastModified}-${Math.random().toString(36).slice(2)}`,
            name: file.name,
            size: file.size,
            type: file.type || null,
            lastModified: file.lastModified || 0
        }));

        if (attachments.length === 0) {
            return;
        }

        input.__quarkPromptInputLastAttachments = attachments;
        dotNetReference.invokeMethodAsync("AddAttachments", attachments)
            .then(() => {
                input.__quarkPromptInputAttachmentDone = true;
            })
            .catch(error => {
                input.__quarkPromptInputAttachmentError = String(error);
                console.error(error);
            });
    };

    const change = event => {
        addFiles(event.target.files);
        event.target.value = "";
    };

    const dragOver = event => {
        if (!hasFiles(event)) {
            return;
        }

        event.preventDefault();
    };

    const drop = event => {
        if (!hasFiles(event)) {
            return;
        }

        event.preventDefault();
        addFiles(event.dataTransfer.files);
    };

    input.addEventListener("change", change);

    const dropTarget = useGlobalDrop ? document : promptInput;
    dropTarget?.addEventListener("dragover", dragOver);
    dropTarget?.addEventListener("drop", drop);

    attachmentRegistrations.set(input, { change, dragOver, drop, dropTarget });
    input.__quarkPromptInputAttachmentsRegistered = true;
    input.__quarkPromptInputGlobalDrop = useGlobalDrop;
    input.__quarkPromptInputHandleDrop = drop;
}

export function unregisterAttachments(input) {
    if (!input) {
        return;
    }

    const registration = attachmentRegistrations.get(input);

    if (!registration) {
        return;
    }

    input.removeEventListener("change", registration.change);
    registration.dropTarget?.removeEventListener("dragover", registration.dragOver);
    registration.dropTarget?.removeEventListener("drop", registration.drop);
    attachmentRegistrations.delete(input);
    delete input.__quarkPromptInputAttachmentsRegistered;
    delete input.__quarkPromptInputGlobalDrop;
    delete input.__quarkPromptInputHandleDrop;
}

function hasFiles(event) {
    if (!event?.dataTransfer) {
        return false;
    }

    return Array.from(event.dataTransfer.types || []).includes("Files");
}
