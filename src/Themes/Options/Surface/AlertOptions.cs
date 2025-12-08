
namespace Soenneker.Quark;

public sealed class AlertOptions : ComponentOptions
{
    public AlertOptions()
    {
        Selector = ".alert, .alert.alert-primary, .alert.alert-success, .alert.alert-danger, .alert.alert-warning, .alert.alert-info, .alert.alert-light, .alert.alert-dark, .alert-dismissible";
    }
}
