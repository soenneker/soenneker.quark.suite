using System.Text.Json;
using System.Text.Json.Serialization;

namespace Soenneker.Quark;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(FloatingWindowOptions))]
[JsonSerializable(typeof(NodeEditorOptions))]
internal partial class QuarkInteropJsonContext : JsonSerializerContext;
