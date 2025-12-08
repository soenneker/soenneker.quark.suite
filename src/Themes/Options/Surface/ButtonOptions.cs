namespace Soenneker.Quark;

public sealed class ButtonOptions : ComponentOptions
{
    public ButtonOptions()
    {
        Selector = ".btn, .btn.btn-primary, .btn.btn-secondary, .btn:hover, .btn:focus, .btn.active";
    }
}
