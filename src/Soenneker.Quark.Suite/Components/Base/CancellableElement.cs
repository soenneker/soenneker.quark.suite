using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <inheritdoc cref="ICancellableElement"/>
public abstract class CancellableElement : CancellableComponent, ICancellableElement
{
    [Parameter]
    public int? TabIndex { get; set; }

    [Parameter]
    public string? Role { get; set; }

    [Parameter]
    public string? AriaLabel { get; set; }

    [Parameter]
    public string? AriaLabelledBy { get; set; }

    [Parameter]
    public string? AriaDescribedBy { get; set; }

    [Parameter]
    public string? AriaCurrent { get; set; }

    [Parameter]
    public bool? AriaHidden { get; set; }

    [Parameter]
    public string? AccessKey { get; set; }

    [Parameter]
    public string? Lang { get; set; }

    [Parameter]
    public string? Dir { get; set; }

    [Parameter]
    public string? ContentEditable { get; set; }

    [Parameter]
    public bool? SpellCheck { get; set; }

    [Parameter]
    public string? AutoCapitalize { get; set; }

    [Parameter]
    public bool? AutoCorrect { get; set; }

    [Parameter]
    public string? EnterKeyHint { get; set; }

    [Parameter]
    public string? HtmlInputMode { get; set; }

    [Parameter]
    public bool? HtmlDraggable { get; set; }

    [Parameter]
    public bool? TranslateContent { get; set; }

    [Parameter]
    public bool? Inert { get; set; }

    [Parameter]
    public bool AutoFocus { get; set; }

    [Parameter]
    public string? HtmlSlot { get; set; }

    [Parameter]
    public string? Part { get; set; }

    [Parameter]
    public string? ExportParts { get; set; }

    [Parameter]
    public string? Popover { get; set; }

    [Parameter]
    public string? HtmlItemId { get; set; }

    [Parameter]
    public string? ItemProp { get; set; }

    [Parameter]
    public string? ItemRef { get; set; }

    [Parameter]
    public bool? ItemScope { get; set; }

    [Parameter]
    public string? ItemType { get; set; }

    [Parameter]
    public string? AriaLive { get; set; }

    [Parameter]
    public bool? AriaAtomic { get; set; }

    [Parameter]
    public bool? AriaBusy { get; set; }

    [Parameter]
    public string? AriaRelevant { get; set; }

    [Parameter]
    public string? AriaControls { get; set; }

    [Parameter]
    public string? AriaDetails { get; set; }

    [Parameter]
    public string? AriaErrorMessage { get; set; }

    [Parameter]
    public string? AriaFlowTo { get; set; }

    [Parameter]
    public string? AriaKeyShortcuts { get; set; }

    [Parameter]
    public string? AriaOwns { get; set; }

    [Parameter]
    public string? AriaRoleDescription { get; set; }

    [Parameter]
    public string? AriaDescription { get; set; }

    [Parameter]
    public string? AriaBrailleLabel { get; set; }

    [Parameter]
    public string? AriaBrailleRoleDescription { get; set; }

