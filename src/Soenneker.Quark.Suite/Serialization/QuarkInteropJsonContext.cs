using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Soenneker.Quark;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(Dictionary<string, double>))]
[JsonSerializable(typeof(FloatingWindowOptions))]
[JsonSerializable(typeof(FloatingWindowPosition))]
[JsonSerializable(typeof(FloatingWindowSize))]
[JsonSerializable(typeof(InputSelectionSnapshot))]
[JsonSerializable(typeof(NodeEditorAddRequest))]
[JsonSerializable(typeof(NodeEditorConnectionChangeRequest))]
[JsonSerializable(typeof(NodeEditorConnectionRequest))]
[JsonSerializable(typeof(NodeEditorConnectionValidationRequest))]
[JsonSerializable(typeof(NodeEditorConnectionValidationResult))]
[JsonSerializable(typeof(NodeEditorDeleteRequest))]
[JsonSerializable(typeof(NodeEditorDropRequest))]
[JsonSerializable(typeof(NodeEditorGraphPoint))]
[JsonSerializable(typeof(NodeEditorNodePositionChangedEventArgs))]
[JsonSerializable(typeof(NodeEditorNodePositionChangedEventArgs[]))]
[JsonSerializable(typeof(NodeEditorOptions))]
[JsonSerializable(typeof(NodeEditorViewportChangedEventArgs))]
[JsonSerializable(typeof(OnThisPageInteropOptions))]
[JsonSerializable(typeof(OnThisPageTocItem[]))]
[JsonSerializable(typeof(PromptInputAttachmentInfo[]))]
[JsonSerializable(typeof(ScrollRevealInteropOptions))]
[JsonSerializable(typeof(ScrollspyInteropOptions))]
[JsonSerializable(typeof(SortableReorderEventArgs))]
[JsonSerializable(typeof(IReadOnlyList<string>))]
[JsonSerializable(typeof(TableColumnSizingOptions))]
[JsonSerializable(typeof(string[]))]
[JsonSerializable(typeof(IReadOnlyDictionary<string, string>))]
internal partial class QuarkInteropJsonContext : JsonSerializerContext;
