using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void TableLoader_matches_overlay_card_shell()
    {
        var cut = Render<CascadingValue<bool>>(parameters => parameters
            .Add(p => p.Name, nameof(TableLoader.IsLoading))
            .Add(p => p.Value, true)
            .AddChildContent<TableLoader>());

        var classes = cut.Find("[data-slot='table-loader-content']").GetAttribute("class")!;

        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border");
        classes.Should().Contain("bg-card");
        classes.Should().Contain("shadow-sm");
        classes.Should().NotContain("rounded-md");
    }
}
