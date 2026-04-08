using System.Threading.Tasks;

namespace Soenneker.Quark;

internal interface IDropdownFocusableItem
{
    bool IsDisabledForNavigation { get; }

    Task FocusAsync();
}
