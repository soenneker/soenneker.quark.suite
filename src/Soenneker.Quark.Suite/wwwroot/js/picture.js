// May also be loaded as an early classic script for prerendered/static pages.
(() => {
    if (globalThis.__quarkPictureObserver) return;
    const root = document.documentElement;
    const selector = 'picture source[data-quark-theme]';
    function synchronize(source) {
        const theme = source.getAttribute('data-quark-theme');
        const selected = root.classList.contains('dark') ? 'dark' : 'light';
        const media = theme === selected ? source.getAttribute('data-quark-media') || 'all' : 'not all';
        if (source.getAttribute('media') !== media) source.setAttribute('media', media);
    }
    function synchronizeTree(node) {
        if (node.nodeType !== Node.ELEMENT_NODE) return;
        if (node.matches(selector)) synchronize(node);
        node.querySelectorAll(selector).forEach(synchronize);
    }
    const observer = new MutationObserver(records => {
        for (const record of records) {
            if (record.type === 'childList') record.addedNodes.forEach(synchronizeTree);
            else if (record.target === root && record.attributeName === 'class') synchronizeTree(root);
            else if (record.target.matches(selector)) synchronize(record.target);
        }
    });
    observer.observe(root, { childList: true, subtree: true, attributes: true,
        attributeFilter: ['class', 'media', 'data-quark-theme', 'data-quark-media'] });
    globalThis.__quarkPictureObserver = observer;
    synchronizeTree(root);
})();
