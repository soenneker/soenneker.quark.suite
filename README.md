# Quark Suite

**Beautiful Blazor interfaces. Composable components. Your design.**

Bring the visual language of shadcn/ui to your next Blazor application. Quark combines ready-to-use components, Tailwind styling, and native C# and Razor APIs so you can go from a blank page to a polished product.

Build a dashboard, an admin workspace, a customer portal, or an AI chat experience. Start with cohesive defaults, compose the pieces your screen needs, and make them your own.

[**Explore the live demo →**](https://quark.soenneker.com/) · [Component catalog](https://quark.soenneker.com/components) · [Get started](#get-started) · [NuGet](https://www.nuget.org/packages/soenneker.quark.suite/)

[![NuGet version](https://img.shields.io/nuget/v/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![Build](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/build-and-test.yml?label=Build&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/build-and-test.yml)
[![NuGet downloads](https://img.shields.io/nuget/dt/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)](LICENSE)

## Why build with Quark?

- **A cohesive starting point.** Cards, forms, navigation, overlays, tables, and charts share a visual language that works across your application.
- **Composition that feels like Razor.** Combine headers, content, actions, and triggers directly in your markup. Bind values and handle events with ordinary C#.
- **Styling with room to grow.** Choose a component variant, use fluent C# builders for responsive layouts, or apply your own Tailwind classes and CSS.
- **Your brand, throughout the UI.** Shared theme tokens let you customize colors and light/dark appearances across components.
- **More than the basics.** Build richer workflows with file uploads, date pickers, data tables, real-time charts, and AI/chat components.
- **Fits your .NET workflow.** Install through NuGet and generate styles and icons during your .NET build, without manually setting up Node.js or npm. The suite also declares AOT compatibility.

Quark is an **MIT-licensed Blazor component library** inspired by shadcn/ui. Its components use Bradix interaction primitives for behaviors such as focus management and overlays, alongside Quark styling and .NET APIs.

## From everyday controls to complete experiences

Explore working examples and adapt them to your application:

| What you are building | What to reach for |
| --- | --- |
| **Application workspaces** | [Sidebars](https://quark.soenneker.com/components/sidebar), [cards](https://quark.soenneker.com/components/card), and [tabs](https://quark.soenneker.com/components/tabs) for navigation and page structure. |
| **Forms and settings** | [Inputs](https://quark.soenneker.com/components/input), [fields](https://quark.soenneker.com/components/field), and [date pickers](https://quark.soenneker.com/components/date-picker) for collecting and organizing input. |
| **Dashboards and reporting** | [Data tables](https://quark.soenneker.com/components/datatables), [charts](https://quark.soenneker.com/components/charts), and [real-time charts](https://quark.soenneker.com/components/realtime-charts) for exploring data. |
| **Actions and feedback** | [Dialogs](https://quark.soenneker.com/components/dialog), [dropdown menus](https://quark.soenneker.com/components/dropdown-menu), and [toast notifications](https://quark.soenneker.com/components/sonner) for focused interactions. |
| **AI and attachment workflows** | [Prompt inputs](https://quark.soenneker.com/components/prompt-inputs), [attachments](https://quark.soenneker.com/components/attachments), and [file drop zones](https://quark.soenneker.com/components/file-drop-zone) for composing messages and working with files. |

[**Browse the full component catalog →**](https://quark.soenneker.com/components)

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

<Card Class="max-w-md">
    <CardHeader>
        <CardTitle>Make room for your next idea</CardTitle>
        <CardDescription>Create a workspace for your team.</CardDescription>
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
            : $"Ready to create {_name}!";
    }
}
```

Run the app:

```shell
dotnet run
```

Open `/quark-start` at the address printed in your terminal. You now have a styled card, a labelled input, an icon, and a working button. This example updates a local message; connect the handler to your application service to create a real workspace.

## Make it yours

### Stable, content-based table columns

Set `AdaptiveLayout="true"` on `Table` or `DataTable` to size columns from rendered content without assigning individual widths:

```razor
<Table AdaptiveLayout="true">
    <Thead><Tr><Th>Job</Th><Th>Attempt</Th><Th>Action</Th></Tr></Thead>
    <Tbody>
        <Tr><Td>PhoneLookupRefreshJob.Recover</Td><Td>1 / 1</Td><Td><Button>Retry</Button></Td></Tr>
    </Tbody>
</Table>
```

Adaptive sizing measures visible content, adds 24 pixels of spare room per column, and gives remaining container space to the widest column. Small button, status, or timestamp changes within those allocations do not move columns. Larger changes grow the affected column; shorter content keeps its allocation across refreshes and pages. Container resizing, changed headers, or a changed column count triggers fresh sizing.

Optional `ColumnSizing="@(new TableColumnSizingOptions { Padding = 32, MinWidth = 64, MaxWidth = 640 })"` configures the reserve and measurement limits for the whole table. Long content wraps at the limit, while narrow containers scroll horizontally. The widest column may exceed `MaxWidth` when filling unused container space. A child `Table` inherits its `DataTable` settings unless overridden.

Adaptive sizing is opt-in and takes precedence over `FixedLayout`. Remove old per-column width rules when adopting it. Authored `colgroup` elements and rowspans retain native table sizing; spanning detail, loading, and summary rows are excluded from measurements. It requires interactive rendering and the normal Quark service registration.

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

Prefer writing Tailwind directly? Use `Class`:

```razor
<Div Class="flex flex-col gap-3 px-4 py-2 md:flex-row">
    <Button>Save changes</Button>
    <Button Variant="ButtonVariant.Outline">Cancel</Button>
</Div>
```

[Explore styling properties](https://quark.soenneker.com/properties) · [Build responsive grids](https://quark.soenneker.com/grid-system)

### Give your application its own identity

Customize shared theme tokens for your brand colors and light/dark appearances. Use semantic colors in your components so the same markup follows your theme:

```razor
<Div BackgroundColor="BackgroundColor.Card"
     TextColor="TextColor.CardForeground"
     Padding="Padding.Is4">
    <Paragraph>A surface that follows your application theme.</Paragraph>
</Div>
```

Configure tokens in `tailwind/quark-shadcn.theme.json`, or use the optional `Soenneker.Quark.Gen.Themes` package to define a shared theme in C#. The [theme guide](https://quark.soenneker.com/themes) walks through configuration, stylesheet setup, and switching between light and dark mode.

### Add icons to your actions

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

## Keep building

| Next step | Guide |
| --- | --- |
| Find a component and see it in action | [Component catalog](https://quark.soenneker.com/components) |
| Walk through a complete first example | [Your first component](https://quark.soenneker.com/first-component) |
| Customize layout and appearance | [Styling](https://quark.soenneker.com/properties) · [Themes](https://quark.soenneker.com/themes) |
| Configure CSS, icons, or build tooling | [Generator guide](https://quark.soenneker.com/generators) |
| Resolve missing styles, icons, or interaction | [Troubleshooting](https://quark.soenneker.com/troubleshooting) |
| Learn from a working application | [Demo source](test/Soenneker.Quark.Suite.Demo) |

If your first page looks unstyled, check that CSS generation is enabled and the stylesheet is loaded. If a button does not respond in a Blazor Web App, check the page's interactive render mode.

## Open source, ready for your next project

Quark is available under the [MIT license](LICENSE). Explore the demo, try it in your app, and help shape what comes next.

Found a bug or have a component idea? [Open an issue](https://github.com/soenneker/soenneker.quark.suite/issues). Contributions and example improvements are welcome. If Quark helps you ship, consider starring the repository so other Blazor developers can find it.
