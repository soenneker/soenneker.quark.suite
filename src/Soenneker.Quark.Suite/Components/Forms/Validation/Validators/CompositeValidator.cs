using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Quark.Base;

namespace Soenneker.Quark;

/// <summary>
/// A composite validator that combines multiple validators.
/// All validators must pass for the composite to pass.
/// </summary>
public class CompositeValidator : QuarkValidator
{
    private readonly List<IQuarkValidator> _validators;

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

        if (_validators.Count == 1)
            return _validators[0].Validate(value);

        int count = _validators.Count;
        ValidationBuffer<ValidationResult> local = default;
        ValidationResult[]? rented = null;
        Span<ValidationResult> results = count <= 8 ? local[..count] :
            (rented = ArrayPool<ValidationResult>.Shared.Rent(count)).AsSpan(0, count);
        try
        {
            for (var i = 0; i < count; i++)
                results[i] = _validators[i].Validate(value);
            return ValidationResult.Combine(results);
        }
        finally
        {
            if (rented is not null)
                ArrayPool<ValidationResult>.Shared.Return(rented, clearArray: true);
        }
    }

    /// <summary>
    /// Validates the given value asynchronously using all validators in the composite.
    /// All validators must pass for the validation to succeed.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="ValidationResult"/> containing the combined validation outcome.</returns>
    public override Task<ValidationResult> Validate(object value, CancellationToken cancellationToken = default)
    {
        try
        {
            if (_validators.Count == 0)
                return ValidationResult.None().AsTask();
            if (_validators.Count == 1)
                return _validators[0].Validate(value, cancellationToken);
            return StartValidators(value, cancellationToken);
        }
        catch (Exception exception)
        {
            return PropagateException(exception);
        }
    }

    // Keep synchronous exceptions in the returned task, including cancellation.
    private static async Task<ValidationResult> PropagateException(Exception exception) =>
        await Task.FromException<ValidationResult>(exception);

    private Task<ValidationResult> StartValidators(object value, CancellationToken cancellationToken)
    {
        int count = _validators.Count;
        ValidationBuffer<Task<ValidationResult>> local = default;
        Task<ValidationResult>[]? rented = null;
        Span<Task<ValidationResult>> tasks = count <= 8 ? local[..count] :
            (rented = ArrayPool<Task<ValidationResult>>.Shared.Rent(count)).AsSpan(0, count);
        try
        {
            bool completed = true;
            for (var i = 0; i < count; i++)
            {
                tasks[i] = _validators[i].Validate(value, cancellationToken);
                completed &= tasks[i].IsCompletedSuccessfully;
            }

            if (completed)
                return CombineCompleted(tasks).AsTask();

            // WhenAll takes its own snapshot before the rented buffer is returned.
            return AwaitResults(Task.WhenAll((ReadOnlySpan<Task<ValidationResult>>)tasks));
        }
        finally
        {
            if (rented is not null)
                ArrayPool<Task<ValidationResult>>.Shared.Return(rented, clearArray: true);
        }
    }

    private static ValidationResult CombineCompleted(ReadOnlySpan<Task<ValidationResult>> tasks)
    {
        ValidationBuffer<ValidationResult> local = default;
        ValidationResult[]? rented = null;
        Span<ValidationResult> results = tasks.Length <= 8 ? local[..tasks.Length] :
            (rented = ArrayPool<ValidationResult>.Shared.Rent(tasks.Length)).AsSpan(0, tasks.Length);
        try
        {
            for (int i = 0; i < tasks.Length; i++)
                results[i] = tasks[i].Result;
            return ValidationResult.Combine(results);
        }
        finally
        {
            if (rented is not null)
                ArrayPool<ValidationResult>.Shared.Return(rented, clearArray: true);
        }
    }

    private static async Task<ValidationResult> AwaitResults(Task<ValidationResult[]> pending)
    {
        var results = await pending;
        return ValidationResult.Combine(results);
    }
}
