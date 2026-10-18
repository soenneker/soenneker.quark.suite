using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark.Suite.Tests;
public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Searchable_select_remote_rejects_late_responses_even_if_provider_ignores_cancellation()
    {
        var first = new TaskCompletionSource<SelectSearchResult>();
        var second = new TaskCompletionSource<SelectSearchResult>();
        var cut = Render<SearchableSelect>(p => p.Add(c => c.DebounceMilliseconds, 0)
            .Add(c => c.LoadOptions, (request, _) => new ValueTask<SelectSearchResult>(request.Search == "a" ? first.Task : second.Task)));
        var firstInput = cut.Find("input").InputAsync(new ChangeEventArgs { Value = "a" });
        var secondInput = cut.Find("input").InputAsync(new ChangeEventArgs { Value = "ab" });
        second.SetResult(new([new("new", "Newest")])); await secondInput;
        first.SetResult(new([new("old", "Stale")])); await firstInput;
        cut.FindAll("[role='option']").Should().ContainSingle();
        cut.Find("[role='option']").TextContent.Should().Be("Newest");
    }

    [Test]
    public async Task Searchable_select_remote_resolves_initial_values_and_deduplicates_pages()
    {
        var cut = Render<SearchableSelect>(p => p.Add(c => c.Values, new[] { "saved" }).Add(c => c.Multiple, true)
            .Add(c => c.Resolve, (_, _) => ValueTask.FromResult<IReadOnlyList<SelectOption>>([new("saved", "Saved contact")]))
            .Add(c => c.LoadOptions, (request, _) => ValueTask.FromResult(request.ContinuationToken is null ? new SelectSearchResult([new("a", "Alpha")], "next") : new SelectSearchResult([new("a", "Alpha"), new("b", "Beta")]))));
        cut.Markup.Should().Contain("Saved contact");
        await cut.Find("input").FocusAsync(new Microsoft.AspNetCore.Components.Web.FocusEventArgs());
        await cut.FindAll("button").First(x => x.TextContent == "Load more").ClickAsync();
        cut.FindAll("[role='option']").Count.Should().Be(2);
        await cut.FindAll("[role='option']")[1].ClickAsync();
        cut.Instance.Values.Should().BeEquivalentTo(new[] { "saved", "b" });
    }

    [Test]
    public async Task Searchable_select_remote_retries_failed_page_and_preserves_previous_options()
    {
        var fail = true;
        var cut = Render<SearchableSelect>(p => p.Add(c => c.LoadOptions, (request, _) =>
        {
            if (request.ContinuationToken is null) return ValueTask.FromResult(new SelectSearchResult([new("a", "Alpha")], "next"));
            if (fail) throw new InvalidOperationException("offline");
            return ValueTask.FromResult(new SelectSearchResult([new("b", "Beta")], "next"));
        }));
        await cut.Find("input").FocusAsync();
        await cut.FindAll("button").First(x => x.TextContent == "Load more").ClickAsync();
        cut.FindAll("[role='option']").Should().ContainSingle();
        cut.Find("[role='alert']").TextContent.Should().NotBeEmpty();
        fail = false;
        await cut.FindAll("button").First(x => x.TextContent == "Retry").ClickAsync();
        cut.FindAll("[role='option']").Count.Should().Be(2);
        cut.FindAll("button").Any(x => x.TextContent == "Load more").Should().BeFalse();
    }

    [Test]
    public async Task Searchable_select_remote_resolves_single_value_and_selects_a_new_value()
    {
        var cut = Render<SearchableSelect>(p => p.Add(c => c.Value, "saved")
            .Add(c => c.Resolve, (_, _) => ValueTask.FromResult<IReadOnlyList<SelectOption>>([new("saved", "Saved contact")]))
            .Add(c => c.LoadOptions, (_, _) => ValueTask.FromResult(new SelectSearchResult([new("", "None"), new("b", "Beta", true)]))));
        cut.Find("input").GetAttribute("value").Should().Be("Saved contact");
        await cut.Find("input").FocusAsync();
        await cut.Find("[data-value='option:']").ClickAsync();
        cut.Instance.Value.Should().BeEmpty();
        cut.Find("input").GetAttribute("value").Should().Be("None");
    }

    [Test]
    public async Task Searchable_select_multiple_removes_chips_and_still_searches()
    {
        var cut = Render<SearchableSelect>(p => p.Add(c => c.Multiple, true).Add(c => c.Values, new[] { "a" })
            .Add(c => c.DebounceMilliseconds, 0)
            .Add(c => c.LoadOptions, (request, _) => ValueTask.FromResult(new SelectSearchResult([new(request.Search, request.Search)]))));
        await cut.Find("[data-slot='combobox-chip-remove']").ClickAsync();
        cut.Instance.Values.Should().BeEmpty();
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "Beta" });
        cut.Find("[role='option']").TextContent.Should().Contain("Beta");
    }

    [Test]
    public async Task Searchable_select_remote_reopens_with_unfiltered_options_after_selection()
    {
        var searches = new List<string>();
        var cut = Render<SearchableSelect>(p => p.Add(c => c.DebounceMilliseconds, 0)
            .Add(c => c.LoadOptions, (request, _) =>
            {
                searches.Add(request.Search);
                return ValueTask.FromResult(new SelectSearchResult([new("b", "Beta")]));
            }));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "Beta" });
        await cut.Find("[role='option']").ClickAsync();
        await cut.Find("input").FocusAsync();
        searches.Should().Equal("Beta", "");
        cut.Find("input").GetAttribute("value").Should().BeEmpty();
        cut.FindAll("[role='option']").Should().ContainSingle();
    }

}
