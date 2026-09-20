using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class AdaptiveTableLayoutTests : BunitContext
{
    private readonly RecordingTablesInterop _interop = new();

    public AdaptiveTableLayoutTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddSingleton<ITablesInterop>(_interop);
    }

    [Test]
    public async Task Table_attaches_once_reconfigures_and_cleans_up()
    {
        var cut = Render<Table>(p => p.Add(c => c.AdaptiveLayout, true).AddChildContent("<tbody><tr><td>Job</td></tr></tbody>"));
        cut.FindAll("colgroup").Should().ContainSingle();
        _interop.Starts.Should().Be(1);
        cut.Render(p => p.AddChildContent("<tbody><tr><td>Updated job</td></tr></tbody>"));
        _interop.Starts.Should().Be(1);
        cut.Render(p => p.Add(c => c.ColumnSizing, new TableColumnSizingOptions { Padding = 40 }));
        _interop.Starts.Should().Be(2);
        _interop.Stops.Should().Be(1);
        _interop.Options!.Padding.Should().Be(40);
        await cut.Instance.DisposeAsync();
        await cut.Instance.DisposeAsync();
        _interop.Stops.Should().Be(2);
    }

    [Test]
    public void DataTable_detaches_when_hidden_and_attaches_to_the_new_table_when_shown()
    {
        var cut = Render<DataTable>(p => p.Add(c => c.AdaptiveLayout, true)
            .Add(c => c.TableContent, "<tbody><tr><td>Job</td></tr></tbody>"));
        _interop.Starts.Should().Be(1);
        cut.Render(p => p.Add(c => c.Visible, false));
        _interop.Stops.Should().Be(1);
        cut.Render(p => p.Add(c => c.Visible, true));
        _interop.Starts.Should().Be(2);
        cut.Render(p => p.Add(c => c.AdaptiveLayout, false));
        _interop.Stops.Should().Be(2);
        cut.FindAll("colgroup").Should().BeEmpty();
    }

    [Test]
    public void Child_table_inherits_adaptive_options_without_a_duplicate_attachment()
    {
        Render<DataTable>(p => p.Add(c => c.AdaptiveLayout, true)
            .Add(c => c.ColumnSizing, new TableColumnSizingOptions { Padding = 32 })
            .AddChildContent<Table>(child => child.AddChildContent("<tbody><tr><td>Job</td></tr></tbody>")));
        _interop.Starts.Should().Be(1);
        _interop.Options!.Padding.Should().Be(32);
    }

    [Test]
    public async Task Disposal_waits_for_pending_attachment_and_then_removes_it()
    {
        _interop.Pending = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var cut = Render<Table>(p => p.Add(c => c.AdaptiveLayout, true));
        var disposal = cut.Instance.DisposeAsync().AsTask();
        disposal.IsCompleted.Should().BeFalse();
        _interop.Pending.SetResult();
        await disposal;
        _interop.Stops.Should().Be(1);
    }

    [Test]
    public void Native_layout_remains_the_default()
    {
        var cut = Render<Table>();
        _interop.Starts.Should().Be(0);
        cut.FindAll("colgroup").Should().BeEmpty();
    }

    private sealed class RecordingTablesInterop : ITablesInterop
    {
        public int Starts;
        public int Stops;
        public TableColumnSizingOptions? Options;
        public TaskCompletionSource? Pending;
        public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
        public ValueTask StartAdaptiveLayout(ElementReference element, ElementReference columns, TableColumnSizingOptions options, CancellationToken cancellationToken = default)
        {
            Starts++;
            Options = options;
            return Pending is null ? ValueTask.CompletedTask : new ValueTask(Pending.Task);
        }
        public ValueTask StopAdaptiveLayout(ElementReference element, CancellationToken cancellationToken = default)
        {
            Stops++;
            return ValueTask.CompletedTask;
        }
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
