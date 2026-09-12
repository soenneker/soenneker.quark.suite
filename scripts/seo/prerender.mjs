import assert from 'node:assert/strict';
import { createServer } from 'node:http';
import { readFile, writeFile, readdir, mkdir, stat, rm } from 'node:fs/promises';
import path from 'node:path';
import { fileURLToPath, pathToFileURL } from 'node:url';
import { chromium } from 'playwright';

const repository = fileURLToPath(new URL('../../', import.meta.url));
const output = path.resolve(repository, process.argv[2] ?? 'artifacts/cloudflare-workers/wwwroot');
const pagesDirectory = path.join(repository, 'test/Soenneker.Quark.Suite.Demo/Pages');
const origin = 'https://quark.soenneker.com';
// The demo's bootstrap URLs are not fingerprinted. Read its source shell so repeated local
// publishing cannot accidentally reuse the generated home page as every route's template.
const template = await readFile(path.join(repository, 'test/Soenneker.Quark.Suite.Demo/wwwroot/index.html'), 'utf8');
assert(template.includes('<div id="app">Loading...</div>'), 'The demo shell must contain the application placeholder.');
await stat(path.join(output, '_framework/blazor.webassembly.js'));
const escape = value => value.replaceAll('&', '&amp;').replaceAll('<', '&lt;').replaceAll('"', '&quot;');
const routes = new Map();
const aliases = new Map();
for (const file of (await readdir(pagesDirectory, { recursive: true })).filter(file => file.endsWith('.razor')).sort()) {
    const source = (await readFile(path.join(pagesDirectory, file), 'utf8')).replace(/^\uFEFF/, '');
    const excluded = /Sitemap\(Exclude\s*=\s*true\)/.test(source);
    const declaredRoutes = [...source.matchAll(/^@page\s+"([^"]+)"/gm)].map(match => match[1]);
    for (const [index, route] of declaredRoutes.entries()) {
        assert(!/[{}]/.test(route), `Add an explicit prerender strategy for parameterized route ${route}.`);
        assert(!routes.has(route) && !aliases.has(route), `Duplicate route: ${route}`);
        if (index > 0) {
            aliases.set(route, declaredRoutes[0]);
            continue;
        }
        routes.set(route, { indexable: !excluded && !route.startsWith('/test/') });
    }
}
assert(routes.has('/'), 'The home page must be present.');

