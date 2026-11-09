using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed class QueryInputTests : BunitContext
{
    private readonly QueryInputTestInterop _interop = new();
    public QueryInputTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddBradixSuiteAsScoped();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddSingleton<IInputInterop>(_interop);
    }

    [Test]
    public void Parser_preserves_free_text_quotes_exclusions_and_repeated_filters()
    {
        QueryInputResult result = QueryInputParser.Parse("login bug state:open -assignee:me label:\"needs review\" label:bug \"literal:term\"");
        result.Text.Should().Be("login bug literal:term");
        result.Filters.Should().Equal(new QueryInputFilter("state", "open"), new QueryInputFilter("assignee", "me", true),
            new QueryInputFilter("label", "needs review"), new QueryInputFilter("label", "bug"));
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void Parser_reports_incomplete_and_unknown_filters_and_round_trips_escapes()
    {
        QueryInputParser.Parse("state: unknown:\"unfinished", [new("state", "State")]).Errors.Should().HaveCount(3);
        const string value = "a \"quote\" \\ folder:one";
        QueryInputParser.Parse("label:" + QueryInputParser.Quote(value)).Filters[0].Value.Should().Be(value);
    }

    [Test]
    public async Task Suggestions_complete_fields_and_quoted_values_and_escape_closes()
    {
        var cut = Render<QueryInput>(p => p.Add(x => x.Fields, [new("label", "Label", [new("needs review", "Needs review")])]));
        await cut.Find("input").FocusAsync(new());
        cut.FindAll("[role=option]").Should().HaveCount(1);
        await cut.Find("input").KeyDownAsync(new Microsoft.AspNetCore.Components.Web.KeyboardEventArgs { Key = "Enter" });
        cut.Instance.Value.Should().Be("label:");
        cut.WaitForAssertion(() => cut.FindAll("[role=option]").Should().HaveCount(1));
        await cut.Find("input").KeyDownAsync(new Microsoft.AspNetCore.Components.Web.KeyboardEventArgs { Key = "Enter" });
        cut.Instance.Value.Should().Be("label:\"needs review\" ");
        _interop.Selection = null;
        await cut.Find("input").InputAsync(new() { Value = "lab" });
        await cut.Find("input").KeyDownAsync(new Microsoft.AspNetCore.Components.Web.KeyboardEventArgs { Key = "Escape" });
        cut.FindAll("[role=listbox]").Should().BeEmpty();
    }

    [Test]
    public async Task Completion_replaces_only_the_token_at_the_caret()
    {
        const string query = "bug state:op assignee:me";
        _interop.Selection = new InputSelectionSnapshot { Start = 12, End = 12, Value = query };
        var cut = Render<QueryInput>(p => p.Add(x => x.Value, query)
            .Add(x => x.Fields, [new("state", "State", [new("open", "Open")]), new("assignee", "Assignee")]));
        await cut.Find("input").FocusAsync(new());
        await cut.Find("input").KeyDownAsync(new Microsoft.AspNetCore.Components.Web.KeyboardEventArgs { Key = "Enter" });
        cut.Instance.Value.Should().Be("bug state:open assignee:me");
        _interop.Selection!.Start.Should().Be(14);
    }

    [Test]
    public async Task Blur_cancels_provider_and_ignores_its_late_result()
    {
        var response = new TaskCompletionSource<IReadOnlyList<SelectOption>>(TaskCreationOptions.RunContinuationsAsynchronously);
        CancellationToken token = default;
        var cut = Render<QueryInput>(p => p.Add(x => x.Value, "state:")
            .Add(x => x.Fields, [new("state", "State")])
            .Add(x => x.SuggestValues, (_, cancellation) => { token = cancellation; return new ValueTask<IReadOnlyList<SelectOption>>(response.Task); }));
        Task focus = cut.Find("input").FocusAsync(new());
        cut.WaitForAssertion(() => token.CanBeCanceled.Should().BeTrue());
        await cut.Find("input").BlurAsync(new());
        token.IsCancellationRequested.Should().BeTrue();
        response.SetResult([new("open", "Open")]);
        await focus;
        cut.FindAll("[role=listbox]").Should().BeEmpty();
    }
}
