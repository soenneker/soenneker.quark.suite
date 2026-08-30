[![](https://img.shields.io/nuget/v/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/publish-package.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/build-and-test.yml?label=Build&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/nuget/dt/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.quark.suite/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/codeql.yml)

# Soenneker.Quark.Suite

A shadcn-inspired Blazor component suite with Bradix-backed interaction behavior and Tailwind-oriented styling.

Quark includes application layout, navigation, buttons, menus, dialogs, forms, validation, tables, data-entry controls, feedback, typography, date and time controls, code editing, AI/chat surfaces, and other product UI. See the [live component demo](https://soenneker.github.io/soenneker.quark.suite/) before adopting it.

## Install

Install the component suite and the app-level build generators:

```bash
dotnet add package Soenneker.Quark.Suite
dotnet add package Soenneker.Quark.Gen.Tailwind
dotnet add package Soenneker.Quark.Gen.Tailwind.Manifest
dotnet add package Soenneker.Quark.Gen.Lucide
dotnet add package Soenneker.Lucide.Icons
dotnet add package Soenneker.Quark.Gen.SimpleIcons
dotnet add package Soenneker.SimpleIcons.Icons
```

The generator packages belong in the final application project, not only in a shared Razor class library. They inspect that project and compile the CSS and icon maps it actually uses.

## Enable Tailwind generation

Add this to the application project file:

```xml
<PropertyGroup>
  <TailwindGeneratorBuildEnabled>true</TailwindGeneratorBuildEnabled>
</PropertyGroup>
```

Node.js and npm must be available when the app builds. The build writes `wwwroot/css/quark-tailwind.css` and `wwwroot/css/quark-tailwind.min.css`.

Load one generated stylesheet from `wwwroot/index.html`, the server host page, or the application’s equivalent shell:

```html
<link rel="stylesheet" href="css/quark-tailwind.min.css" />
```

## Register services

```csharp
using Soenneker.Quark;
using Soenneker.Quark.Gen.Lucide.Generated;
using Soenneker.Quark.Gen.SimpleIcons.Generated;

builder.Services.AddQuarkSuiteAsScoped();
builder.Services.AddLucideIconsAsScoped();
builder.Services.AddSimpleIconsAsScoped();
```

The generated icon registrations are app-local: only icons referenced directly through `LucideIcon.Name`, `SimpleIcon.Name`, or supported literal Razor syntax are embedded. Dynamically constructed icon names need a direct source reference so the build can discover them.

## Use components

Add the namespace to `_Imports.razor`:

```razor
@using Soenneker.Quark
```

Compose components normally:

```razor
<Card>
    <CardHeader>
        <CardTitle>Edit profile</CardTitle>
        <CardDescription>Update the name shown to your team.</CardDescription>
    </CardHeader>
    <CardContent>
        <Input @bind-Value="_displayName" Placeholder="Display name" />
    </CardContent>
    <CardFooter>
        <Button OnClick="Save">Save changes</Button>
    </CardFooter>
</Card>
```

Quark’s overlay and composite components use `Soenneker.Bradix.Suite` underneath for focus management, portals, dismissable layers, menus, selects, tabs, tooltips, and related interaction behavior. Bradix is already a package dependency; it does not require a separate install.

## Options

`AddQuarkSuiteAsScoped()` installs default `QuarkOptions` unless the application has registered its own:

```csharp
builder.Services.AddQuarkOptionsAsScoped(new QuarkOptions
{
    Debug = false,
    AutomaticFrameworkResourceLoading = false,
    CodeEditorUseCdn = false
});

builder.Services.AddQuarkSuiteAsScoped();
```

Register custom options before the suite so the default registration is not added. `AlwaysRender` is available for applications that intentionally mutate component parameter objects in place; leave it disabled for normal immutable/replacement-style parameter updates.

## Custom themes

The default Tailwind build creates `tailwind/quark-shadcn.theme.json`. Edit that file to choose theme tokens. For strongly typed theme definitions and separate runtime component CSS, install `Soenneker.Quark.Gen.Themes` and annotate a static `Theme` factory with `GenerateQuarkThemeCss`.

Generated Tailwind and icon artifacts are build outputs. Rebuild after adding utility classes, builder expressions, icons, or theme changes.
