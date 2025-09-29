using System;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Soenneker.Quark.Components.Validations;

namespace Soenneker.Quark;

/// <summary>
/// Base class for validation result messages.
/// </summary>
public abstract class ValidationResultComponent : ComponentBase, IDisposable
{
    private Validation? _previousParentValidation;

    /// <summary>
    /// Gets or sets the reference to the parent validation.
    /// </summary>
    [CascadingParameter]
    protected Validation? ParentValidation { get; set; }

    /// <summary>
    /// Specifies the content to be rendered inside this validation result.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (ParentValidation != _previousParentValidation)
        {
            DetachValidationStatusChangedListener();
            if (ParentValidation != null)
            {
                ParentValidation.ValidationStatusChanged += OnValidationStatusChanged;
            }

            _previousParentValidation = ParentValidation;
        }
    }

    private void DetachValidationStatusChangedListener()
    {
        if (_previousParentValidation != null)
        {
            _previousParentValidation.ValidationStatusChanged -= OnValidationStatusChanged;
        }
    }

    /// <summary>
    /// Handles validation status changes.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event arguments.</param>
    protected virtual async void OnValidationStatusChanged(object? sender, ValidationStatusChangedEventArgs e)
    {
        await InvokeAsync(StateHasChanged);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        DetachValidationStatusChangedListener();
        GC.SuppressFinalize(this);
    }
}