const types = { '.html': 'text/html; charset=utf-8', '.js': 'text/javascript', '.mjs': 'text/javascript', '.json': 'application/json', '.wasm': 'application/wasm', '.css': 'text/css', '.svg': 'image/svg+xml', '.png': 'image/png', '.woff2': 'font/woff2', '.xml': 'application/xml', '.txt': 'text/plain' };
let published = false;
const server = createServer(async (request, response) => {
    try {
        const url = new URL(request.url, 'http://localhost');
        const pathname = decodeURIComponent(url.pathname);
        if (aliases.has(pathname)) {
            response.writeHead(301, { Location: aliases.get(pathname) + url.search }).end();
            return;
        }
        const relative = pathname.replace(/^\/+/, '');
        let filename = path.resolve(output, relative);
        if (filename !== output && !filename.startsWith(output + path.sep)) {
            response.writeHead(400).end();
            return;
        }
        if (routes.has(pathname)) {
            if (!published) {
                response.writeHead(200, { 'Content-Type': types['.html'] }).end(template);
                return;
            }
            filename = path.join(output, pathname === '/' ? 'index.html' : relative + '.html');
        }
        if ((await stat(filename).catch(() => null))?.isFile()) {
            response.writeHead(200, { 'Content-Type': types[path.extname(filename)] ?? 'application/octet-stream' });
            response.end(await readFile(filename));
        } else {
            response.writeHead(404, { 'Content-Type': types['.html'] });
            response.end(published ? await readFile(path.join(output, '404.html')) : 'Not found');
        }
    } catch (error) {
        response.writeHead(500).end(String(error));
    }
});
await new Promise(resolve => server.listen(0, '127.0.0.1', resolve));
const localOrigin = `http://127.0.0.1:${server.address().port}`;
let browser;
try {
    browser = await chromium.launch({ headless: true });
    const social = await browser.newPage({ viewport: { width: 1200, height: 630 }, deviceScaleFactor: 1 });
    await social.goto(pathToFileURL(path.join(repository, 'scripts/seo/social-card.html')).href);
    await social.screenshot({ path: path.join(output, 'social-card.png') });
    await social.close();
    const context = await browser.newContext({ viewport: { width: 1440, height: 1000 }, reducedMotion: 'reduce', colorScheme: 'light' });
    const page = await context.newPage();
    const snapshots = new Map();
    const titles = new Set();
    const descriptions = new Set();
    for (const [route, { indexable }] of routes) {
        if (snapshots.size === 0) {
            await page.goto(localOrigin + route, { waitUntil: 'domcontentloaded' });
        } else {
            await page.evaluate(route => window.Blazor.navigateTo(route), route);
        }
        await page.waitForFunction(route => window.quarkDemo?.renderedPath === route, route, { timeout: 60000 });
        await page.evaluate(() => new Promise(resolve => requestAnimationFrame(() => requestAnimationFrame(resolve))));
        const snapshot = await page.evaluate(() => {
            const app = document.querySelector('#app').cloneNode(true);
            // Blazor recreates the application root on startup. Do not retain runtime markers or injected scripts.
            app.querySelectorAll('script').forEach(element => element.remove());
            const comments = document.createTreeWalker(app, NodeFilter.SHOW_COMMENT);
            const remove = [];
            while (comments.nextNode()) remove.push(comments.currentNode);
            remove.forEach(comment => comment.remove());
            const head = [...document.head.querySelectorAll('[data-quark-seo]')].map(element => {
                const clone = element.cloneNode(true);
                clone.setAttribute('data-quark-prerendered', '');
                return clone.outerHTML;
            }).join('\n');
            return {
                title: document.title,
                description: document.head.querySelector('meta[name="description"]')?.content,
                canonical: document.head.querySelector('link[rel="canonical"]')?.href,
                robots: document.head.querySelector('meta[name="robots"]')?.content,
                h1: [...app.querySelectorAll('h1')].filter(heading => !heading.closest('[data-slot="component-preview"], [data-quark-example]')).length,
                textLength: app.textContent.trim().length,
                head,
                body: app.outerHTML,
                error: getComputedStyle(document.querySelector('#blazor-error-ui')).display !== 'none'
            };
        });
        assert(!snapshot.error, `Blazor failed on ${route}`);
        assert(snapshot.description?.length >= 50, `Missing description: ${route}`);
        assert(!titles.has(snapshot.title), `Duplicate title: ${route}`);
        assert(!descriptions.has(snapshot.description), `Duplicate description: ${route}`);
        titles.add(snapshot.title);
        descriptions.add(snapshot.description);
        if (indexable) {
            assert.equal(snapshot.canonical, origin + route, `Canonical mismatch: ${route}`);
            assert(!snapshot.robots.includes('noindex'), `Unexpected noindex: ${route}`);
            assert.equal(snapshot.h1, 1, `Expected one primary heading: ${route}`);
            assert(snapshot.textLength > 200, `Empty page: ${route}`);
        } else {
            assert(snapshot.robots.includes('noindex'), `Excluded route must be noindex: ${route}`);
        }
        const html = template.replace(/<title>.*?<\/title>/s, `<title>${escape(snapshot.title)}</title>\n${snapshot.head}`)
            .replace('<div id="app">Loading...</div>', () => snapshot.body.replaceAll(localOrigin, origin));
        snapshots.set(route, html);
        console.log(`Prerendered ${route}`);
    }
    for (const [route, html] of snapshots) {
        const filename = path.join(output, route === '/' ? 'index.html' : route.slice(1) + '.html');
        await mkdir(path.dirname(filename), { recursive: true });
        await writeFile(filename, html);
        // dotnet publish compressed the old shell. Never ship those stale representations.
        await Promise.all(['.br', '.gz'].map(suffix => rm(filename + suffix, { force: true })));
    }
    const publicRoutes = [...routes].filter(([, route]) => route.indexable).map(([route]) => route).sort();
    await writeFile(path.join(output, '_redirects'), [...aliases].map(([alias, canonical]) => `${alias} ${canonical} 301`).join('\n') + '\n');
    await writeFile(path.join(output, 'sitemap.xml'), `<?xml version="1.0" encoding="utf-8"?>\n<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">\n${publicRoutes.map(route => `  <url><loc>${origin}${escape(route)}</loc></url>`).join('\n')}\n</urlset>\n`);
    await Promise.all(['.br', '.gz'].map(suffix => rm(path.join(output, 'sitemap.xml' + suffix), { force: true })));
    await writeFile(path.join(output, '404.html'), `<!doctype html><html lang="en"><head><meta charset="utf-8"><meta name="viewport" content="width=device-width, initial-scale=1"><title>Page not found | Quark Suite</title><meta name="robots" content="noindex, follow"><base href="/"><link rel="stylesheet" href="css/quark-tailwind.min.css"><link rel="stylesheet" href="css/app.css"></head><body><main class="mx-auto max-w-4xl p-8"><h1 class="text-3xl font-semibold">Page not found</h1><p class="mt-4">This address does not match a Quark Suite documentation page.</p><nav aria-label="Documentation" class="mt-4"><a class="underline" href="/">Quark Suite home</a> · <a class="underline" href="/components">Blazor components</a> · <a class="underline" href="/installation">Installation guide</a></nav></main></body></html>`);
    published = true;

    // Verify the actual generated HTML with scripting disabled, then verify Blazor can take over it.
    const noJs = await browser.newContext({ javaScriptEnabled: false });
    const staticPage = await noJs.newPage();
    for (const route of ['/', '/installation', '/components', '/components/button', '/components/datatables', '/components/accordion']) {
        await staticPage.goto(localOrigin + route);
        assert.equal(await staticPage.locator('h1').count(), 1, `Static heading: ${route}`);
        assert.equal(await staticPage.locator('head link[rel="canonical"]').getAttribute('href'), origin + route);
        await page.goto(localOrigin + route, { waitUntil: 'domcontentloaded' });
        await page.waitForFunction(route => window.quarkDemo?.renderedPath === route, route);
        assert.equal(await page.locator('head meta[name="description"]').count(), 1, `Duplicate metadata after startup: ${route}`);
        assert.equal(await page.locator('head link[rel="canonical"]').count(), 1);
        assert.equal(await page.locator('head title').count(), 1);
        assert.equal(await page.locator('[data-quark-prerendered]').count(), 0);
    }
    const accordion = page.locator('[data-slot="accordion-trigger"]').first();
    const expanded = await accordion.getAttribute('aria-expanded');
    await accordion.click();
    await page.waitForFunction(previous => document.querySelector('[data-slot="accordion-trigger"]')?.getAttribute('aria-expanded') !== previous, expanded);
    // Exercise client navigation, including returning from a missing route and query/fragment canonicalization.
    await page.goto(localOrigin + '/installation');
    await page.waitForFunction(() => window.quarkDemo?.renderedPath === '/installation');
    await page.locator('a[href="components/button"]').first().click();
    await page.waitForFunction(() => document.querySelector('link[rel="canonical"]')?.href.endsWith('/components/button'));
    assert.equal(await page.locator('head meta[name="description"]').count(), 1);
    await page.goto(localOrigin + '/components/button?utm_source=seo-check#examples');
    await page.waitForFunction(() => window.quarkDemo?.renderedPath === '/components/button');
    assert.equal(await page.locator('head link[rel="canonical"]').getAttribute('href'), origin + '/components/button');
    await page.evaluate(() => window.Blazor.navigateTo('/does-not-exist-seo-check'));
    await page.waitForFunction(() => document.head.querySelector('meta[name="robots"]')?.content.includes('noindex'));
    assert.equal(await page.locator('head link[rel="canonical"]').count(), 0);
    await page.evaluate(() => window.Blazor.navigateTo('/components/button'));
    await page.waitForFunction(() => document.head.querySelector('meta[name="robots"]')?.content.startsWith('index'));
    assert.equal(await page.locator('head link[rel="canonical"]').getAttribute('href'), origin + '/components/button');
    for (const [alias, canonical] of aliases) {
        const response = await fetch(localOrigin + alias, { redirect: 'manual' });
        assert.equal(response.status, 301);
        assert.equal(response.headers.get('location'), canonical);
    }
    const missing = await staticPage.goto(localOrigin + '/does-not-exist-seo-check');
    assert.equal(missing.status(), 404);
    assert.match(await staticPage.locator('meta[name="robots"]').getAttribute('content'), /noindex/);
    assert.equal((await fetch(localOrigin + '/_framework/missing.wasm')).status, 404);
    await noJs.close();
    await context.close();
    console.log(`SEO verification passed: ${publicRoutes.length} indexable routes, static HTML, startup, navigation, canonical URLs, and 404s.`);
} finally {
    await browser?.close();
    server.closeAllConnections();
    await new Promise(resolve => server.close(resolve));
}
