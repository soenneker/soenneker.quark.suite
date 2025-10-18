using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Interface for the Image component
/// </summary>
public interface IImage : IComponent
{
    string? Source { get; set; }
    string? Alt { get; set; }
    bool Fluid { get; set; }
    bool Lazy { get; set; }
    string? Loading { get; set; }
    string? Decoding { get; set; }
    string? FetchPriority { get; set; }
    string? Sizes { get; set; }
    string? SrcSet { get; set; }
    string? CrossOrigin { get; set; }
    string? ReferrerPolicy { get; set; }
    string? UseMap { get; set; }
    bool IsMap { get; set; }
    string? LongDesc { get; set; }
    EventCallback<ProgressEventArgs> OnLoad { get; set; }
    EventCallback<ErrorEventArgs> OnError { get; set; }
    EventCallback<ProgressEventArgs> OnLoadStart { get; set; }
    EventCallback<ProgressEventArgs> OnAbort { get; set; }
}

