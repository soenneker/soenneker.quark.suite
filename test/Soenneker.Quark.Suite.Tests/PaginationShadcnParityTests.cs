using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
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

        var paginationClasses = pagination.Find("[data-slot='pagination']").GetAttribute("class")!;
        var contentClasses = pagination.Find("[data-slot='pagination-content']").GetAttribute("class")!;
        var itemClasses = pagination.Find("[data-slot='pagination-item']").GetAttribute("class") ?? string.Empty;
        var linkClasses = pagination.Find("[data-slot='pagination-link']").GetAttribute("class")!;
        var ellipsisClasses = pagination.Find("[data-slot='pagination-ellipsis']").GetAttribute("class")!;
        var ariaCurrent = pagination.Find("[data-slot='pagination-link']").GetAttribute("aria-current");
        var ellipsisAriaHidden = pagination.Find("[data-slot='pagination-ellipsis']").GetAttribute("aria-hidden");

        pagination.Find("[data-slot='pagination-link']").TagName.Should().Be("A");
        paginationClasses.Should().Contain("mx-auto");
        paginationClasses.Should().Contain("flex");
        paginationClasses.Should().Contain("w-full");
        paginationClasses.Should().Contain("justify-center");
        paginationClasses.Should().NotContain("q-pagination");

        contentClasses.Should().Contain("flex");
        contentClasses.Should().Contain("flex-row");
        contentClasses.Should().Contain("items-center");
        contentClasses.Should().Contain("gap-1");
        contentClasses.Should().NotContain("gap-0.5");
        contentClasses.Should().NotContain("q-pagination-content");

        itemClasses.Should().NotContain("q-pagination-item");
        itemClasses.Should().NotContain("q-pagination-item-active");

        linkClasses.Should().Contain("inline-flex");
        linkClasses.Should().Contain("rounded-md");
        linkClasses.Should().Contain("size-9");
        linkClasses.Should().Contain("border");
        linkClasses.Should().Contain("bg-background");
        linkClasses.Should().Contain("shadow-xs");
        linkClasses.Should().Contain("hover:bg-accent");
        linkClasses.Should().Contain("focus-visible:ring-[3px]");
        linkClasses.Should().NotContain("rounded-lg");
        linkClasses.Should().NotContain("size-8");
        linkClasses.Should().NotContain("border-border");
        linkClasses.Should().NotContain("q-pagination-link");
        ariaCurrent.Should().Be("page");
        pagination.Find("[data-slot='pagination-link']").GetAttribute("data-active").Should().Be("true");

        ellipsisClasses.Should().Contain("flex");
        ellipsisClasses.Should().Contain("size-9");
        ellipsisClasses.Should().Contain("items-center");
        ellipsisClasses.Should().Contain("justify-center");
        ellipsisClasses.Should().NotContain("size-8");
        ellipsisClasses.Should().NotContain("q-pagination-ellipsis");
        ellipsisAriaHidden.Should().Be("true");
    }

    [Test]
    public void Pagination_previous_and_next_match_shadcn_labels_and_spacing()
    {
        var previous = Render<PaginationPrevious>(parameters => parameters.Add(p => p.Text, "Previous"));
        var next = Render<PaginationNext>(parameters => parameters.Add(p => p.Text, "Next"));

        var previousLink = previous.Find("[data-slot='pagination-link']");
        var nextLink = next.Find("[data-slot='pagination-link']");
        var previousClasses = previousLink.GetAttribute("class")!;
        var nextClasses = nextLink.GetAttribute("class")!;

        previousLink.TagName.Should().Be("A");
        nextLink.TagName.Should().Be("A");
        previousLink.GetAttribute("aria-label").Should().Be("Go to previous page");
        nextLink.GetAttribute("aria-label").Should().Be("Go to next page");

        previousClasses.Should().Contain("gap-1");
        previousClasses.Should().Contain("px-2.5");
        previousClasses.Should().Contain("sm:pl-2.5");
        previousClasses.Should().Contain("h-9");
        previousClasses.Should().Contain("has-[>svg]:px-3");
        previousClasses.Should().NotContain("pl-1.5!");
        previousClasses.Should().NotContain("rtl:pr-1.5!");
        previousClasses.Should().NotContain("h-8");
        previous.Find("svg").GetAttribute("class")!.Should().NotContain("rtl:rotate-180");

        nextClasses.Should().Contain("gap-1");
        nextClasses.Should().Contain("px-2.5");
        nextClasses.Should().Contain("sm:pr-2.5");
        nextClasses.Should().Contain("h-9");
        nextClasses.Should().Contain("has-[>svg]:px-3");
        nextClasses.Should().NotContain("pr-1.5!");
        nextClasses.Should().NotContain("rtl:pl-1.5!");
        nextClasses.Should().NotContain("h-8");
        next.Find("svg").GetAttribute("class")!.Should().NotContain("rtl:rotate-180");
    }
}
