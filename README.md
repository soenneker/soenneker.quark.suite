[![](https://img.shields.io/nuget/v/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.quark.suite/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Quark.Suite

Shadcn-inspired Blazor components for modern .NET apps.

`Soenneker.Quark.Suite` is the main Quark package: a Razor class library with a large set of UI components, lower-level primitives, and Tailwind-oriented styling utilities.

## Installation

```bash
dotnet add package Soenneker.Quark.Suite
```

## Basic Setup

Register the suite through the main DI entry point:

```csharp
using Soenneker.Quark;

builder.Services.AddQuarkSuiteAsScoped();
```

Then start using components in Razor:

```razor
<Button>Save changes</Button>

<Input Placeholder="Search..." />

<Dialog @bind-Visible="_showDialog">
    <DialogHeader>
        <DialogTitle>Edit profile</DialogTitle>
        <DialogCloseButton />
    </DialogHeader>
    <DialogBody>
        <Input Placeholder="Display name" />
    </DialogBody>
</Dialog>
```

## What You Get

- dialogs, sheets, popovers, tooltips, alert dialogs
- buttons, cards, badges, menus, breadcrumbs, navigation, sidebar
- fields, inputs, selects, comboboxes, date pickers, validation
- tables, pagination, resizable layouts, charts, code editor
- sonner, skeletons, progress, steps, and other UI primitives

## Theming

The suite includes runtime theme interop and theme-related infrastructure. For most apps, `AddQuarkSuiteAsScoped()` is the only required registration. If you need custom theme generation, the source also includes `Theme` and `ThemeProvider`.
