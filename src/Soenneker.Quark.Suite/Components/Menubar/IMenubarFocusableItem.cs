using System.Threading.Tasks;

namespace Soenneker.Quark;

internal interface IMenubarFocusableItem
{
    bool IsDisabledForNavigation { get; }

    Task FocusAsync();
}
