using System.Threading.Tasks;

namespace Soenneker.Quark;

internal interface ISelectFocusableItem
{
    bool IsDisabledForNavigation { get; }

    string? Value { get; }

    string ItemId { get; }

    Task FocusAsync();

    void SetActive(bool active);
}
