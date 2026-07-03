namespace Soenneker.Quark;

internal sealed record StepperItemContext(int Step, string Value, string State, bool Disabled, bool Loading, string Orientation, StepperItemVariant Variant, bool Responsive)
{
    public bool IsStacked => ReferenceEquals(Variant, StepperItemVariant.Stacked);
}
