using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ValidationLifetimeTests : BunitContext
{
    public ValidationLifetimeTests() => Services.AddSingleton<IValidationInterop, FakeInterop>();

    [Test]
    public async Task Data_annotations_replace_messages_and_clear_them_on_disposal()
    {
        var model = new Model();
        var context = new EditContext(model);
        var cut = Render<Validation>(p => p.AddCascadingValue(nameof(EditContext), context));
        await cut.InvokeAsync(() => cut.Instance.InitializeInput(new Input()));
        await cut.InvokeAsync(() => cut.Instance.InitializeInputExpression(() => model.Name));
        await cut.InvokeAsync(() => cut.Instance.ValidateValue(""));
        await cut.InvokeAsync(() => cut.Instance.ValidateValue(""));
        context.GetValidationMessages().Should().ContainSingle();

        model.Name = "valid";
        await cut.InvokeAsync(() => cut.Instance.ValidateValue(model.Name));
        context.GetValidationMessages().Should().BeEmpty();

        model.Name = "";
        await cut.InvokeAsync(() => cut.Instance.ValidateValue(model.Name));
        context.GetValidationMessages().Should().ContainSingle();
        await cut.InvokeAsync(cut.Instance.Clear);
        context.GetValidationMessages().Should().BeEmpty();
        await cut.InvokeAsync(() => cut.Instance.ValidateValue(model.Name));
        await cut.InvokeAsync(cut.Instance.Dispose);
        context.GetValidationMessages().Should().BeEmpty();
    }

    [Test]
    public async Task Superseded_validation_cannot_publish_an_old_error()
    {
        var pending = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var cut = Render<Validation>(p => p.Add(c => c.AsyncFunc, async (args, _) =>
        {
            if (Equals(args.Value, "old"))
            {
                await pending.Task;
                args.Status = ValidationStatus.Error;
                args.ErrorText = "Old error";
            }
            else
                args.Status = ValidationStatus.Success;
        }));
        await cut.InvokeAsync(() => cut.Instance.InitializeInput(new Input()));
        Task<ValidationStatus>? oldValidation = null;
        await cut.InvokeAsync(() => { oldValidation = cut.Instance.ValidateValue("old"); });
        await cut.InvokeAsync(() => cut.Instance.ValidateValue("new"));
        pending.SetResult();
        await oldValidation!;
        cut.Instance.Status.Should().Be(ValidationStatus.Success);
        cut.Instance.Messages.Should().BeNull();
    }

    [Test]
    public async Task Disposal_cancels_pending_validation_and_prevents_status_updates()
    {
        var pending = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        CancellationToken observed = default;
        var cut = Render<Validation>(p => p.Add(c => c.AsyncFunc, async (args, token) =>
        {
            observed = token;
            await pending.Task;
            args.Status = ValidationStatus.Error;
        }));
        await cut.InvokeAsync(() => cut.Instance.InitializeInput(new Input()));
        Task<ValidationStatus>? validation = null;
        await cut.InvokeAsync(() => { validation = cut.Instance.ValidateValue("pending"); });
        await cut.InvokeAsync(cut.Instance.Dispose);
        observed.IsCancellationRequested.Should().BeTrue();
        pending.SetResult();
        await validation!;
        cut.Instance.Status.Should().Be(ValidationStatus.None);
    }

    private sealed class Model
    {
        [Required]
        public string Name { get; set; } = "";
    }

    private sealed class Input : IValidationInput
    {
        public object ValidationValue => "";
        public bool Disabled => false;
    }

    private sealed class FakeInterop : IValidationInterop
    {
        public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
