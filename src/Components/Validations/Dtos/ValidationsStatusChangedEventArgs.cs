using System.Collections.Generic;

namespace Soenneker.Quark;

public sealed record ValidationsStatusChangedEventArgs(ValidationStatus Status, IReadOnlyCollection<string>? Messages, IValidation? Validation);
