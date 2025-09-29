using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

public static class UserSelect
{
    public static UserSelectBuilder None => new(UserSelectKeyword.NoneValue);
    public static UserSelectBuilder Auto => new(UserSelectKeyword.AutoValue);
    public static UserSelectBuilder All => new(UserSelectKeyword.AllValue);

    public static UserSelectBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static UserSelectBuilder Initial => new(GlobalKeyword.InitialValue);
    public static UserSelectBuilder Revert => new(GlobalKeyword.RevertValue);
    public static UserSelectBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static UserSelectBuilder Unset => new(GlobalKeyword.UnsetValue);
}
