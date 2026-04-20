using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Pagination_slots_match_shadcn_base_classes()
    {
        var pagination = Render<Pagination>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<PaginationContent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<PaginationItem>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(itemBuilder =>
                    {
                        itemBuilder.OpenComponent<PaginationLink>(0);
                        itemBuilder.AddAttribute(1, "Active", true);
                        itemBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(linkBuilder => linkBuilder.AddContent(0, "1")));
                        itemBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<PaginationItem>(2);
                    contentBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(itemBuilder =>
                    {
                        itemBuilder.OpenComponent<PaginationEllipsis>(0);
                        itemBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        string paginationClasses = pagination.Find("[data-slot='pagination']").GetAttribute("class")!;
        string contentClasses = pagination.Find("[data-slot='pagination-content']").GetAttribute("class")!;
        string itemClasses = pagination.Find("[data-slot='pagination-item']").GetAttribute("class")!;
        string linkClasses = pagination.Find("[data-slot='pagination-link']").GetAttribute("class")!;
        string ellipsisClasses = pagination.Find("[data-slot='pagination-ellipsis']").GetAttribute("class")!;
        string? ariaCurrent = pagination.Find("[data-slot='pagination-link']").GetAttribute("aria-current");

        paginationClasses.Should().Contain("mx-auto");
        paginationClasses.Should().Contain("flex");
        paginationClasses.Should().Contain("w-full");
        paginationClasses.Should().Contain("justify-center");
        paginationClasses.Should().NotContain("q-pagination");

        contentClasses.Should().Contain("flex");
        contentClasses.Should().Contain("items-center");
        contentClasses.Should().Contain("gap-1");
        contentClasses.Should().NotContain("q-pagination-content");

        itemClasses.Should().Contain("list-none");
        itemClasses.Should().NotContain("q-pagination-item");
        itemClasses.Should().NotContain("q-pagination-item-active");

        linkClasses.Should().Contain("inline-flex");
        linkClasses.Should().Contain("rounded-md");
        linkClasses.Should().Contain("size-8");
        linkClasses.Should().Contain("border-border");
        linkClasses.Should().Contain("bg-background");
        linkClasses.Should().NotContain("q-pagination-link");
        ariaCurrent.Should().Be("page");

        ellipsisClasses.Should().Contain("flex");
        ellipsisClasses.Should().Contain("size-9");
        ellipsisClasses.Should().Contain("items-center");
        ellipsisClasses.Should().Contain("justify-center");
        ellipsisClasses.Should().NotContain("q-pagination-ellipsis");
    }
}
