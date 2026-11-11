using System;
using Bunit;
using System.Threading.Tasks;
using System.Threading;
using AwesomeAssertions;
using Microsoft.AspNetCore.Components.Forms;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class ValidationLifetimeTests
{
    [Test]
    public async Task Framework_submit_uses_the_generated_async_annotation_contract()
    {
        var model = new AsyncAnnotationModel();
        var context = new EditContext(model);
        var cut = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), context));
        await cut.InvokeAsync(() => cut.Instance.InitializeInput(new Input()));
        await cut.InvokeAsync(() => cut.Instance.InitializeInputField(new FieldIdentifier(model, nameof(model.Name))));
        (await cut.InvokeAsync(() => context.ValidateAsync())).Should().BeFalse();
        context.GetValidationMessages().Should().Contain("Remote annotation rejected value");
        cut.Instance.Status.Should().Be(ValidationStatus.Error);
    }

    [Test]
    public async Task Framework_submit_awaits_custom_validation_and_receives_errors()
    {
        var model = new Model();
        var context = new EditContext(model);
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var release = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var cut = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), context)
            .Add(c => c.AsyncFunc, async (args, token) =>
            {
                started.TrySetResult();
                await release.Task.WaitAsync(token);
                args.Status = ValidationStatus.Error;
                args.ErrorText = "Already registered";
            }));
        await cut.InvokeAsync(() => cut.Instance.InitializeInput(new Input()));
        await cut.InvokeAsync(() => cut.Instance.InitializeInputField(new FieldIdentifier(model, nameof(model.Name))));
        var validation = cut.InvokeAsync(() => context.ValidateAsync());
        await started.Task.WaitAsync(TimeSpan.FromSeconds(10));
        context.IsValidationPending().Should().BeTrue();
        release.SetResult();
        (await validation).Should().BeFalse();
        context.GetValidationMessages().Should().Contain("Already registered");
        await cut.InvokeAsync(cut.Instance.Dispose);
        context.GetValidationMessages().Should().BeEmpty();
        (await context.ValidateAsync()).Should().BeTrue();
    }

    [Test]
    public async Task Framework_tracks_pending_fields_and_cancels_superseded_validation()
    {
        var model = new Model();
        var context = new EditContext(model);
        var field = new FieldIdentifier(model, nameof(model.Name));
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var cancelled = false;
        var cut = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), context)
            .Add(c => c.AsyncFunc, async (args, token) =>
            {
                if ((string)args.Value! == "first")
                {
                    started.TrySetResult();
                    try { await Task.Delay(Timeout.InfiniteTimeSpan, token); }
                    catch (OperationCanceledException) { cancelled = true; throw; }
                }
                args.Status = ValidationStatus.Success;
            }));
        await cut.InvokeAsync(() => cut.Instance.InitializeInput(new Input()));
        await cut.InvokeAsync(() => cut.Instance.InitializeInputField(field));
        var first = cut.InvokeAsync(() => cut.Instance.ValidateValue("first"));
        await started.Task.WaitAsync(TimeSpan.FromSeconds(10));
        context.IsValidationPending(field).Should().BeTrue();
        await cut.InvokeAsync(() => cut.Instance.ValidateValue("second"));
        await first;
        cancelled.Should().BeTrue();
        context.IsValidationPending(field).Should().BeFalse();
        cut.Instance.Status.Should().Be(ValidationStatus.Success);
    }
}
