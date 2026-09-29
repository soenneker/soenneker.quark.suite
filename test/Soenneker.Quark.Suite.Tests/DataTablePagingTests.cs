using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Soenneker.DataTables.Dtos.ServerSideRequest;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task DataTable_continuation_paging_survives_parent_renders_without_an_exact_total()
    {
        DataTableServerSideRequest? requested = null;
        var cut = Render<DataTable>(p => p
            .Add(c => c.OnInteraction, request => requested = request));

        await cut.InvokeAsync(() => cut.Instance.UpdateContinuationTokenPaging(10, "page-two"));
        cut.Instance.TotalPages.Should().Be(2);
        cut.Instance.HasExactTotalRecords.Should().BeFalse();
        cut.Render();
        cut.Instance.TotalPages.Should().Be(2);
        await cut.InvokeAsync(() => cut.Instance.HandleGoToPage(2).AsTask());
        requested!.ContinuationToken.Should().Be("page-two");

        await cut.InvokeAsync(() => cut.Instance.UpdateContinuationTokenPaging(3, null, "page-two"));
        cut.Instance.TotalRecordsCount.Should().Be(13);
        cut.Instance.HasExactTotalRecords.Should().BeTrue();
        cut.Instance.TotalPages.Should().Be(2);
    }

    [Test]
    public void Continuation_paging_keeps_the_known_end_when_visiting_an_earlier_page()
    {
        var paging = new DataTableContinuationTokenPaging();
        paging.UpdateFromResponse(10, 10, "page-two");
        paging.UpdateVirtualPage(10, 10, "page-two");
        paging.UpdateFromResponse(10, 3, null, "page-two");
        paging.CalculateTotalRecords(10).Should().Be(13);
        paging.UpdateVirtualPage(0, 10, null);
        paging.UpdateFromResponse(10, 10, "page-two");
        paging.CalculateTotalRecords(10).Should().Be(13);
    }
}
