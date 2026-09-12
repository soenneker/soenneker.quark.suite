using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.DataTables.Dtos.ServerSideRequest;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task DataTable_continues_after_one_hundred_interactions()
    {
        var count = 0;
        var cut = Render<DataTable>(p => p.Add(x => x.OnInteraction, _ => count++));
        var initial = count;
        for (var i = 0; i < 110; i++)
            await cut.InvokeAsync(() => cut.Instance.HandleSearch(i.ToString()).AsTask());
        count.Should().Be(initial + 110);
    }

    [Arguments(false)]
    [Arguments(true)]
    [Test]
    public async Task DataTable_coalesces_pending_search_to_latest_request(bool failFirst)
    {
        var release = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var terms = new List<string?>();
        var cut = Render<DataTable>(p => p.Add(x => x.OnInteraction, async request =>
        {
            terms.Add(request.Search?.Value);
            if (request.Search?.Value == "first")
            {
                started.SetResult();
                await release.Task;
                if (failFirst)
                    throw new System.InvalidOperationException("Simulated request failure");
            }
        }));
        var first = cut.InvokeAsync(() => cut.Instance.HandleSearch("first").AsTask());
        await started.Task;
        await cut.InvokeAsync(() => cut.Instance.HandleSearch("second").AsTask());
        await cut.InvokeAsync(() => cut.Instance.HandleSearch("latest").AsTask());
        release.SetResult();
        await first;
        terms.Should().Equal(null, "first", "latest");
    }

    [Test]
    public async Task DataTable_reset_preserves_registered_columns_and_reindexes_removals()
    {
        DataTableServerSideRequest? last = null;
        var showFirst = true;
        RenderFragment columns = builder =>
        {
            builder.OpenElement(0, "thead");
            builder.OpenElement(1, "tr");
            if (showFirst)
            {
                builder.OpenComponent<Th>(2);
                builder.SetKey("first");
                builder.AddAttribute(3, nameof(Th.Data), "first");
                builder.AddAttribute(4, nameof(Th.Sortable), true);
                builder.CloseComponent();
            }
            builder.OpenComponent<Th>(5);
            builder.SetKey("second");
            builder.AddAttribute(6, nameof(Th.Data), "second");
            builder.AddAttribute(7, nameof(Th.Sortable), true);
            builder.CloseComponent();
            builder.CloseElement();
            builder.CloseElement();
        };
        var cut = Render<DataTable>(p => p
            .Add(x => x.OnInteraction, request => last = request)
            .Add(x => x.TableContent, columns));
        await cut.InvokeAsync(() => cut.Instance.HandleColumnSort(1).AsTask());
        cut.Instance.SortBy.Should().Be("second");
        await cut.InvokeAsync(() => cut.Instance.Reset().AsTask());
        last!.Columns.Should().HaveCount(2);
        showFirst = false;
        await cut.InvokeAsync(cut.Instance.Refresh);
        cut.WaitForAssertion(() => cut.FindComponents<Th>().Should().ContainSingle());
        await cut.InvokeAsync(() => cut.Instance.HandleColumnSort(0).AsTask());
        last!.Columns.Should().ContainSingle();
        cut.Instance.SortBy.Should().Be("second");
    }
}
