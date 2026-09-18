using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Demo.Interops.Abstract;

public interface IDemoPageInterop
{
    /// <summary>Scrolls the demo page to the top after navigation.</summary>
    ValueTask ScrollToTop();
}
