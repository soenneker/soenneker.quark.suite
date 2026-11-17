using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
namespace Soenneker.Quark;

/// <summary>Recognizes intentional directional swipes without owning navigation or drawer state.
/// Keep ordinary buttons available as accessible alternatives. Gestures starting on interactive controls are ignored.</summary>
public interface ISwipeArea : IElement
{
    /// <summary>Raised after a completed left swipe.</summary>
    EventCallback OnSwipeLeft { get; set; }
    /// <summary>Raised after a completed right swipe.</summary>
    EventCallback OnSwipeRight { get; set; }
    /// <summary>Raised after a completed upward swipe. Registering this consumes intentional upward gestures.</summary>
    EventCallback OnSwipeUp { get; set; }
    /// <summary>Raised after a completed downward swipe. Registering this consumes intentional downward gestures.</summary>
    EventCallback OnSwipeDown { get; set; }
    /// <summary>Disables gesture recognition.</summary>
    bool Disabled { get; set; }
    /// <summary>Required travel in CSS pixels; defaults to 60 and is clamped to at least 10.</summary>
    double Threshold { get; set; }
    /// <summary>Maximum gesture duration in milliseconds; defaults to 1000.</summary>
    int MaxDuration { get; set; }
    /// <summary>Optional starting edge relative to this component; defaults to None.</summary>
    SwipeEdge StartEdge { get; set; }
    /// <summary>Width of the starting edge region in CSS pixels; defaults to 32.</summary>
    double EdgeSize { get; set; }
    /// <summary>Ignores starts within 24 CSS pixels of the viewport's left or right edge, leaving native navigation priority.
    /// Defaults to true. Native gesture interception is platform dependent.</summary>
    bool PreserveNativeEdgeGestures { get; set; }
    /// <summary>Also recognizes primary mouse drags. Defaults to false.</summary>
    bool EnableMouse { get; set; }
    /// <summary>Dispatches a browser-recognized direction to its callback.</summary>
    Task NotifySwipe(string direction);
}
