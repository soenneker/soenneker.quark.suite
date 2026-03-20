using System;

namespace Soenneker.Quark;

public sealed class SonnerPromiseOptions
{
    public string Loading { get; set; } = "Loading...";

    public string Success { get; set; } = "Done";

    public string Error { get; set; } = "Something went wrong";

    public string? Description { get; set; }

    public string? SuccessDescription { get; set; }

    public string? ErrorDescription { get; set; }

    public Action<SonnerToastOptions>? ConfigureLoading { get; set; }

    public Action<SonnerToastOptions>? ConfigureSuccess { get; set; }

    public Action<SonnerToastOptions>? ConfigureError { get; set; }
}
