using System.Collections.Generic;

namespace Soenneker.Quark;

internal sealed record ConsoleJsonContent(string Summary, List<string> Payloads);
