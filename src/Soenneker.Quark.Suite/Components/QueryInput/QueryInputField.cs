using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>A query qualifier and its optional suggested values. Keys must be unique and contain no whitespace, colon, quotes, or leading minus.</summary>
public sealed record QueryInputField(string Key, string Label, IReadOnlyList<SelectOption>? Values = null);
