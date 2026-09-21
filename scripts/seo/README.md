# Quark demo SEO

The demo runs as a client-rendered Blazor WebAssembly application. The deployment workflow publishes the .NET application directly to Cloudflare with `not_found_handling: single-page-application`, so documentation URLs load the application shell and Blazor handles routing.

```powershell
dotnet publish test/Soenneker.Quark.Suite.Demo/Soenneker.Quark.Suite.Demo.csproj --configuration Release --output artifacts/cloudflare-workers -p:UseLocalProjects=false
```

## Editing documentation

- Each routed page supplies a descriptive `Title` and `Description` to the app-specific `QuarkSeoHead` wrapper. It supplies Quark branding, canonical URLs, and structured data to the public `Soenneker.Quark.SeoHead` component, which renders the head metadata. `HeadOutlet` is registered at `head::after` in `Program.cs`.
- Canonical URLs use HTTPS and omit query strings, fragments, and trailing slashes, except for `/`. Set `CanonicalPath` when multiple routes share one documentation page. Permanent alias redirects are maintained in `wwwroot/_redirects`.
- Test/demo harnesses use both `[Sitemap(Exclude = true)]` and `NoIndex="true"`. Blazor's not-found view also supplies `noindex` metadata. With SPA hosting, the HTTP response for an unknown application route remains the application shell.
- Add public component pages to `DocsNavigation` so the sidebar and component index expose links. Do not add testing harnesses.
- The existing Razor sitemap generator produces `wwwroot/sitemap.xml` during the .NET build.
- `social-card.html` is the design source for the 1200 by 630 image checked in at `wwwroot/social-card.png`. Update the PNG when changing that design; publishing copies it as a normal static asset.

Page-specific metadata and documentation content appear when Blazor runs. The deployment pipeline does not run a browser or generate page snapshots.

## SEO title budget

Use "Blazor" when it identifies the framework for a component or example. Omit it from branded setup guides, styling references, migration notes, and test harnesses where the topic and Quark Suite branding are sufficient. Keep titles specific to the page rather than adding a universal keyword prefix.

Keep the final rendered SEO title at or below 60 characters, including spaces, punctuation, and the brand suffix. Count decoded text (for example, `&amp;` counts as one character). Rewrite overlong titles; do not blindly truncate them or reject page rendering. Check dynamically composed titles too, including chart categories and blog article titles. This is an editorial budget: Google truncates title links to fit the available display width, not a fixed character count.
