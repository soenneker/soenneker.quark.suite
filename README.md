[![](https://img.shields.io/nuget/v/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.quark.suite/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.quark.suite/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.quark.suite.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.quark.suite/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.quark.suite/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Quark.Suite

**Blazor component library for .NET — Bootstrap 5, full theming, and type-safe CSS utilities.**

## Highlights

- 🎯 **Type-safe CSS**: Strong enums for colors, spacing, layout, typography  
- 🎨 **Theming**: Bootstrap CSS variable overrides + runtime theme switching  
- 🧩 **60+ components**: Buttons, forms, tables, modals, navs, data grid, more  
- 📦 **Single package**: All components in one NuGet, no extras  
- 🚀 **Optimized**: Built for performance and low overhead  

## Install

```bash
dotnet add package Soenneker.Quark.Suite
````

## Setup

```csharp
builder.Services.AddQuarkSuiteAsScoped();

var host = builder.Build();

await host.Services.LoadQuarkResources(); // Optional if you want to load resources via html

```

## Examples

**Type-safe styling**

```razor
<Button Color="@Color.Primary" Size="@Size.Large">Click</Button>

<Div Margin="Margin.Is3.FromTop" Padding="Padding.Is4.OnX">
    <Text Color="@TextColor.Success" Weight="@FontWeight.Bold">
        Success message
    </Text>
</Div>
```

**Data table with server-side paging**

```razor
<Table TItem="Employee" Data="employees" PageSize="10"
            ServerSide="true" OnRequestData="LoadEmployees">
    <TableColumns>
        <Th Field="@nameof(Employee.Name)" Sortable />
        <Th Field="@nameof(Employee.Email)" />
        <Th Field="@nameof(Employee.Department)" Sortable />
    </TableColumns>
</Table>
```