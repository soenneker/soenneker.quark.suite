using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Node_editor_node_exposes_position_and_selection_state()
    {
        var node = new NodeEditorNodeModel { Id = "qualify", X = 120.5, Y = 240, Selectable = true };
        var cut = Render<NodeEditorNode>(parameters => parameters
            .Add(component => component.Node, node)
            .Add(component => component.Selected, true)
            .Add(component => component.ChildContent, "Qualify"));

        var root = cut.Find("[data-slot='node-editor-node']");
        root.GetAttribute("data-node-id").Should().Be("qualify");
        root.GetAttribute("data-node-x").Should().Be("120.5");
        root.GetAttribute("data-selectable").Should().Be("true");
        root.GetAttribute("data-selected").Should().Be("true");
        root.GetAttribute("style").Should().Contain("translate3d(120.5px, 240px, 0)");
    }

    [Test]
    public void Node_editor_port_uses_cascaded_node_identity()
    {
        var cut = Render<CascadingValue<NodeEditorNodeContext>>(parameters => parameters
            .Add(component => component.Value, new NodeEditorNodeContext("decision"))
            .Add(component => component.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<NodeEditorPort>(0);
                builder.AddAttribute(1, nameof(NodeEditorPort.PortId), "approved");
                builder.AddAttribute(2, nameof(NodeEditorPort.Type), (object)NodeEditorPortType.Source);
                builder.AddAttribute(3, nameof(NodeEditorPort.Placement), (object)NodeEditorPortPlacement.Right);
                builder.AddAttribute(4, nameof(NodeEditorPort.Disabled), true);
                builder.AddAttribute(5, nameof(NodeEditorPort.Offset), 0.75);
                builder.AddAttribute(6, nameof(NodeEditorPort.MaxConnections), 2);
                builder.CloseComponent();
            })));

        var port = cut.Find("[data-slot='node-editor-port']");
        port.GetAttribute("data-node-id").Should().Be("decision");
        port.GetAttribute("data-port-id").Should().Be("approved");
        port.GetAttribute("data-placement").Should().Be("right");
        port.GetAttribute("data-position").Should().Be("0.75");
        port.GetAttribute("data-max-connections").Should().Be("2");
        port.GetAttribute("style").Should().Contain("top: 75%");
        port.GetAttribute("data-disabled").Should().Be("true");
        port.HasAttribute("disabled").Should().BeTrue();
    }

    [Test]
    public void Node_editor_palette_item_exposes_native_drag_contract()
    {
        var cut = Render<NodeEditorPaletteItem>(parameters => parameters
            .Add(component => component.Type, "message")
            .Add(component => component.Data, """{"channel":"email"}""")
            .AddChildContent("Message"));

        var item = cut.Find("[data-slot='node-editor-palette-item']");
        item.TagName.Should().Be("BUTTON");
        item.GetAttribute("type").Should().Be("button");
        item.GetAttribute("draggable").Should().Be("true");
        item.GetAttribute("data-palette-type").Should().Be("message");
        item.GetAttribute("data-palette-data").Should().Be("""{"channel":"email"}""");
    }

    [Test]
    public void Node_editor_renders_quiet_orthogonal_edge_contract()
    {
        NodeEditorNodeModel[] nodes =
        [
            new() { Id = "source" },
            new() { Id = "target", Y = 160 }
        ];
        NodeEditorEdgeModel[] edges =
        [
            new()
            {
                Id = "source-target",
                SourceNodeId = "source",
                SourcePortId = "out",
                TargetNodeId = "target",
                TargetPortId = "in",
                Label = "Continue",
                Selectable = false
            }
        ];

        var cut = Render<NodeEditor>(parameters => parameters
            .Add(component => component.Nodes, nodes)
            .Add(component => component.Edges, edges)
            .Add(component => component.NodeTemplate, node => builder => builder.AddContent(0, node.Id)));

        var edge = cut.Find("[data-edge-id='source-target']");
        edge.GetAttribute("data-selectable").Should().Be("false");
        edge.GetAttribute("role").Should().Be("button");
        edge.GetAttribute("aria-label").Should().Be("Continue");
        edge.GetAttribute("tabindex").Should().Be("-1");

        var path = edge.QuerySelector("[data-edge-path]")!;
        path.GetAttribute("stroke-width").Should().Be("1.5");
        path.HasAttribute("marker-end").Should().BeFalse();
        edge.QuerySelectorAll("[data-edge-endpoint]").Should().OnlyContain(endpoint => endpoint.GetAttribute("aria-hidden") == "true");
        edge.QuerySelectorAll("[data-edge-endpoint][role]").Should().BeEmpty();

        edge.QuerySelector("[data-edge-label-text]")!.TextContent.Should().Be("Continue");
    }

    [Test]
    public void Node_editor_reports_duplicate_node_identifiers_clearly()
    {
        NodeEditorNodeModel[] nodes =
        [
            new() { Id = "duplicate" },
            new() { Id = "duplicate" }
        ];

        Action render = () => Render<NodeEditor>(parameters => parameters
            .Add(component => component.Nodes, nodes)
            .Add(component => component.NodeTemplate, node => builder => builder.AddContent(0, node.Id)));

        render.Should().Throw<InvalidOperationException>()
            .WithMessage("*Duplicate identifier: 'duplicate'*");
    }

    [Test]
    public void Node_editor_renders_empty_branch_as_a_dashed_connection_and_add_action()
    {
        NodeEditorNodeModel[] nodes = [new() { Id = "decision" }];
        NodeEditorAddHandleModel[] handles =
        [
            new()
            {
                Id = "otherwise",
                SourceNodeId = "decision",
                SourcePortId = "otherwise",
                X = 400,
                Y = 220,
                Label = "Otherwise"
            }
        ];

        var cut = Render<NodeEditor>(parameters => parameters
            .Add(component => component.Nodes, nodes)
            .Add(component => component.AddHandles, handles)
            .Add(component => component.NodeTemplate, node => builder =>
            {
                builder.OpenComponent<NodeEditorPort>(0);
                builder.AddAttribute(1, nameof(NodeEditorPort.PortId), "otherwise");
                builder.CloseComponent();
            }));

        var placeholder = cut.Find("[data-add-handle-edge='otherwise']");
        placeholder.QuerySelector("[data-add-handle-path]")!.GetAttribute("stroke-dasharray").Should().Be("3 4");
        placeholder.QuerySelector("[data-edge-label-text]")!.TextContent.Should().Be("Otherwise");

        var handle = cut.Find("[data-slot='node-editor-add-handle']");
        handle.GetAttribute("data-source-node").Should().Be("decision");
        handle.GetAttribute("data-source-port").Should().Be("otherwise");
        handle.GetAttribute("aria-label").Should().Be("Otherwise");
    }

    [Test]
    public void Node_editor_reports_missing_edge_nodes_clearly()
    {
        NodeEditorNodeModel[] nodes = [new() { Id = "source" }];
        NodeEditorEdgeModel[] edges =
        [
            new()
            {
                Id = "broken",
                SourceNodeId = "source",
                SourcePortId = "out",
                TargetNodeId = "missing",
                TargetPortId = "in"
            }
        ];

        Action render = () => Render<NodeEditor>(parameters => parameters
            .Add(component => component.Nodes, nodes)
            .Add(component => component.Edges, edges)
            .Add(component => component.NodeTemplate, node => builder => builder.AddContent(0, node.Id)));

        render.Should().Throw<InvalidOperationException>()
            .WithMessage("*references missing target node 'missing'*");
    }

    [Test]
    public void Node_editor_reports_invalid_zoom_configuration_clearly()
    {
        var options = new NodeEditorOptions { MinZoom = 2, MaxZoom = 1 };

        Action render = () => Render<NodeEditor>(parameters => parameters
            .Add(component => component.Options, options)
            .Add(component => component.NodeTemplate, _ => _ => { }));

        render.Should().Throw<InvalidOperationException>()
            .WithMessage("*MaxZoom*greater than or equal to MinZoom*");
    }

    [Test]
    public void Node_editor_port_requires_a_non_empty_identifier()
    {
        Action render = () => Render<CascadingValue<NodeEditorNodeContext>>(parameters => parameters
            .Add(component => component.Value, new NodeEditorNodeContext("node"))
            .Add(component => component.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<NodeEditorPort>(0);
                builder.CloseComponent();
            })));

        render.Should().Throw<InvalidOperationException>()
            .WithMessage("*requires a non-empty PortId*");
    }

    [Test]
    public void Node_editor_port_reports_invalid_capacity_clearly()
    {
        Action render = () => Render<CascadingValue<NodeEditorNodeContext>>(parameters => parameters
            .Add(component => component.Value, new NodeEditorNodeContext("node"))
            .Add(component => component.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<NodeEditorPort>(0);
                builder.AddAttribute(1, nameof(NodeEditorPort.PortId), "output");
                builder.AddAttribute(2, nameof(NodeEditorPort.MaxConnections), 0);
                builder.CloseComponent();
            })));

        render.Should().Throw<InvalidOperationException>()
            .WithMessage("*MaxConnections*greater than zero*");
    }

    [Test]
    public void Node_editor_reports_missing_add_handle_nodes_clearly()
    {
        NodeEditorAddHandleModel[] handles =
        [
            new()
            {
                Id = "orphan",
                SourceNodeId = "missing",
                SourcePortId = "output",
                X = 10,
                Y = 20
            }
        ];

        Action render = () => Render<NodeEditor>(parameters => parameters
            .Add(component => component.AddHandles, handles)
            .Add(component => component.NodeTemplate, _ => _ => { }));

        render.Should().Throw<InvalidOperationException>()
            .WithMessage("*references missing source node 'missing'*");
    }

    [Test]
    public async Task Node_editor_exposes_controlled_history_commands()
    {
        var undoCount = 0;
        var redoCount = 0;
        var cut = Render<NodeEditor>(parameters => parameters
            .Add(component => component.CanUndo, true)
            .Add(component => component.CanRedo, true)
            .Add(component => component.OnUndoRequested, () => undoCount++)
            .Add(component => component.OnRedoRequested, () => redoCount++)
            .Add(component => component.NodeTemplate, _ => _ => { }));

        await cut.Instance.InvokeUndoRequested();
        await cut.Instance.InvokeRedoRequested();

        undoCount.Should().Be(1);
        redoCount.Should().Be(1);
    }

    [Test]
    public async Task Node_editor_duplicate_request_includes_a_snapped_suggested_position()
    {
        var node = new NodeEditorNodeModel { Id = "message", X = 40, Y = 80 };
        NodeEditorDuplicateRequest? request = null;
        var cut = Render<NodeEditor>(parameters => parameters
            .Add(component => component.Nodes, new[] { node })
            .Add(component => component.Options, new NodeEditorOptions { GridSize = 12 })
            .Add(component => component.OnDuplicateRequested, value => request = value)
            .Add(component => component.NodeTemplate, _ => _ => { }));

        await cut.Instance.InvokeDuplicateRequested("message");

        request.Should().NotBeNull();
        request!.NodeId.Should().Be("message");
        request.SuggestedX.Should().Be(64);
        request.SuggestedY.Should().Be(104);
    }

    [Test]
    public void Node_editor_drop_request_can_identify_an_edge_for_insertion()
    {
        var request = new NodeEditorDropRequest
        {
            Type = "delay",
            X = 120,
            Y = 180,
            EdgeId = "edge-4"
        };

        request.EdgeId.Should().Be("edge-4");
    }

    [Test]
    public async Task Node_editor_commits_inline_edge_label_edits()
    {
        NodeEditorNodeModel[] nodes =
        [
            new() { Id = "source" },
            new() { Id = "target", Y = 160 }
        ];
        var edge = new NodeEditorEdgeModel
        {
            Id = "source-target",
            SourceNodeId = "source",
            SourcePortId = "out",
            TargetNodeId = "target",
            TargetPortId = "in",
            Label = "Continue"
        };
        NodeEditorEdgeLabelChangedEventArgs? changed = null;
        var cut = Render<NodeEditor>(parameters => parameters
            .Add(component => component.Nodes, nodes)
            .Add(component => component.Edges, new[] { edge })
            .Add(component => component.OnEdgeLabelChanged, args => changed = args)
            .Add(component => component.NodeTemplate, node => builder => builder.AddContent(0, node.Id)));

        await cut.Instance.InvokeEdgeLabelEditRequested(edge.Id);
        var input = cut.Find("[data-edge-label-input]");
        await input.InputAsync(new ChangeEventArgs { Value = "Approved" });
        await input.KeyDownAsync(new KeyboardEventArgs { Key = "Enter" });

        edge.Label.Should().Be("Approved");
        changed.Should().NotBeNull();
        changed!.EdgeId.Should().Be(edge.Id);
        changed.PreviousLabel.Should().Be("Continue");
        changed.Label.Should().Be("Approved");
        cut.Find("[data-edge-label-text]").TextContent.Should().Be("Approved");
    }

    [Test]
    public async Task Node_editor_autosave_debounces_graph_changes()
    {
        var saveCount = 0;
        var states = new List<AutoSaveState>();
        var gate = new object();

        var cut = Render<NodeEditor>(parameters => parameters
            .Add(component => component.AutoSave, true)
            .Add(component => component.AutoSaveDelay, 150)
            .Add(component => component.OnAutoSave, (CancellationToken _) =>
            {
                Interlocked.Increment(ref saveCount);
                return ValueTask.CompletedTask;
            })
            .Add(component => component.AutoSaveStateChanged, state =>
            {
                lock (gate)
                {
                    states.Add(state);
                }

                return Task.CompletedTask;
            })
            .Add(component => component.NodeTemplate, _ => _ => { }));

        await cut.Instance.NotifyChanged();
        await Task.Delay(50);
        await cut.Instance.NotifyChanged();
        await Task.Delay(50);
        await cut.Instance.NotifyChanged();

        await Task.Delay(75);
        Volatile.Read(ref saveCount).Should().Be(0);

        cut.WaitForAssertion(() => Volatile.Read(ref saveCount).Should().Be(1), TimeSpan.FromSeconds(2));
        cut.WaitForAssertion(() =>
        {
            cut.Instance.AutoSaveState.Should().Be(AutoSaveState.Saved);
            cut.Instance.HasAutoSaved.Should().BeTrue();
        }, TimeSpan.FromSeconds(2));

        lock (gate)
        {
            states.Should().ContainInOrder(AutoSaveState.Pending, AutoSaveState.Saving, AutoSaveState.Saved);
        }
    }

    [Test]
    public async Task Node_editor_can_flush_a_pending_autosave()
    {
        var saveCount = 0;

        var cut = Render<NodeEditor>(parameters => parameters
            .Add(component => component.AutoSave, true)
            .Add(component => component.AutoSaveDelay, 10_000)
            .Add(component => component.OnAutoSave, (CancellationToken _) =>
            {
                Interlocked.Increment(ref saveCount);
                return ValueTask.CompletedTask;
            })
            .Add(component => component.NodeTemplate, _ => _ => { }));

        await cut.Instance.NotifyChanged();
        cut.Instance.AutoSaveState.Should().Be(AutoSaveState.Pending);

        await cut.Instance.FlushAutoSave();

        Volatile.Read(ref saveCount).Should().Be(1);
        cut.Instance.AutoSaveState.Should().Be(AutoSaveState.Saved);
        cut.Instance.HasAutoSaved.Should().BeTrue();
    }
}
