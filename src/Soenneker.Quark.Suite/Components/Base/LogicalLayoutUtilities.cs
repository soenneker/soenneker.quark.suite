namespace Soenneker.Quark;

internal sealed class LogicalLayoutUtilities
{
    public CssValue<BlockSizeBuilder>? BlockSize;
    public CssValue<InlineSizeBuilder>? InlineSize;
    public CssValue<MaxBlockSizeBuilder>? MaxBlockSize;
    public CssValue<MaxInlineSizeBuilder>? MaxInlineSize;
    public CssValue<MinBlockSizeBuilder>? MinBlockSize;
    public CssValue<MinInlineSizeBuilder>? MinInlineSize;
    public CssValue<InsetBlockEndBuilder>? InsetBlockEnd;
    public CssValue<InsetBlockStartBuilder>? InsetBlockStart;
    public CssValue<InsetEndBuilder>? InsetEnd;
    public CssValue<InsetStartBuilder>? InsetStart;
}
