using System.Text.Json;
using System.Threading.Tasks;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class InteropJsonTests
{
    [Test]
    public void Options_preserve_browser_property_names_and_false_values()
    {
        var options = new ScrollspyInteropOptions { TargetSelector = "#article", Smooth = false, History = false, Offset = 80 };
        var payload = JsonSerializer.SerializeToElement(options, QuarkInteropJsonContext.Default.ScrollspyInteropOptions);
        payload.GetProperty("targetSelector").GetString().Should().Be("#article");
        payload.GetProperty("smooth").GetBoolean().Should().BeFalse();
        payload.GetProperty("history").GetBoolean().Should().BeFalse();
        payload.GetProperty("offset").GetInt32().Should().Be(80);
    }

    [Test]
    public void Browser_results_support_records_collections_and_named_position_properties()
    {
        using var headings = JsonDocument.Parse("""[{"id":"intro","title":"Introduction","level":2}]""");
        var items = QuarkInteropJson.Deserialize(headings.RootElement, QuarkInteropJsonContext.Default.OnThisPageTocItemArray)!;
        items[0].Should().Be(new OnThisPageTocItem("intro", "Introduction", 2));
        using var position = JsonDocument.Parse("""{"x":125,"y":-40}""");
        var point = QuarkInteropJson.Deserialize(position.RootElement, QuarkInteropJsonContext.Default.FloatingWindowPosition)!;
        point.X.Should().Be(125);
        point.Y.Should().Be(-40);
        using var heights = JsonDocument.Parse("""{"toast-1":42.5}""");
        QuarkInteropJson.Deserialize(heights.RootElement, QuarkInteropJsonContext.Default.DictionaryStringDouble)!["toast-1"].Should().Be(42.5);
    }

    [Test]
    public void Input_selection_null_and_empty_values_remain_distinct()
    {
        QuarkInteropJson.Deserialize(null, QuarkInteropJsonContext.Default.InputSelectionSnapshot).Should().BeNull();
        using var json = JsonDocument.Parse("""{"start":0,"end":0,"value":""}""");
        var result = QuarkInteropJson.Deserialize(json.RootElement, QuarkInteropJsonContext.Default.InputSelectionSnapshot)!;
        result.Value.Should().BeEmpty();
        result.Start.Should().Be(0);
    }

    [Test]
    public async Task Connection_validation_callback_uses_generated_metadata_in_both_directions()
    {
        NodeEditorConnectionValidationRequest? received = null;
        var editor = new NodeEditor
        {
            ConnectionValidator = request =>
            {
                received = request;
                return ValueTask.FromResult(NodeEditorConnectionValidationResult.Accept());
            }
        };
        using var json = JsonDocument.Parse("""{"sourceNodeId":"a","sourcePortId":"out","targetNodeId":"b","targetPortId":"in","edgeId":null}""");
        var result = await editor.InvokeConnectionValidationRequestedFromJson(json.RootElement);
        received!.SourceNodeId.Should().Be("a");
        received.TargetPortId.Should().Be("in");
        received.EdgeId.Should().BeNull();
        result.GetProperty("allowed").GetBoolean().Should().BeTrue();
    }

    [Test]
    public void Reorder_payload_preserves_nested_lists_and_missing_member_defaults()
    {
        using var json = JsonDocument.Parse("""{"newIndex":2,"fromItemIds":["a"],"toItemIds":["b","c"]}""");
        var result = JsonSerializer.Deserialize(json.RootElement, QuarkInteropJsonContext.Default.SortableReorderEventArgs)!;
        result.OldIndex.Should().Be(-1);
        result.NewIndex.Should().Be(2);
        result.FromItemIds.Should().Equal("a");
        result.ToItemIds.Should().Equal("b", "c");
    }
}
