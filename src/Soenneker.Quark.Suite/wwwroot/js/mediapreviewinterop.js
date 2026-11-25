export function create(bytes, contentType) {
    return URL.createObjectURL(new Blob([bytes], { type: contentType }));
}

export function release(url) {
    URL.revokeObjectURL(url);
}
