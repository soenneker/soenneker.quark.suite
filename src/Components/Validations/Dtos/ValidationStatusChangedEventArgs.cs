using System.Collections.Generic;

namespace Soenneker.Quark;

public sealed record ValidationStatusChangedEventArgs(ValidationStatus Status, IEnumerable<string>? Messages);
