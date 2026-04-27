[![](https://img.shields.io/nuget/v/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.quark.suite/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Quark.Suite

**A shadcn-inspired component suite for Blazor.**

`Soenneker.Quark.Suite` gives Blazor applications a practical, styled UI layer: buttons, dialogs, forms, tables, navigation, sidebars, menus, typography, feedback components, AI/chat surfaces, and the application patterns that usually have to be rebuilt from scratch in every product.

The goal is not to be a generic theme slapped on top of HTML. Quark is meant to be the everyday UI system for real Blazor apps: dense admin screens, dashboards, settings pages, forms, tables, internal tools, SaaS products, and AI-heavy interfaces.

Quark relies on [`Soenneker.Bradix.Suite`](https://github.com/soenneker/soenneker.bradix.suite) for the hard primitive behavior underneath: focus management, portals, overlays, dismissable layers, popovers, menus, selects, tabs, tooltips, and other interaction details. Bradix is the unstyled foundation. Quark is the styled component suite built on top of it.

## Why Quark

Quark is for teams that want to move quickly without giving up control of the product UI.

- **Blazor-first components**: normal Razor components, parameters, events, DI registration, and .NET-friendly patterns.
- **Shadcn-inspired composition**: familiar component shapes, clean markup, and practical defaults without hiding everything behind a heavy framework.
- **Bradix-backed behavior**: overlays, menus, popovers, selects, tabs, tooltips, and focus behavior are built on a real primitive layer instead of one-off JavaScript.
- **Tailwind-oriented styling**: designed around utility classes, generated manifests, presets, and theme options.
- **Product coverage**: enough components to build full application screens, not just isolated demos.
- **Design-system friendly**: use the defaults directly, configure them centrally, or wrap components for your own product language.

## What You Can Build

Quark is broad enough to cover the common surface area of a modern Blazor product.

**Application structure**  
Headers, sidebars, navigation menus, breadcrumbs, tabs, sections, containers, grids, collapsible regions, semantic layout wrappers, and responsive app shells.

**Actions and overlays**  
Buttons, button groups, dropdowns, context menus, menubars, dialogs, alert dialogs, sheets, drawers, popovers, hover cards, tooltips, commands, and navigation menus.

**Forms and data entry**  
Inputs, text areas, input groups, selects, native selects, comboboxes, checks, radio groups, switches, sliders, date inputs, date pickers, field layouts, validation, and one-time-password inputs.

**Data display and workflows**  
Tables, pagination, sorting helpers, resizable panels, sortable lists, tree views, attachments, steps, timelines, scores, progress, loading states, empty states, and code editor support.

**Content and polish**  
Cards, alerts, badges, avatars, icons, images, typography, lists, blockquotes, code, keyboard hints, skeletons, spinners, sonner toasts, and theme toggles.

**AI and messaging UI**  
Prompt inputs, thread-oriented components, model selectors, suggestions, attachments, and message-focused layouts.

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

Use the components in Razor:

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

## Theming

Quark includes browser theme interop for light/dark mode and centralized component options. For most apps, `AddQuarkSuiteAsScoped()` is the only required registration.

If you need custom build-time theme generation, author a `Theme` and use `Soenneker.Quark.Gen.Themes` to emit generated CSS artifacts.

## Demo

[Open the demo site](https://soenneker.github.io/soenneker.quark.suite/)
