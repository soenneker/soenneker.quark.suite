# Quark demo SEO publishing

The demo is a standalone Blazor WebAssembly application. Publishing only the .NET output gives every route an empty application shell. The deployment workflow now renders the application in Chromium and saves real HTML for each documentation page before uploading to Cloudflare.

```powershell
dotnet publish test/Soenneker.Quark.Suite.Demo/Soenneker.Quark.Suite.Demo.csproj --configuration Release --output artifacts/cloudflare-workers -p:UseLocalProjects=false
npm ci --prefix scripts/seo
npx --prefix scripts/seo playwright install chromium
node scripts/seo/prerender.mjs
```

Use Node 24 or later. On Linux, install Chromium system dependencies with `playwright install --with-deps chromium`. The script starts a temporary loopback server and closes the server and browser on completion or failure. It never deploys. Publish the current .NET code before running it; use a clean output directory when removing routes. CI starts from a clean checkout.

## Editing documentation

- Each routed page supplies a descriptive `Title` and `Description` to `SeoHead`. The shared component owns canonical URLs, Open Graph, Twitter cards, robots metadata, and JSON-LD. `HeadOutlet` is registered at `head::after` in `Program.cs`.
- Canonical URLs use HTTPS and omit query strings, fragments, and trailing slashes, except for `/`.
- For multiple `@page` directives, the first route is canonical. Set `CanonicalPath` on `SeoHead` to that route. The publishing script creates permanent redirects for the aliases and keeps them out of the sitemap.
- Test/demo harnesses use both `[Sitemap(Exclude = true)]` and `NoIndex="true"`. The prerender check enforces this agreement. Public pages need a documentation H1; headings inside a component preview are examples and are counted separately.
- Add public component pages to `DocsNavigation` so the sidebar and component index expose crawlable links. Do not add testing harnesses.
- `social-card.html` is the editable source of the 1200 × 630 social image. Publishing regenerates `social-card.png`. A checked-in copy under `wwwroot` also serves local development.

The generated HTML shows the same content to visitors and crawlers. Blazor replaces the saved application DOM on startup; `SeoHead` then removes only the marked static metadata after rendering its current metadata. This avoids duplicate or stale canonical tags during client navigation. This is build-time browser rendering, not Blazor server hydration.

## Deployment checks

The prerender step fails before deployment if a page cannot render or has missing/duplicate metadata, an incorrect canonical URL, unexpected indexing directives, or a missing documentation heading. It verifies representative pages with JavaScript disabled, then checks Blazor startup and client navigation. It writes a sitemap containing only canonical public pages, alias redirects, a real 404 page, and the social preview. Stale compressed shell/sitemap files from .NET publishing are removed.

Cloudflare uses `html_handling: drop-trailing-slash` and `not_found_handling: 404-page`. Deploy the complete generated output. Uploading the raw .NET publish folder without running the prerender step will break direct documentation URLs under this routing configuration.

After deployment, submit `https://quark.soenneker.com/sitemap.xml` in the site's existing Google Search Console property and inspect representative component URLs. Ranking and indexing changes require search-engine recrawling; the local checks cannot verify those outcomes.

References: [Google's JavaScript SEO guidance](https://developers.google.com/search/docs/crawling-indexing/javascript/javascript-seo-basics), [Cloudflare HTML routing](https://developers.cloudflare.com/workers/static-assets/routing/advanced/html-handling/).
