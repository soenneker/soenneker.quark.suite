export function show(element) {
    if (!element.open)
        element.show();
}

export function showModal(element) {
    if (!element.open)
        element.showModal();
}

export function close(element, returnValue) {
    if (!element.open)
        return;

    if (returnValue == null)
        element.close();
    else
        element.close(returnValue);
}

export function getReturnValue(element) {
    return element.returnValue;
}
