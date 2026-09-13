# Quark demo SEO

The demo runs as a client-rendered Blazor WebAssembly application. The deployment workflow publishes the .NET application directly to Cloudflare with `not_found_handling: single-page-application`, so documentation URLs load the application shell and Blazor handles routing.

```powershell
dotnet publish test/Soenneker.Quark.Suite.Demo/Soenneker.Quark.Suite.Demo.csproj --configuration Release --output artifacts/cloudflare-workers -p:UseLocalProjects=false
```

## Editing documentation

- Each routed page supplies a descriptive `Title` and `Description` to `SeoHead`. The shared component owns canonical URLs, Open Graph, Twitter cards, robots metadata, and JSON-LD. `HeadOutlet` is registered at `head::after` in `Program.cs`.
- Canonical URLs use HTTPS and omit query strings, fragments, and trailing slashes, except for `/`. Set `CanonicalPath` when multiple routes share one documentation page. Permanent alias redirects are maintained in `wwwroot/_redirects`.
- Test/demo harnesses use both `[Sitemap(Exclude = true)]` and `NoIndex="true"`. Blazor's not-found view also supplies `noindex` metadata. With SPA hosting, the HTTP response for an unknown application route remains the application shell.
- Add public component pages to `DocsNavigation` so the sidebar and component index expose links. Do not add testing harnesses.
- The existing Razor sitemap generator produces `wwwroot/sitemap.xml` during the .NET build.
- `social-card.html` is the design source for the 1200 by 630 image checked in at `wwwroot/social-card.png`. Update the PNG when changing that design; publishing copies it as a normal static asset.

Page-specific metadata and documentation content appear when Blazor runs. The deployment pipeline does not run a browser or generate page snapshots.
