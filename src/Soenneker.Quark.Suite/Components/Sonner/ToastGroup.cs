using System.Collections.Generic;

namespace Soenneker.Quark;

internal sealed record ToastGroup(SonnerPosition Position, IReadOnlyList<SonnerToast> Toasts);
