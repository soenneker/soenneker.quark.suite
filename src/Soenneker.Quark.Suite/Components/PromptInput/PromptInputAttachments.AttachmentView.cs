using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public partial class PromptInputAttachments
{
    private sealed class AttachmentView : IHandleEvent
    {
        private readonly PromptInputAttachments _owner;
        private CultureInfo? _culture;
        private string? _description;
        internal PromptInputAttachmentInfo Attachment { get; }
        internal string RemoveLabel { get; }
        internal Func<Task> Remove { get; }
        internal string Description
        {
            get
            {
                var culture = CultureInfo.CurrentCulture;
                if (_description is null || !culture.IsReadOnly || !ReferenceEquals(_culture, culture))
                {
                    _description = Attachment.Description;
                    _culture = culture;
                }
                return _description;
            }
        }

        internal AttachmentView(PromptInputAttachments owner, PromptInputAttachmentInfo attachment)
        {
            _owner = owner;
            Attachment = attachment;
            RemoveLabel = $"Remove {attachment.Name}";
            Remove = RemoveAttachment;
        }

        private Task RemoveAttachment() => _owner.PromptInput!.RemoveAttachment(Attachment.Id);

        public Task HandleEventAsync(EventCallbackWorkItem callback, object? argument) =>
            ((IHandleEvent)_owner).HandleEventAsync(callback, argument);
    }
}