    private RenderFragment? _lastChildContentRef;
    private bool _childContentChanged;

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);
        BuildGlobalHtmlAttributes(attrs);

        if (TabIndex.HasValue)
            attrs["tabindex"] = QuarkAttributeValues.FromInt32(TabIndex.Value);

        if (Role is not null)
            attrs["role"] = Role;

        if (AriaLabel is not null)
            attrs["aria-label"] = AriaLabel;

        if (AriaLabelledBy is not null)
            attrs["aria-labelledby"] = AriaLabelledBy;

        if (AriaDescribedBy is not null)
            attrs["aria-describedby"] = AriaDescribedBy;

        if (AriaCurrent is not null)
            attrs["aria-current"] = AriaCurrent;

        if (AriaHidden.HasValue)
            attrs["aria-hidden"] = AriaHidden.Value ? "true" : "false";
    }

    protected void BuildGlobalHtmlAttributes(Dictionary<string, object> attributes)
    {
        if (AccessKey is not null)
            attributes["accesskey"] = AccessKey;

        if (Lang is not null)
            attributes["lang"] = Lang;

        if (Dir is not null)
            attributes["dir"] = Dir;

        if (ContentEditable is not null)
            attributes["contenteditable"] = ContentEditable;

        if (SpellCheck.HasValue)
            attributes["spellcheck"] = SpellCheck.Value ? "true" : "false";

        if (AutoCapitalize is not null)
            attributes["autocapitalize"] = AutoCapitalize;

        if (AutoCorrect.HasValue)
            attributes["autocorrect"] = AutoCorrect.Value ? "on" : "off";

        if (EnterKeyHint is not null)
            attributes["enterkeyhint"] = EnterKeyHint;

        if (HtmlInputMode is not null)
            attributes["inputmode"] = HtmlInputMode;

        if (HtmlDraggable.HasValue)
            attributes["draggable"] = HtmlDraggable.Value ? "true" : "false";

        if (TranslateContent.HasValue)
            attributes["translate"] = TranslateContent.Value ? "yes" : "no";

        if (Inert.HasValue)
            attributes["inert"] = Inert.Value ? QuarkAttributeValues.True : false;

        if (AutoFocus)
            attributes["autofocus"] = QuarkAttributeValues.True;

        if (HtmlSlot is not null)
            attributes["slot"] = HtmlSlot;

        if (Part is not null)
            attributes["part"] = Part;

        if (ExportParts is not null)
            attributes["exportparts"] = ExportParts;

        if (Popover is not null)
            attributes["popover"] = Popover;

        if (HtmlItemId is not null)
            attributes["itemid"] = HtmlItemId;

        if (ItemProp is not null)
            attributes["itemprop"] = ItemProp;

        if (ItemRef is not null)
            attributes["itemref"] = ItemRef;

        if (ItemScope.HasValue)
            attributes["itemscope"] = ItemScope.Value ? QuarkAttributeValues.True : false;

        if (ItemType is not null)
            attributes["itemtype"] = ItemType;

        if (AriaLive is not null)
            attributes["aria-live"] = AriaLive;

        if (AriaAtomic.HasValue)
            attributes["aria-atomic"] = AriaAtomic.Value ? "true" : "false";

        if (AriaBusy.HasValue)
            attributes["aria-busy"] = AriaBusy.Value ? "true" : "false";

        if (AriaRelevant is not null)
            attributes["aria-relevant"] = AriaRelevant;

        if (AriaControls is not null)
            attributes["aria-controls"] = AriaControls;

        if (AriaDetails is not null)
            attributes["aria-details"] = AriaDetails;

        if (AriaErrorMessage is not null)
            attributes["aria-errormessage"] = AriaErrorMessage;

        if (AriaFlowTo is not null)
            attributes["aria-flowto"] = AriaFlowTo;

        if (AriaKeyShortcuts is not null)
            attributes["aria-keyshortcuts"] = AriaKeyShortcuts;

        if (AriaOwns is not null)
            attributes["aria-owns"] = AriaOwns;

        if (AriaRoleDescription is not null)
            attributes["aria-roledescription"] = AriaRoleDescription;

        if (AriaDescription is not null)
            attributes["aria-description"] = AriaDescription;

        if (AriaBrailleLabel is not null)
            attributes["aria-braillelabel"] = AriaBrailleLabel;

        if (AriaBrailleRoleDescription is not null)
            attributes["aria-brailleroledescription"] = AriaBrailleRoleDescription;

    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        hc.Add(AccessKey);
        hc.Add(Lang);
        hc.Add(Dir);
        hc.Add(ContentEditable);
        hc.Add(SpellCheck);
        hc.Add(AutoCapitalize);
        hc.Add(AutoCorrect);
        hc.Add(EnterKeyHint);
        hc.Add(HtmlInputMode);
        hc.Add(HtmlDraggable);
        hc.Add(TranslateContent);
        hc.Add(Inert);
        hc.Add(AutoFocus);
        hc.Add(HtmlSlot);
        hc.Add(Part);
        hc.Add(ExportParts);
        hc.Add(Popover);
        hc.Add(HtmlItemId);
        hc.Add(ItemProp);
        hc.Add(ItemRef);
        hc.Add(ItemScope);
        hc.Add(ItemType);
        hc.Add(AriaLive);
        hc.Add(AriaAtomic);
        hc.Add(AriaBusy);
        hc.Add(AriaRelevant);
        hc.Add(AriaControls);
        hc.Add(AriaDetails);
        hc.Add(AriaErrorMessage);
        hc.Add(AriaFlowTo);
        hc.Add(AriaKeyShortcuts);
        hc.Add(AriaOwns);
        hc.Add(AriaRoleDescription);
        hc.Add(AriaDescription);
        hc.Add(AriaBrailleLabel);
        hc.Add(AriaBrailleRoleDescription);


        hc.Add(TabIndex);
        hc.Add(Role);
        hc.Add(AriaLabel);
        hc.Add(AriaLabelledBy);
        hc.Add(AriaDescribedBy);
        hc.Add(AriaCurrent);
        hc.Add(AriaHidden);
    }

    protected override void OnParametersSet()
    {
        var cc = ChildContent;
        _childContentChanged = !ReferenceEquals(cc, _lastChildContentRef);
        _lastChildContentRef = cc;

        base.OnParametersSet();
    }

    protected override bool ShouldRender()
    {
        if (_childContentChanged)
        {
            _childContentChanged = false;
            return true;
        }

        return base.ShouldRender();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (_childContentChanged)
        {
            _lastChildContentRef = ChildContent;
            _childContentChanged = false;
        }

        return base.OnAfterRenderAsync(firstRender);
    }
}
