export function getBrowserTimeZone() {
    try {
        const options = Intl.DateTimeFormat().resolvedOptions();
        return options.timeZone || null;
    } catch {
        return null;
    }
}
