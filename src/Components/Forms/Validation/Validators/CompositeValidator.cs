using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// A composite validator that combines multiple validators.
/// All validators must pass for the composite to pass.
/// </summary>
public class CompositeValidator : QuarkValidator
{
    private readonly List<IQuarkValidator> _validators;

    /// <summary>
    /// Initializes a new instance of the CompositeValidator class.
    /// </summary>
    /// <param name="validators">The validators to combine.</param>
    public CompositeValidator(params IQuarkValidator[] validators)
    {
        _validators = validators?.ToList() ?? [];
    }

    /// <summary>
    /// Adds a validator to the composite.
    /// </summary>
    /// <param name="validator">The validator to add.</param>
    public void AddValidator(IQuarkValidator validator)
    {
        if (validator != null)
        {
            _validators.Add(validator);
        }
    }

    /// <summary>
    /// Removes a validator from the composite.
    /// </summary>
    /// <param name="validator">The validator to remove.</param>
    public void RemoveValidator(IQuarkValidator validator)
    {
        _validators.Remove(validator);
    }

    public override ValidationResult Validate(object value)
    {
        var results = _validators.Select(validator => validator.Validate(value)).ToList();
        return ValidationResult.Combine(results.ToArray());
    }

    public override async Task<ValidationResult> ValidateAsync(object value, CancellationToken cancellationToken = default)
    {
        var tasks = _validators.Select(validator => validator.ValidateAsync(value, cancellationToken));
        var results = await Task.WhenAll(tasks);
        return ValidationResult.Combine(results);
    }
}
