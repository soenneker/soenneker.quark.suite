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
        ComponentFactories.AddStub<Spinner>();
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
    [Arguments("source", true)]
    [Arguments("assigned", false)]
    public async Task Selecting_field_opens_async_values_without_typing(string field, bool mouse)
    {
        var response = new TaskCompletionSource<IReadOnlyList<SelectOption>>(TaskCreationOptions.RunContinuationsAsynchronously);
        QueryInputRequest? requested = null;
        var cut = Render<QueryInput>(p => p.Add(x => x.Fields, [new(field, field)])
            .Add(x => x.SuggestValues, (request, _) =>
            {
                requested = request;
                return new ValueTask<IReadOnlyList<SelectOption>>(response.Task);
            }));
        await cut.Find("input").FocusAsync(new());
        Task choose = mouse
            ? cut.Find("[role=option]").ClickAsync(new())
            : cut.Find("input").KeyDownAsync(new Microsoft.AspNetCore.Components.Web.KeyboardEventArgs { Key = "Enter" });
        cut.WaitForAssertion(() => requested.Should().NotBeNull());
        cut.FindAll("[data-slot=query-input-loading]").Should().HaveCount(1);
        cut.Find("input").GetAttribute("aria-busy").Should().Be("true");
        requested!.Field.Should().Be(field);
        requested.Search.Should().BeEmpty();
        response.SetResult([new("option-id", "Available option")]);
        await choose;
        cut.WaitForAssertion(() => cut.FindAll("[data-slot=query-input-loading]").Should().BeEmpty());
        cut.Find("input").GetAttribute("aria-busy").Should().Be("false");
        cut.WaitForAssertion(() => cut.Find("[role=option]").TextContent.Should().Be("Available option"));
        await cut.Find("[role=option]").ClickAsync(new());
        cut.Instance.Value.Should().Be(field + ":option-id ");
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
        cut.FindAll("[data-slot=query-input-loading]").Should().BeEmpty();
        cut.Find("input").GetAttribute("aria-busy").Should().Be("false");
        response.SetResult([new("open", "Open")]);
        await focus;
        cut.FindAll("[role=listbox]").Should().BeEmpty();
    }
    [Test]
    public void Logical_operators_preserve_precedence_and_quoted_literals()
    {
        QueryInputResult result = QueryInputParser.Parse("state:open or state:closed AND -assignee:me urgent");
        result.Errors.Should().BeEmpty();
        result.Text.Should().Be("urgent");
        result.Expression.Should().Be(new QueryInputBinaryExpression(
            new QueryInputFilterExpression(new("state", "open")), QueryInputOperator.Or,
            new QueryInputBinaryExpression(
                new QueryInputBinaryExpression(new QueryInputFilterExpression(new("state", "closed")), QueryInputOperator.And,
                    new QueryInputFilterExpression(new("assignee", "me", true))), QueryInputOperator.And,
                new QueryInputTextExpression("urgent"))));
        QueryInputParser.Parse("\"AND\" \"OR\" label:OR").Text.Should().Be("AND OR");
        QueryInputParser.Parse(QueryInputParser.Quote("AND")).Expression.Should().Be(new QueryInputTextExpression("AND"));
    }

    [Test]
    [Arguments("AND state:open")]
    [Arguments("state:open OR")]
    [Arguments("state:open AND OR state:closed")]
    public void Logical_operators_require_operands(string query)
    {
        QueryInputParser.Parse(query).Errors.Should().NotBeEmpty();
    }

    [Test]
    public async Task Field_server_provider_receives_logical_context_and_limit()
    {
        QueryInputRequest? requested = null;
        var cut = Render<QueryInput>(p => p.Add(x => x.Value, "state:open OR assignee:ja")
            .Add(x => x.MaxSuggestions, 1)
            .Add(x => x.Fields, [new("state", "State"), new("assignee", "Assignee")
            {
                SuggestValues = (request, _) =>
                {
                    requested = request;
                    return ValueTask.FromResult<IReadOnlyList<SelectOption>>([new("jake", "Jake"), new("jane", "Jane")]);
                }
            }])
            .Add(x => x.SuggestValues, (_, _) => throw new System.InvalidOperationException("Field provider should override global provider.")));
        await cut.Find("input").FocusAsync(new());
        requested!.Search.Should().Be("ja");
        requested.MaxSuggestions.Should().Be(1);
        requested.Parsed.Expression.Should().BeOfType<QueryInputBinaryExpression>().Which.Operator.Should().Be(QueryInputOperator.Or);
        cut.FindAll("[role=option]").Should().HaveCount(1);
        await cut.Find("[role=option]").ClickAsync(new());
        cut.Instance.Value.Should().Be("state:open OR assignee:jake ");
    }

    [Test]
    public async Task Superseded_lookup_does_not_hide_current_loading_spinner()
    {
        var first = new TaskCompletionSource<IReadOnlyList<SelectOption>>(TaskCreationOptions.RunContinuationsAsynchronously);
        var second = new TaskCompletionSource<IReadOnlyList<SelectOption>>(TaskCreationOptions.RunContinuationsAsynchronously);
        int calls = 0;
        var cut = Render<QueryInput>(p => p.Add(x => x.Value, "state:")
            .Add(x => x.Fields, [new("state", "State")])
            .Add(x => x.SuggestValues, (_, _) => new ValueTask<IReadOnlyList<SelectOption>>(++calls == 1 ? first.Task : second.Task)));
        Task focus = cut.Find("input").FocusAsync(new());
        cut.WaitForAssertion(() => calls.Should().Be(1));
        Task input = cut.Find("input").InputAsync(new() { Value = "state:o" });
        cut.WaitForAssertion(() => calls.Should().Be(2));
        first.SetResult([new("closed", "Closed")]);
        await focus;
        cut.FindAll("[data-slot=query-input-loading]").Should().HaveCount(1);
        cut.Find("input").GetAttribute("aria-busy").Should().Be("true");
        second.SetResult([]);
        await input;
        cut.WaitForAssertion(() => cut.FindAll("[data-slot=query-input-loading]").Should().BeEmpty());
        cut.Find("input").GetAttribute("aria-busy").Should().Be("false");
    }

}
