using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// A composite validator that combines multiple validators.
/// </summary>
public class CompositeValidator : BaseQuarkValidator
{
    private readonly List<IQuarkValidator> _validators;

    /// <summary>
    /// Initializes a new instance of the CompositeValidator class.
    /// </summary>
    /// <param name="validators">The validators to combine.</param>
    public CompositeValidator(params IQuarkValidator[] validators)
    {
        _validators = validators?.ToList() ?? [];

        ErrorMessage = "Validation failed.";
    }

    /// <summary>
    /// Initializes a new instance of the CompositeValidator class with a custom error message.
    /// </summary>
    /// <param name="errorMessage">The error message to display when validation fails.</param>
    /// <param name="validators">The validators to combine.</param>
    public CompositeValidator(string errorMessage, params IQuarkValidator[] validators)
    {
        _validators = validators?.ToList() ?? new List<IQuarkValidator>();
        ErrorMessage = errorMessage;
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

    /// <inheritdoc/>
    protected override bool ValidateValue(object value)
    {
        return _validators.All(validator => validator.Validate(value));
    }

    /// <inheritdoc/>
    protected override async Task<bool> ValidateValueAsync(object value, CancellationToken cancellationToken = default)
    {
        var tasks = _validators.Select(validator => validator.ValidateAsync(value, cancellationToken));
        var results = await Task.WhenAll(tasks);
        return results.All(result => result);
    }

    /// <summary>
    /// Gets all validation error messages from failed validators.
    /// </summary>
    /// <param name="value">The value that was validated.</param>
    /// <returns>A collection of error messages from failed validators.</returns>
    public IEnumerable<string> GetErrorMessages(object value)
    {
        return _validators
            .Where(validator => !validator.Validate(value))
            .Select(validator => validator.ErrorMessage);
    }

    /// <summary>
    /// Gets all validation error messages from failed validators asynchronously.
    /// </summary>
    /// <param name="value">The value that was validated.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of error messages from failed validators.</returns>
    public async Task<IEnumerable<string>> GetErrorMessagesAsync(object value, CancellationToken cancellationToken = default)
    {
        var tasks = _validators.Select(async validator => new { Validator = validator, IsValid = await validator.ValidateAsync(value, cancellationToken) });
        var results = await Task.WhenAll(tasks);
        
        return results
            .Where(result => !result.IsValid)
            .Select(result => result.Validator.ErrorMessage);
    }
}
