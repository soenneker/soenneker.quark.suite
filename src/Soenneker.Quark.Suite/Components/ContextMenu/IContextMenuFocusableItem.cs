using System.Threading.Tasks;

namespace Soenneker.Quark;

internal interface IContextMenuFocusableItem
{
    bool IsDisabledForNavigation { get; }

    Task Focus();
}
