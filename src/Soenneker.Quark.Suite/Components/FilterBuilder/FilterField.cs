using System.Collections.Generic;
namespace Soenneker.Quark;
/// <summary>A stable filter field identifier, label, value type, and optional enumerated choices.</summary>
public sealed record FilterField(string Key, string Label, FilterFieldType Type = FilterFieldType.Text, IReadOnlyList<SelectOption>? Choices = null);
