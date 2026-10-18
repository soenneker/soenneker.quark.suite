# Quark Suite

**Blazor components with shadcn/ui styling and typed C# APIs.**

Quark gives you forms, tables, navigation, dialogs, and more with consistent defaults. Compose components in Razor, handle events in C#, and control layout and appearance with typed properties.

Use it to build dashboards, admin tools, customer portals, and other Blazor applications without styling every control from scratch.

[**Explore the live demo →**](https://quark.soenneker.com/) · [Component catalog](https://quark.soenneker.com/components) · [Get started](#get-started) · [NuGet](https://www.nuget.org/packages/soenneker.quark.suite/)

[![NuGet version](https://img.shields.io/nuget/v/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![Build](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/build-and-test.yml?label=Build&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/build-and-test.yml)
[![NuGet downloads](https://img.shields.io/nuget/dt/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)](LICENSE)

## Why build with Quark?

- **Less styling to repeat.** Components share defaults for spacing, typography, colors, and interaction states. Variants cover common choices such as primary and secondary actions.
- **Ordinary Razor and C#.** Compose headers, content, and actions in markup. Bind values and handle events using the Blazor patterns you already know.
- **Typed styling.** Set layout, spacing, colors, and responsive behavior with Quark properties and fluent builders. Reuse styling through presets.
- **Shared themes.** Theme tokens apply your colors and light/dark appearances across components.
- **Build tooling included.** Install through NuGet and generate styles and icons during your .NET build. The generators provision Node.js and npm when needed.

Quark is MIT-licensed and uses Bradix interaction primitives for behaviors such as focus management and overlays.

## Get started

The following setup uses a **.NET 10 standalone Blazor WebAssembly app**. In an existing app, start with the package commands in its project directory.

### 1. Create an app and install Quark

```shell
dotnet new blazorwasm -n MyQuarkApp
cd MyQuarkApp

dotnet add package Soenneker.Quark.Suite
dotnet add package Soenneker.Quark.Gen.Tailwind
dotnet add package Soenneker.Quark.Gen.Tailwind.Manifest
dotnet add package Soenneker.Quark.Gen.Lucide
dotnet add package Soenneker.Lucide.Icons
```

These packages provide the components, stylesheet generation, and Lucide icons. Add them directly to the app project.

### 2. Enable styling

Add this property group inside your app's `.csproj`:

```xml
<PropertyGroup>
  <TailwindGeneratorBuildEnabled>true</TailwindGeneratorBuildEnabled>
</PropertyGroup>
```

In the `<head>` of `wwwroot/index.html`, add:

```html
<link rel="stylesheet" href="css/quark-tailwind.min.css" />
```

Your .NET build generates the stylesheet. The first build needs network access to download any missing tools and dependencies; Node.js and npm are provisioned automatically when needed.

### 3. Register services and import components

In `Program.cs`, add these namespaces:

```csharp
using Soenneker.Quark;
using Soenneker.Quark.Gen.Lucide.Generated;
```

After creating `builder` and before `builder.Build()`, register Quark and icons:

```csharp
builder.Services.AddQuarkSuiteAsScoped();
builder.Services.AddLucideIconsAsScoped();
```

Add to `_Imports.razor`:

```razor
@using Soenneker.Quark
@using Soenneker.Lucide.Enums.Icons
```

**Using a Blazor Web App?** Load the stylesheet in your document shell, usually `Components/App.razor`, and register the services in the project that runs your interactive components. Pages with input and event handlers need an interactive render mode. See the [installation guide](https://quark.soenneker.com/installation) for more setup details.

### 4. Build your first screen

Create `Pages/QuarkStart.razor`:

```razor
@page "/quark-start"

<Card MaxWidth="MaxWidth.IsMd">
    <CardHeader>
        <CardTitle>Create a workspace</CardTitle>
        <CardDescription>Choose a name for your team's workspace.</CardDescription>
    </CardHeader>
    <CardContent>
        <Field>
            <FieldLabel For="workspace-name">Workspace name</FieldLabel>
            <Input Id="workspace-name" @bind-Value="_name" />
        </Field>
    </CardContent>
    <CardFooter>
        <Button OnClick="CreateWorkspace">
            <Icon Name="LucideIcon.Check" aria-hidden="true" />
            Create workspace
        </Button>
    </CardFooter>
</Card>
<Paragraph role="status">@_message</Paragraph>

@code {
    private string? _name = "My workspace";
    private string _message = "";

    private void CreateWorkspace()
    {
        _message = string.IsNullOrWhiteSpace(_name)
            ? "Give your workspace a name to get started."
            : $"Workspace name: {_name}";
    }
}
```

Run the app:

```shell
dotnet run
```

Open `/quark-start` at the address printed in your terminal. The example demonstrates component composition, input binding, and a button handler. It only updates a local message; connect the handler to your application service to create a workspace.

## Components

The [component catalog](https://quark.soenneker.com/components) includes working examples. Common starting points:

| What you are building | What to reach for |
| --- | --- |
| **Application workspaces** | [Sidebars](https://quark.soenneker.com/components/sidebar), [cards](https://quark.soenneker.com/components/card), and [tabs](https://quark.soenneker.com/components/tabs) for navigation and page structure. |
| **Forms and settings** | [Inputs](https://quark.soenneker.com/components/input), [fields](https://quark.soenneker.com/components/field), and [date pickers](https://quark.soenneker.com/components/date-picker) for collecting and organizing input. |
| **Dashboards and reporting** | [Data tables](https://quark.soenneker.com/components/datatables), [charts](https://quark.soenneker.com/components/charts), and [real-time charts](https://quark.soenneker.com/components/realtime-charts) for exploring data. |
| **Actions and feedback** | [Dialogs](https://quark.soenneker.com/components/dialog), [dropdown menus](https://quark.soenneker.com/components/dropdown-menu), and [toast notifications](https://quark.soenneker.com/components/sonner) for focused interactions. |
| **AI and attachment workflows** | [Prompt inputs](https://quark.soenneker.com/components/prompt-inputs), [attachments](https://quark.soenneker.com/components/attachments), and [file drop zones](https://quark.soenneker.com/components/file-drop-zone) for composing messages and working with files. |

## Styling

### Start with a variant

Use built-in variants to give actions the right emphasis:

```razor
<Button>Save changes</Button>
<Button Variant="ButtonVariant.Secondary">Preview</Button>
<Button Variant="ButtonVariant.Outline">Cancel</Button>
```

### Compose responsive layouts in C#

Typed styling keeps spacing and responsive behavior alongside your components:

```razor
<Div Display="Display.Flex"
     FlexDirection="FlexDirection.Col.OnMd.Row"
     Gap="Gap.Is3"
     Padding="Padding.OnX.Is4.OnY.Is2">
    <Button>Save changes</Button>
    <Button Variant="ButtonVariant.Outline">Cancel</Button>
</Div>
```

The actions stack on smaller screens and sit in a row from the medium breakpoint. Read the chains left to right: `OnMd.Row` applies the breakpoint to the row layout; `OnX.Is4.OnY.Is2` sets horizontal and vertical padding.

Use typed Quark properties for styling whenever an equivalent property exists. For reusable styling, group those properties in a dedicated Quark preset.

[Explore styling properties](https://quark.soenneker.com/properties) · [Build responsive grids](https://quark.soenneker.com/grid-system)

### Apply your theme

Use semantic colors so components follow your theme in light and dark mode:

```razor
<Div BackgroundColor="BackgroundColor.Card"
     TextColor="TextColor.CardForeground"
     Padding="Padding.Is4">
    <Paragraph>A surface that follows your application theme.</Paragraph>
</Div>
```

Configure tokens in `tailwind/quark-shadcn.theme.json`, or use the optional `Soenneker.Quark.Gen.Themes` package to define a shared theme in C#. The [theme guide](https://quark.soenneker.com/themes) walks through configuration, stylesheet setup, and switching between light and dark mode.

## Icons

The quick start includes Lucide icons. Reference an icon by name in your markup:

```razor
<Button Variant="ButtonVariant.Outline">
    <Icon Name="LucideIcon.Check" aria-hidden="true" />
    Mark as complete
</Button>
```

For brand logos, add `Soenneker.Quark.Gen.SimpleIcons` and `Soenneker.SimpleIcons.Icons`, then register `AddSimpleIconsAsScoped()` from `Soenneker.Quark.Gen.SimpleIcons.Generated`. See the [generator guide](https://quark.soenneker.com/generators) for setup and dynamically selected icons.

## Page metadata

Use [`SeoHead`](src/Soenneker.Quark.Suite/Components/SeoHead/README.md) for page titles, descriptions, canonical URLs, Open Graph, Twitter cards, and optional JSON-LD. It works with Blazor's `HeadOutlet`; your application supplies its own branding and structured data.

## Documentation

| Next step | Guide |
| --- | --- |
| Find a component and see it in action | [Component catalog](https://quark.soenneker.com/components) |
| Walk through a complete first example | [Your first component](https://quark.soenneker.com/first-component) |
| Customize layout and appearance | [Styling](https://quark.soenneker.com/properties) · [Themes](https://quark.soenneker.com/themes) |
| Configure CSS, icons, or build tooling | [Generator guide](https://quark.soenneker.com/generators) |
| Resolve missing styles, icons, or interaction | [Troubleshooting](https://quark.soenneker.com/troubleshooting) |
| Learn from a working application | [Demo source](test/Soenneker.Quark.Suite.Demo) |

## Troubleshooting

| Problem | Check |
| --- | --- |
| Components appear unstyled | Enable `TailwindGeneratorBuildEnabled`, rebuild, and confirm the generated stylesheet is loaded by the page. |
| Buttons or inputs do not respond | In a Blazor Web App, ensure the page uses an interactive render mode and Quark services are registered in the project running the components. |
| Lucide icons do not appear | Confirm both Lucide packages are installed and `AddLucideIconsAsScoped()` is registered, then rebuild. |

See the [troubleshooting guide](https://quark.soenneker.com/troubleshooting) for more details.

## License and contributions

Quark is available under the [MIT license](LICENSE). Issues and contributions are welcome.

For bugs, [open an issue](https://github.com/soenneker/soenneker.quark.suite/issues) with the package version, hosting model, and a minimal reproduction. For feature requests, describe the workflow you need to support.
