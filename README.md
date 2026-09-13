[![](https://img.shields.io/nuget/v/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/publish-package.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/build-and-test.yml?label=Build&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/nuget/dt/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://quark.soenneker.com/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/codeql.yml)

# Quark Suite

**Build Blazor interfaces with the composition and visual language of shadcn/ui, using C# and Razor.**

Quark gives you ready-to-use components for application layouts, forms, dialogs, data tables, charts, navigation, and AI/chat interfaces. Start with sensible defaults, compose the pieces you need, and customize them with typed Tailwind builders or ordinary CSS classes.

[Explore the components](https://quark.soenneker.com/components) · [Install Quark](https://quark.soenneker.com/installation) · [Build your first component](https://quark.soenneker.com/first-component) · [Understand the generators](https://quark.soenneker.com/generators)

## Why Quark?

- **Compose real application UI.** A card has a header, content, and actions; a dialog has a trigger and content. Work with those parts directly in Razor.
- **Keep styling close to the component.** Use typed values such as `Padding.OnX.Is4.OnY.Is2`, responsive modifiers, semantic colors, and component variants. Use `Class` and `Style` for custom designs.
- **Build with Blazor conventions.** Bind values, handle events in C#, and use your existing services. Bradix supplies interaction primitives for focus management, overlays, and composite controls; it is included as a dependency.
- **Let the build prepare the assets.** Generators produce the Tailwind stylesheet, selected icon SVGs, and optional theme CSS. You do not need to set up a separate npm workflow to get started.
- **Own the application's visual identity.** Start with shadcn-compatible tokens, then define light/dark colors and component styling in a shared C# theme when your app needs it.

Quark is an MIT-licensed NuGet component library. It takes design inspiration from shadcn/ui while using Razor composition and .NET APIs. It does not require a React application or a shadcn CLI setup.

## Quick start

Use an interactive Blazor application targeting **.NET 10**. The walkthrough below uses a standalone Blazor WebAssembly app, like the Quark demo. Run package commands in the app project directory.

### 1. Add the core packages

```shell
dotnet add package Soenneker.Quark.Suite
dotnet add package Soenneker.Quark.Gen.Tailwind
dotnet add package Soenneker.Quark.Gen.Tailwind.Manifest
dotnet add package Soenneker.Quark.Gen.Lucide
dotnet add package Soenneker.Lucide.Icons
```

The suite contains the UI. The Tailwind packages produce the CSS. The Lucide generator produces the application's icon provider; Lucide is also used by Quark controls. Brand icons and C# theme generation can be added later.

Reference the generators directly in the application project. Do not assume the suite's private build-time dependencies will run for your app. If you package a reusable library, mark its generator references with `PrivateAssets="all"`, as the Quark and Soenneker demos do.

### 2. Enable CSS generation

Add this property group to the app's `.csproj`:

```xml
<PropertyGroup>
  <TailwindGeneratorBuildEnabled>true</TailwindGeneratorBuildEnabled>
</PropertyGroup>
```

**You do not need to install Node.js or npm manually.** The Tailwind build uses `Soenneker.Node.Util` to locate or provision the toolchain and install its npm dependencies. The first build needs network access for any missing tools and packages, plus permission to write its build/cache files. Later builds reuse the installed dependencies and skip unchanged generator work.

### 3. Register Quark and the icon provider

In `Program.cs`, after creating the builder and before building the host:

```csharp
using Soenneker.Quark;
using Soenneker.Quark.Gen.Lucide.Generated;

// After creating builder, before builder.Build():
builder.Services.AddQuarkSuiteAsScoped();
builder.Services.AddLucideIconsAsScoped();
```

### 4. Import the components and load the stylesheet

Add to `_Imports.razor`:

```razor
@using Soenneker.Quark
@using Soenneker.Lucide.Enums.Icons
```

Add to the document head in `wwwroot/index.html`:

```html
<link rel="stylesheet" href="css/quark-tailwind.min.css" />
```

Then run `dotnet build`. The generator creates both `wwwroot/css/quark-tailwind.css` and `wwwroot/css/quark-tailwind.min.css`; load one of them. With a Blazor Web App, load the stylesheet in its document shell (usually `Components/App.razor`) and register services in the host that executes the interactive components. Components that handle input and events need an interactive render mode.

### 5. Build something you can click

Create `Pages/QuarkStart.razor`, paste the complete example below, run the app, and open `/quark-start`:

```razor
@page "/quark-start"
@using Soenneker.Quark
@using Soenneker.Lucide.Enums.Icons

<Card Class="max-w-md">
    <CardHeader>
        <CardTitle>Your first Quark component</CardTitle>
        <CardDescription>Give your workspace a name.</CardDescription>
    </CardHeader>
    <CardContent>
        <Field>
            <FieldLabel For="workspace-name">Workspace name</FieldLabel>
            <Input Id="workspace-name" @bind-Value="_name" />
        </Field>
    </CardContent>
    <CardFooter>
        <Button OnClick="Save">
            <Icon Name="LucideIcon.Check" aria-hidden="true" />
            Save workspace
        </Button>
    </CardFooter>
</Card>
<p role="status">@_message</p>

@code {
    private string? _name = "My workspace";
    private string _message = "Changes are saved only in this example.";

    private void Save()
    {
        _message = $"Saved: {_name}";
    }
}
```

You should see a styled card, a labelled input, a check icon, and a Save button that updates the status message. This checks the stylesheet, generated icon registration, value binding, and event handling in one small example. See the [live walkthrough](https://quark.soenneker.com/first-component).

## What each generator does

The generators have different jobs. A typical app starts with Tailwind, Tailwind.Manifest, and Lucide. Add SimpleIcons for brand logos and Themes for a C# theme. The remaining generators primarily build Quark's reusable APIs.

| Package | Who uses it? | Job |
| --- | --- | --- |
| [Soenneker.Quark.Gen.Tailwind](https://github.com/soenneker/soenneker.quark.gen.tailwind) | Application | Provisions the Node/npm toolchain as needed, installs Tailwind dependencies, combines sources and theme tokens, and writes the full and minified app stylesheets. Enable TailwindGeneratorBuildEnabled. |
| [Soenneker.Quark.Gen.Tailwind.Manifest](https://github.com/soenneker/soenneker.quark.gen.tailwind.manifest) | Application / source project | Finds utilities composed by C# builders and writes tailwind/quark-tailwind-manifest.txt. This supplies complete class names that Tailwind cannot read from a fluent expression. Enabled by default. |
| [Soenneker.Quark.Gen.Lucide](https://github.com/soenneker/soenneker.quark.gen.lucide) | Application | Scans source for Lucide icon references and generates an SVG map, provider, and AddLucideIconsAsScoped(). Soenneker.Lucide.Icons supplies the SVG catalog. |
| [Soenneker.Quark.Gen.SimpleIcons](https://github.com/soenneker/soenneker.quark.gen.simpleicons) | Optional: brand icons | Generates the brand-icon map, provider, and AddSimpleIconsAsScoped(). Pair with Soenneker.SimpleIcons.Icons and the SimpleIcon component. |
| [Soenneker.Quark.Gen.Themes](https://github.com/soenneker/soenneker.quark.gen.themes) | Optional: a C# theme | Executes an attributed Theme factory during the build; writes runtime theme CSS plus Tailwind token CSS. Useful for shared application colors and component styling. |
| [Soenneker.Quark.Gen.Tailwind.Manifest.Suite](https://github.com/soenneker/soenneker.quark.gen.tailwind.manifest.suite) | Library authors | Builds the suite-wide class manifest shipped with Quark, including component defaults and variants. The app consumes the packaged manifest; it does not normally install this generator. |
| [Soenneker.Quark.Gen.Presets](https://github.com/soenneker/soenneker.quark.gen.presets) | Preset authors | Creates typed QuarkPresets registry tokens from classes marked with QuarkPreset. Quark already ships its built-in tokens. Add it when generating your own preset registry. |
| [Soenneker.Quark.Gen.Tailwind.Modifiers](https://github.com/soenneker/soenneker.quark.gen.tailwind.modifiers) | Builder-library authors | Generates modifier entry points such as OnHover, OnMd, and OnDark for partial builder entry classes. Applications already receive those APIs through Soenneker.Quark.Builders. |

```text
Your Razor + C# builders  -> Tailwind.Manifest -> app class manifest
Quark's NuGet package     -> suite class manifest
Theme JSON or C# factory  -> theme token CSS
                         -> Tailwind build -> quark-tailwind.min.css

Your LucideIcon references -> Gen.Lucide -> SVG map + provider + DI extension
Your brand icon references -> Gen.SimpleIcons -> SVG map + provider + DI extension
```

A class manifest is a list of complete Tailwind utilities, not a stylesheet. The suite manifest protects Quark's built-in styles; the app manifest adds the utilities created by your builder expressions. The Tailwind generator combines them with the app's literal classes and theme input.

The icon catalog packages (`Soenneker.Lucide.Icons` and `Soenneker.SimpleIcons.Icons`) provide SVG source assets. Their enum packages provide typed names. The generators create the app-local maps and DI registrations. These are separate responsibilities; installing a catalog alone does not register the generated provider.

For configuration, build outputs, shared libraries, and dynamic values, see the [generator guide](https://quark.soenneker.com/generators).

## Add brand icons when you need them

```shell
dotnet add package Soenneker.Quark.Gen.SimpleIcons
dotnet add package Soenneker.SimpleIcons.Icons
```

```csharp
using Soenneker.Quark.Gen.SimpleIcons.Generated;

builder.Services.AddSimpleIconsAsScoped();
```

```razor
@using static Soenneker.SimpleIcons.Enums.Icons.SimpleIconEnum

<SimpleIcon Name="Github" aria-label="GitHub" />
```

The static import brings enum members such as `Github` into scope, and the literal `Name` lets the generator discover the SVG. Keep this import in the page that needs it. If runtime logic chooses an icon, ensure each allowed icon also appears in a literal `Name` attribute in source. Do not assume arbitrary enum aliases or configuration strings are discovered.

## Style components without leaving Razor

```razor
<Div Padding="Padding.OnX.Is4.OnY.Is2"
     FlexDirection="FlexDirection.Col.OnMd.Row"
     Display="Display.Flex"
     Gap="Gap.Is3">
    Content
</Div>
```

Read builder chains left to right: **a modifier applies to the next value**. `Padding.OnX.Is4.OnY.Is2` emits `px-4 py-2`; `FlexDirection.Col.OnMd.Row` emits `flex-col md:flex-row`. Do not put a trailing modifier after the value you intend it to affect.

Use component variants for normal choices, typed properties for common utilities, and `Class` for exact Tailwind expressions. Prefer semantic colors such as `BackgroundColor.Card` and `TextColor.CardForeground` so light and dark themes can share the same markup.

[Styling and properties](https://quark.soenneker.com/properties) · [Responsive grids](https://quark.soenneker.com/grid-system)

## Make it look like your application

For a token-based starting point, configure `tailwind/quark-shadcn.theme.json`. To explicitly use that JSON as the theme input, set `ShadcnThemeConfig` to its project-relative path. Existing generated token CSS can otherwise be reused, so choose one source of theme tokens deliberately.

For a C# theme, add `Soenneker.Quark.Gen.Themes`, then define a factory:

```csharp
using Soenneker.Quark;
using Soenneker.Quark.Gen.Themes;
using Soenneker.Quark.Tokens;

[GenerateQuarkThemeCss("wwwroot/css/app-theme.css")]
public static class AppTheme
{
    public static Theme Build() => new()
    {
        Name = "MyApp",
        Tokens = new ThemeTokens
        {
            Light =
            {
                Primary = "#2563eb",
                PrimaryForeground = "#ffffff"
            },
            Dark =
            {
                Primary = "#93c5fd",
                PrimaryForeground = "#172554"
            }
        },
        Buttons = new ButtonOptions
        {
            Rounded = Rounded.Lg
        }
    };
}
```

The factory is evaluated during the build. Its output includes `app-theme.css`, `app-theme.min.css`, and the Tailwind token file. Keep it deterministic; it should not depend on logged-in users or services that only exist in a running browser.

The `Buttons` rule produces the runtime component stylesheet. A factory containing only tokens produces the Tailwind token input; it does not need a separate component stylesheet. Rebuild Tailwind after token changes.

Load the runtime theme stylesheet after the generated Tailwind stylesheet:

```html
<link rel="stylesheet" href="css/quark-tailwind.min.css" />
<link rel="stylesheet" href="css/app-theme.min.css" />
```

This separation follows the pattern used in Leadping: application-owned theme tokens and component rules, with the Tailwind and theme stylesheets loaded in order. The [theme guide](https://quark.soenneker.com/themes) covers JSON configuration, C# themes, output files, and switching light/dark mode.

## Understand the files in your project

```text
YourApp/
  Program.cs                         # service registration
  _Imports.razor                     # Quark and icon namespaces
  Pages/QuarkStart.razor              # your components
  tailwind/
    input.css                        # Tailwind sources and explicit classes
    quark-shadcn.theme.json           # theme choices, if using JSON
    package.json / package-lock.json # Tailwind dependencies
    quark-tailwind-manifest.txt       # generated app classes
    quark-suite-tailwind-manifest.txt # generated/copied suite classes
    quark-theme.generated.css        # generated theme tokens
  wwwroot/css/
    quark-tailwind.css                # generated full stylesheet
    quark-tailwind.min.css            # generated minified stylesheet
  obj/.../Generated/                  # generated icon C# files
```

Edit the source files and configuration you own. Keep your customized `input.css`, theme configuration, package metadata, and lockfile in source control. Treat manifests, generated theme CSS, icon maps, and build hashes as generated output. The suite repository may check in generated files; that does not make them the place to author a change.

Use normal `dotnet build` and `dotnet publish` commands after changing components, builders, icons, or themes. There is no documentation prerendering step. For CI, make the .NET SDK and required restore/download access available; a separate Node setup step is not a Quark prerequisite.

## Configuration and common fixes

Defaults are enough to start. If you need custom `QuarkOptions`, register them before the suite:

```csharp
builder.Services.AddQuarkOptionsAsScoped(new QuarkOptions
{
    Debug = false,
    AlwaysRender = false
});
builder.Services.AddQuarkSuiteAsScoped();
```

`AlwaysRender` defaults to false. Replace mutable parameter objects when their contents change; opt into `AlwaysRender` only when your app intentionally relies on in-place mutation.

| Symptom | First thing to check |
| --- | --- |
| Components look unstyled | Enable `TailwindGeneratorBuildEnabled`, build the app project, and check that its generated CSS URL returns a stylesheet. |
| Built-in styles work but a builder value does not | Confirm Tailwind.Manifest runs in the source project and inspect the app manifest for the complete class. |
| An icon is missing | Check the catalog package, generated provider registration, and a source-visible reference to the icon. |
| Generated DI extension is unresolved | Build the app once; confirm the generator is referenced by that app and its provider/registration output is enabled. |
| A runtime-computed class is absent | Enumerate the full classes in `tailwind/input.css` with `@source inline(...)`; generators cannot infer arbitrary runtime data. |
| Theme changes do not show | Check which input owns the tokens, rebuild, and load the runtime theme CSS after Tailwind. |
| A button is visible but does not respond | Check Blazor startup, the browser console, and the page's interactive render mode. |

See [troubleshooting](https://quark.soenneker.com/troubleshooting) for the specific files and settings to inspect.

## Learn from working apps

- [Quark demo source](https://github.com/soenneker/soenneker.quark.suite/tree/main/test/Soenneker.Quark.Suite.Demo): app-level generators, icon registrations, theme switching, and the full component catalog.
- [CreditCards demo](https://github.com/soenneker/soenneker.blazor.creditcards/tree/main/test/Soenneker.Blazor.CreditCards.Demo): a small Blazor app combining Quark services, Lucide icons, and a focused interop library.
- [Cloudflare AI Search demo](https://github.com/soenneker/soenneker.blazor.cloudflare.aisearch/tree/main/test/Soenneker.Blazor.Cloudflare.AiSearch.Demo): Quark UI composed with a separate feature package and its service registration.

## Contribute

See [CONTRIBUTING.md](https://github.com/soenneker/soenneker.quark.suite/blob/main/.github/CONTRIBUTING.md) for contribution guidance and [SECURITY.md](https://github.com/soenneker/soenneker.quark.suite/blob/main/.github/SECURITY.md) for private vulnerability reporting. Quark is available under the [MIT license](https://github.com/soenneker/soenneker.quark.suite/blob/main/LICENSE).# Quark Suite

**Build Blazor interfaces with the composition and visual language of shadcn/ui, using C# and Razor.**

Quark gives you ready-to-use components for application layouts, forms, dialogs, data tables, charts, navigation, and AI/chat interfaces. Start with sensible defaults, compose the pieces you need, and customize them with typed Tailwind builders or ordinary CSS classes.

[Explore the components](https://quark.soenneker.com/components) · [Install Quark](https://quark.soenneker.com/installation) · [Build your first component](https://quark.soenneker.com/first-component) · [Understand the generators](https://quark.soenneker.com/generators)

## Why Quark?

- **Compose real application UI.** A card has a header, content, and actions; a dialog has a trigger and content. Work with those parts directly in Razor.
- **Keep styling close to the component.** Use typed values such as `Padding.OnX.Is4.OnY.Is2`, responsive modifiers, semantic colors, and component variants. Use `Class` and `Style` for custom designs.
- **Build with Blazor conventions.** Bind values, handle events in C#, and use your existing services. Bradix supplies interaction primitives for focus management, overlays, and composite controls; it is included as a dependency.
- **Let the build prepare the assets.** Generators produce the Tailwind stylesheet, selected icon SVGs, and optional theme CSS. You do not need to set up a separate npm workflow to get started.
- **Own the application's visual identity.** Start with shadcn-compatible tokens, then define light/dark colors and component styling in a shared C# theme when your app needs it.

Quark is an MIT-licensed NuGet component library. It takes design inspiration from shadcn/ui while using Razor composition and .NET APIs. It does not require a React application or a shadcn CLI setup.

## Quick start

Use an interactive Blazor application targeting **.NET 10**. The walkthrough below uses a standalone Blazor WebAssembly app, like the Quark demo. Run package commands in the app project directory.

### 1. Add the core packages

```shell
dotnet add package Soenneker.Quark.Suite
dotnet add package Soenneker.Quark.Gen.Tailwind
dotnet add package Soenneker.Quark.Gen.Tailwind.Manifest
dotnet add package Soenneker.Quark.Gen.Lucide
dotnet add package Soenneker.Lucide.Icons
```

The suite contains the UI. The Tailwind packages produce the CSS. The Lucide generator produces the application's icon provider; Lucide is also used by Quark controls. Brand icons and C# theme generation can be added later.

Reference the generators directly in the application project. Do not assume the suite's private build-time dependencies will run for your app. If you package a reusable library, mark its generator references with `PrivateAssets="all"`, as the Quark and Soenneker demos do.

### 2. Enable CSS generation

Add this property group to the app's `.csproj`:

```xml
<PropertyGroup>
  <TailwindGeneratorBuildEnabled>true</TailwindGeneratorBuildEnabled>
</PropertyGroup>
```

**You do not need to install Node.js or npm manually.** The Tailwind build uses `Soenneker.Node.Util` to locate or provision the toolchain and install its npm dependencies. The first build needs network access for any missing tools and packages, plus permission to write its build/cache files. Later builds reuse the installed dependencies and skip unchanged generator work.

### 3. Register Quark and the icon provider

In `Program.cs`, after creating the builder and before building the host:

```csharp
using Soenneker.Quark;
using Soenneker.Quark.Gen.Lucide.Generated;

// After creating builder, before builder.Build():
builder.Services.AddQuarkSuiteAsScoped();
builder.Services.AddLucideIconsAsScoped();
```

### 4. Import the components and load the stylesheet

Add to `_Imports.razor`:

```razor
@using Soenneker.Quark
@using Soenneker.Lucide.Enums.Icons
```

Add to the document head in `wwwroot/index.html`:

```html
<link rel="stylesheet" href="css/quark-tailwind.min.css" />
```

Then run `dotnet build`. The generator creates both `wwwroot/css/quark-tailwind.css` and `wwwroot/css/quark-tailwind.min.css`; load one of them. With a Blazor Web App, load the stylesheet in its document shell (usually `Components/App.razor`) and register services in the host that executes the interactive components. Components that handle input and events need an interactive render mode.

### 5. Build something you can click

Create `Pages/QuarkStart.razor`, paste the complete example below, run the app, and open `/quark-start`:

```razor
@page "/quark-start"
@using Soenneker.Quark
@using Soenneker.Lucide.Enums.Icons

<Card Class="max-w-md">
    <CardHeader>
        <CardTitle>Your first Quark component</CardTitle>
        <CardDescription>Give your workspace a name.</CardDescription>
    </CardHeader>
    <CardContent>
        <Field>
            <FieldLabel For="workspace-name">Workspace name</FieldLabel>
            <Input Id="workspace-name" @bind-Value="_name" />
        </Field>
    </CardContent>
    <CardFooter>
        <Button OnClick="Save">
            <Icon Name="LucideIcon.Check" aria-hidden="true" />
            Save workspace
        </Button>
    </CardFooter>
</Card>
<p role="status">@_message</p>

@code {
    private string? _name = "My workspace";
    private string _message = "Changes are saved only in this example.";

    private void Save()
    {
        _message = $"Saved: {_name}";
    }
}
```

You should see a styled card, a labelled input, a check icon, and a Save button that updates the status message. This checks the stylesheet, generated icon registration, value binding, and event handling in one small example. See the [live walkthrough](https://quark.soenneker.com/first-component).

## What each generator does

The generators have different jobs. A typical app starts with Tailwind, Tailwind.Manifest, and Lucide. Add SimpleIcons for brand logos and Themes for a C# theme. The remaining generators primarily build Quark's reusable APIs.

| Package | Who uses it? | Job |
| --- | --- | --- |
| [Soenneker.Quark.Gen.Tailwind](https://github.com/soenneker/soenneker.quark.gen.tailwind) | Application | Provisions the Node/npm toolchain as needed, installs Tailwind dependencies, combines sources and theme tokens, and writes the full and minified app stylesheets. Enable TailwindGeneratorBuildEnabled. |
| [Soenneker.Quark.Gen.Tailwind.Manifest](https://github.com/soenneker/soenneker.quark.gen.tailwind.manifest) | Application / source project | Finds utilities composed by C# builders and writes tailwind/quark-tailwind-manifest.txt. This supplies complete class names that Tailwind cannot read from a fluent expression. Enabled by default. |
| [Soenneker.Quark.Gen.Lucide](https://github.com/soenneker/soenneker.quark.gen.lucide) | Application | Scans source for Lucide icon references and generates an SVG map, provider, and AddLucideIconsAsScoped(). Soenneker.Lucide.Icons supplies the SVG catalog. |
| [Soenneker.Quark.Gen.SimpleIcons](https://github.com/soenneker/soenneker.quark.gen.simpleicons) | Optional: brand icons | Generates the brand-icon map, provider, and AddSimpleIconsAsScoped(). Pair with Soenneker.SimpleIcons.Icons and the SimpleIcon component. |
| [Soenneker.Quark.Gen.Themes](https://github.com/soenneker/soenneker.quark.gen.themes) | Optional: a C# theme | Executes an attributed Theme factory during the build; writes runtime theme CSS plus Tailwind token CSS. Useful for shared application colors and component styling. |
| [Soenneker.Quark.Gen.Tailwind.Manifest.Suite](https://github.com/soenneker/soenneker.quark.gen.tailwind.manifest.suite) | Library authors | Builds the suite-wide class manifest shipped with Quark, including component defaults and variants. The app consumes the packaged manifest; it does not normally install this generator. |
| [Soenneker.Quark.Gen.Presets](https://github.com/soenneker/soenneker.quark.gen.presets) | Preset authors | Creates typed QuarkPresets registry tokens from classes marked with QuarkPreset. Quark already ships its built-in tokens. Add it when generating your own preset registry. |
| [Soenneker.Quark.Gen.Tailwind.Modifiers](https://github.com/soenneker/soenneker.quark.gen.tailwind.modifiers) | Builder-library authors | Generates modifier entry points such as OnHover, OnMd, and OnDark for partial builder entry classes. Applications already receive those APIs through Soenneker.Quark.Builders. |

```text
Your Razor + C# builders  -> Tailwind.Manifest -> app class manifest
Quark's NuGet package     -> suite class manifest
Theme JSON or C# factory  -> theme token CSS
                         -> Tailwind build -> quark-tailwind.min.css

Your LucideIcon references -> Gen.Lucide -> SVG map + provider + DI extension
Your brand icon references -> Gen.SimpleIcons -> SVG map + provider + DI extension
```

A class manifest is a list of complete Tailwind utilities, not a stylesheet. The suite manifest protects Quark's built-in styles; the app manifest adds the utilities created by your builder expressions. The Tailwind generator combines them with the app's literal classes and theme input.

The icon catalog packages (`Soenneker.Lucide.Icons` and `Soenneker.SimpleIcons.Icons`) provide SVG source assets. Their enum packages provide typed names. The generators create the app-local maps and DI registrations. These are separate responsibilities; installing a catalog alone does not register the generated provider.

For configuration, build outputs, shared libraries, and dynamic values, see the [generator guide](https://quark.soenneker.com/generators).

## Add brand icons when you need them

```shell
dotnet add package Soenneker.Quark.Gen.SimpleIcons
dotnet add package Soenneker.SimpleIcons.Icons
```

```csharp
using Soenneker.Quark.Gen.SimpleIcons.Generated;

builder.Services.AddSimpleIconsAsScoped();
```

```razor
<SimpleIcon Name="Github"
            aria-label="GitHub" />
```

The fully qualified enum name avoids a collision with the Razor component name and keeps the icon discoverable by the source scanner. Prefer explicit enum references or supported literal icon names. If an icon is chosen from configuration, keep a source-visible mapping of every allowed icon and rebuild when the set changes.

## Style components without leaving Razor

```razor
<Div Padding="Padding.OnX.Is4.OnY.Is2"
     FlexDirection="FlexDirection.Col.OnMd.Row"
     Display="Display.Flex"
     Gap="Gap.Is3">
    Content
</Div>
```

Read builder chains left to right: **a modifier applies to the next value**. `Padding.OnX.Is4.OnY.Is2` emits `px-4 py-2`; `FlexDirection.Col.OnMd.Row` emits `flex-col md:flex-row`. Do not put a trailing modifier after the value you intend it to affect.

Use component variants for normal choices, typed properties for common utilities, and `Class` for exact Tailwind expressions. Prefer semantic colors such as `BackgroundColor.Card` and `TextColor.CardForeground` so light and dark themes can share the same markup.

[Styling and properties](https://quark.soenneker.com/properties) · [Responsive grids](https://quark.soenneker.com/grid-system)

## Make it look like your application

For a token-based starting point, configure `tailwind/quark-shadcn.theme.json`. To explicitly use that JSON as the theme input, set `ShadcnThemeConfig` to its project-relative path. Existing generated token CSS can otherwise be reused, so choose one source of theme tokens deliberately.

For a C# theme, add `Soenneker.Quark.Gen.Themes`, then define a factory:

```csharp
using Soenneker.Quark;
using Soenneker.Quark.Gen.Themes;
using Soenneker.Quark.Tokens;

[GenerateQuarkThemeCss("wwwroot/css/app-theme.css")]
public static class AppTheme
{
    public static Theme Build() => new()
    {
        Name = "MyApp",
        Tokens = new ThemeTokens
        {
            Light =
            {
                Primary = "#2563eb",
                PrimaryForeground = "#ffffff"
            },
            Dark =
            {
                Primary = "#93c5fd",
                PrimaryForeground = "#172554"
            }
        }
    };
}
```

The factory is evaluated during the build. Its output includes `app-theme.css`, `app-theme.min.css`, and the Tailwind token file. Keep it deterministic; it should not depend on logged-in users or services that only exist in a running browser.

Load the runtime theme stylesheet after the generated Tailwind stylesheet:

```html
<link rel="stylesheet" href="css/quark-tailwind.min.css" />
<link rel="stylesheet" href="css/app-theme.min.css" />
```

This separation follows the pattern used in Leadping: application-owned theme tokens and component rules, with the Tailwind and theme stylesheets loaded in order. The [theme guide](https://quark.soenneker.com/themes) covers JSON configuration, C# themes, output files, and switching light/dark mode.

## Understand the files in your project

```text
YourApp/
  Program.cs                         # service registration
  _Imports.razor                     # Quark and icon namespaces
  Pages/QuarkStart.razor              # your components
  tailwind/
    input.css                        # Tailwind sources and explicit classes
    quark-shadcn.theme.json           # theme choices, if using JSON
    package.json / package-lock.json # Tailwind dependencies
    quark-tailwind-manifest.txt       # generated app classes
    quark-suite-tailwind-manifest.txt # generated/copied suite classes
    quark-theme.generated.css        # generated theme tokens
  wwwroot/css/
    quark-tailwind.css                # generated full stylesheet
    quark-tailwind.min.css            # generated minified stylesheet
  obj/.../Generated/                  # generated icon C# files
```

Edit the source files and configuration you own. Keep your customized `input.css`, theme configuration, package metadata, and lockfile in source control. Treat manifests, generated theme CSS, icon maps, and build hashes as generated output. The suite repository may check in generated files; that does not make them the place to author a change.

Use normal `dotnet build` and `dotnet publish` commands after changing components, builders, icons, or themes. There is no documentation prerendering step. For CI, make the .NET SDK and required restore/download access available; a separate Node setup step is not a Quark prerequisite.

## Configuration and common fixes

Defaults are enough to start. If you need custom `QuarkOptions`, register them before the suite:

```csharp
builder.Services.AddQuarkOptionsAsScoped(new QuarkOptions
{
    Debug = false,
    AlwaysRender = false
});
builder.Services.AddQuarkSuiteAsScoped();
```

`AlwaysRender` defaults to false. Replace mutable parameter objects when their contents change; opt into `AlwaysRender` only when your app intentionally relies on in-place mutation.

| Symptom | First thing to check |
| --- | --- |
| Components look unstyled | Enable `TailwindGeneratorBuildEnabled`, build the app project, and check that its generated CSS URL returns a stylesheet. |
| Built-in styles work but a builder value does not | Confirm Tailwind.Manifest runs in the source project and inspect the app manifest for the complete class. |
| An icon is missing | Check the catalog package, generated provider registration, and a source-visible reference to the icon. |
| Generated DI extension is unresolved | Build the app once; confirm the generator is referenced by that app and its provider/registration output is enabled. |
| A runtime-computed class is absent | Enumerate the full classes in `tailwind/input.css` with `@source inline(...)`; generators cannot infer arbitrary runtime data. |
| Theme changes do not show | Check which input owns the tokens, rebuild, and load the runtime theme CSS after Tailwind. |
| A button is visible but does not respond | Check Blazor startup, the browser console, and the page's interactive render mode. |

See [troubleshooting](https://quark.soenneker.com/troubleshooting) for the specific files and settings to inspect.

## Learn from working apps

- [Quark demo source](https://github.com/soenneker/soenneker.quark.suite/tree/main/test/Soenneker.Quark.Suite.Demo): app-level generators, icon registrations, theme switching, and the full component catalog.
- [CreditCards demo](https://github.com/soenneker/soenneker.blazor.creditcards/tree/main/test/Soenneker.Blazor.CreditCards.Demo): a small Blazor app combining Quark services, Lucide icons, and a focused interop library.
- [Cloudflare AI Search demo](https://github.com/soenneker/soenneker.blazor.cloudflare.aisearch/tree/main/test/Soenneker.Blazor.Cloudflare.AiSearch.Demo): Quark UI composed with a separate feature package and its service registration.
