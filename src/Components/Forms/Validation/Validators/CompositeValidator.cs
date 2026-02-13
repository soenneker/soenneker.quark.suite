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
        _validators = validators?.Length > 0 ? [..validators] : [];
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

    /// <summary>
    /// Validates the given value using all validators in the composite.
    /// All validators must pass for the validation to succeed.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the combined validation outcome.</returns>
    public override ValidationResult Validate(object value)
    {
        if (_validators.Count == 0)
            return ValidationResult.None();

        var results = new ValidationResult[_validators.Count];
        for (var i = 0; i < _validators.Count; i++)
        {
            results[i] = _validators[i].Validate(value);
        }

        return ValidationResult.Combine(results);
    }

    /// <summary>
    /// Validates the given value asynchronously using all validators in the composite.
    /// All validators must pass for the validation to succeed.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the combined validation outcome.</returns>
    public override async Task<ValidationResult> ValidateAsync(object value, CancellationToken cancellationToken = default)
    {
        IEnumerable<Task<ValidationResult>> tasks = _validators.Select(validator => validator.ValidateAsync(value, cancellationToken));
        ValidationResult[] results = await Task.WhenAll(tasks);
        return ValidationResult.Combine(results);
    }
}
