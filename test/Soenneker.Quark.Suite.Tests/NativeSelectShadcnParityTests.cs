using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void NativeSelect_matches_shadcn_base_classes()
    {
        var nativeSelect = Render<NativeSelect>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "option");
                builder.AddAttribute(1, "value", "");
                builder.AddContent(2, "Select status");
                builder.CloseElement();
            })));

        string nativeSelectClasses = nativeSelect.Find("[data-slot='native-select']").GetAttribute("class")!;
        string nativeSelectIconClasses = nativeSelect.Find("[data-slot='native-select-icon']").GetAttribute("class")!;

        nativeSelectClasses.Should().Contain("h-8");
        nativeSelectClasses.Should().Contain("rounded-lg");
        nativeSelectClasses.Should().Contain("border-input");
        nativeSelectClasses.Should().Contain("py-1");
        nativeSelectClasses.Should().Contain("pr-8");
        nativeSelectClasses.Should().Contain("pl-2.5");
        nativeSelectClasses.Should().Contain("select-none");
        nativeSelectClasses.Should().Contain("aria-invalid:ring-[3px]");
        nativeSelectClasses.Should().NotContain("q-native-select");

        nativeSelectIconClasses.Should().Contain("right-2.5");
        nativeSelectIconClasses.Should().Contain("size-4");
        nativeSelectIconClasses.Should().Contain("text-muted-foreground");
    }
}
