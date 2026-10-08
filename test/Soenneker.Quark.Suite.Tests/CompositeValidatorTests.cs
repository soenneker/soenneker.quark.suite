using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class CompositeValidatorTests
{
    [Test]
    public async Task Composite_reuses_completed_success_tasks_and_keeps_synchronous_failures_in_the_task(CancellationToken cancellationToken)
    {
        var composite = new CompositeValidator(new DelegateValidationProbe(ValidationResult.Success),
            new DelegateValidationProbe(ValidationResult.Success));
        Task<ValidationResult> first = composite.Validate("", cancellationToken);
        composite.Validate("", cancellationToken).Should().BeSameAs(first);
        var throwing = new CompositeValidator(new DelegateValidationProbe(() => throw new InvalidOperationException("sync")));
        Task<ValidationResult> failed = throwing.Validate("", cancellationToken);
        Func<Task> action = async () => await failed;
        await action.Should().ThrowAsync<InvalidOperationException>().WithMessage("sync");
    }

    [Test]
    public async Task Composite_preserves_error_order_and_member_deduplication_on_both_buffer_paths(CancellationToken cancellationToken)
    {
        foreach (int count in new[] { 2, 8, 9, 32 })
        {
            var validators = Enumerable.Range(0, count).Select(i => (IQuarkValidator)new DelegateValidationProbe(() =>
                ValidationResult.Error(i.ToString(), ["shared", $"field{i}"]))).ToArray();
            var composite = new CompositeValidator(validators);
            foreach (ValidationResult result in new[] { composite.Validate(""), await composite.Validate("", cancellationToken) })
            {
                result.ErrorText.Should().Be(string.Join(" ", Enumerable.Range(0, count)));
                result.MemberNames.Should().Equal(new[] { "shared" }.Concat(Enumerable.Range(0, count).Select(i => $"field{i}")));
            }
        }
    }

    [Test]
    public async Task Composite_starts_all_async_validators_and_waits_for_all_even_after_a_failure(CancellationToken cancellationToken)
    {
        var first = new TaskCompletionSource<ValidationResult>();
        var second = new TaskCompletionSource<ValidationResult>();
        int started = 0;
        var composite = new CompositeValidator(
            new DelegateValidationProbe(ValidationResult.Success, () => { started++; return first.Task; }),
            new DelegateValidationProbe(ValidationResult.Success, () => { started++; return second.Task; }));
        Task<ValidationResult> pending = composite.Validate("", cancellationToken);
        started.Should().Be(2);
        first.SetException(new InvalidOperationException("first"));
        pending.IsCompleted.Should().BeFalse();
        second.SetResult(ValidationResult.Success());
        Func<Task> action = async () => await pending;
        await action.Should().ThrowAsync<InvalidOperationException>().WithMessage("first");
    }

    [Test]
    public async Task Composite_preserves_cancellation_and_evaluates_members_after_all_validators(CancellationToken cancellationToken)
    {
        bool secondRan = false;
        IEnumerable<string> Members()
        {
            secondRan.Should().BeTrue();
            yield return "field";
        }
        var composite = new CompositeValidator(
            new DelegateValidationProbe(() => ValidationResult.Error("first", Members())),
            new DelegateValidationProbe(() => { secondRan = true; return ValidationResult.Success(); }));
        composite.Validate("").MemberNames.Should().Equal("field");
        var canceled = new CompositeValidator(
            new DelegateValidationProbe(ValidationResult.Success),
            new DelegateValidationProbe(ValidationResult.Success, () => Task.FromCanceled<ValidationResult>(new CancellationToken(true))));
        Task<ValidationResult> pending = canceled.Validate("", cancellationToken);
        Func<Task> action = async () => await pending;
        await action.Should().ThrowAsync<OperationCanceledException>();
        pending.IsCanceled.Should().BeTrue();
    }
}
