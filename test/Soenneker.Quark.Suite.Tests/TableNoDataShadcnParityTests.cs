using System.Reflection;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void TableNoData_matches_empty_shell_contract()
    {
        var table = new Table();

        var backingField = typeof(Table).GetField("<HasLoadedOnce>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;
        backingField.SetValue(table, true);

        var cut = Render<CascadingValue<ITable>>(parameters => parameters
            .Add(p => p.Name, nameof(TableNoData.Table))
            .Add(p => p.Value, table)
            .AddChildContent<TableNoData>());

        var classes = cut.Find("[data-slot='table-empty']").GetAttribute("class")!;

        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border");
        classes.Should().Contain("bg-card");
        classes.Should().Contain("py-12");
        classes.Should().NotContain("rounded-md");
    }
}
