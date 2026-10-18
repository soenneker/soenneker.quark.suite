using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Soenneker.Quark;
[JsonSerializable(typeof(List<QuarkSavedView>))]
[JsonSerializable(typeof(QuarkViewState))]
public partial class QuarkViewJsonContext : JsonSerializerContext { }
