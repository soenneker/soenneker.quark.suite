using Soenneker.Atomics.ValueBools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

#pragma warning disable BL0007 // Lazy storage only; setters preserve parameter values without triggering rendering.
public abstract class Element : Component, IElement
{
    private EditingHtmlAttributes? _editingHtmlAttributes;
    private AdvancedHtmlAttributes? _advancedHtmlAttributes;
    private MicrodataAttributes? _microdataAttributes;
    private ExtendedAriaAttributes? _extendedAriaAttributes;

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
    public string? AccessKey
    {
        get => _advancedHtmlAttributes?.AccessKey;
        set
        {
            if (value is not null) (_advancedHtmlAttributes ??= new AdvancedHtmlAttributes()).AccessKey = value;
            else if (_advancedHtmlAttributes is not null) _advancedHtmlAttributes.AccessKey = null;
        }
    }

    [Parameter]
    public string? Lang { get; set; }

    [Parameter]
    public string? Dir { get; set; }

    [Parameter]
    public string? ContentEditable
    {
        get => _editingHtmlAttributes?.ContentEditable;
        set
        {
            if (value is not null) (_editingHtmlAttributes ??= new EditingHtmlAttributes()).ContentEditable = value;
            else if (_editingHtmlAttributes is not null) _editingHtmlAttributes.ContentEditable = null;
        }
    }

    [Parameter]
    public bool? SpellCheck
    {
        get => _editingHtmlAttributes?.SpellCheck;
        set
        {
            if (value is not null) (_editingHtmlAttributes ??= new EditingHtmlAttributes()).SpellCheck = value;
            else if (_editingHtmlAttributes is not null) _editingHtmlAttributes.SpellCheck = null;
        }
    }

    [Parameter]
    public string? AutoCapitalize
    {
        get => _editingHtmlAttributes?.AutoCapitalize;
        set
        {
            if (value is not null) (_editingHtmlAttributes ??= new EditingHtmlAttributes()).AutoCapitalize = value;
            else if (_editingHtmlAttributes is not null) _editingHtmlAttributes.AutoCapitalize = null;
        }
    }

    [Parameter]
    public bool? AutoCorrect
    {
        get => _editingHtmlAttributes?.AutoCorrect;
        set
        {
            if (value is not null) (_editingHtmlAttributes ??= new EditingHtmlAttributes()).AutoCorrect = value;
            else if (_editingHtmlAttributes is not null) _editingHtmlAttributes.AutoCorrect = null;
        }
    }

    [Parameter]
    public string? EnterKeyHint
    {
        get => _editingHtmlAttributes?.EnterKeyHint;
        set
        {
            if (value is not null) (_editingHtmlAttributes ??= new EditingHtmlAttributes()).EnterKeyHint = value;
            else if (_editingHtmlAttributes is not null) _editingHtmlAttributes.EnterKeyHint = null;
        }
    }

    [Parameter]
    public string? HtmlInputMode
    {
        get => _editingHtmlAttributes?.HtmlInputMode;
        set
        {
            if (value is not null) (_editingHtmlAttributes ??= new EditingHtmlAttributes()).HtmlInputMode = value;
            else if (_editingHtmlAttributes is not null) _editingHtmlAttributes.HtmlInputMode = null;
        }
    }

    [Parameter]
    public bool? HtmlDraggable
    {
        get => _editingHtmlAttributes?.HtmlDraggable;
        set
        {
            if (value is not null) (_editingHtmlAttributes ??= new EditingHtmlAttributes()).HtmlDraggable = value;
            else if (_editingHtmlAttributes is not null) _editingHtmlAttributes.HtmlDraggable = null;
        }
    }

    [Parameter]
    public bool? TranslateContent
    {
        get => _editingHtmlAttributes?.TranslateContent;
        set
        {
            if (value is not null) (_editingHtmlAttributes ??= new EditingHtmlAttributes()).TranslateContent = value;
            else if (_editingHtmlAttributes is not null) _editingHtmlAttributes.TranslateContent = null;
        }
    }

    [Parameter]
    public bool? Inert
    {
        get => _advancedHtmlAttributes?.Inert;
        set
        {
            if (value is not null) (_advancedHtmlAttributes ??= new AdvancedHtmlAttributes()).Inert = value;
            else if (_advancedHtmlAttributes is not null) _advancedHtmlAttributes.Inert = null;
        }
    }

    [Parameter]
    public bool AutoFocus { get; set; }

    [Parameter]
    public string? HtmlSlot
    {
        get => _advancedHtmlAttributes?.HtmlSlot;
        set
        {
            if (value is not null) (_advancedHtmlAttributes ??= new AdvancedHtmlAttributes()).HtmlSlot = value;
            else if (_advancedHtmlAttributes is not null) _advancedHtmlAttributes.HtmlSlot = null;
        }
    }

    [Parameter]
    public string? Part
    {
        get => _advancedHtmlAttributes?.Part;
        set
        {
            if (value is not null) (_advancedHtmlAttributes ??= new AdvancedHtmlAttributes()).Part = value;
            else if (_advancedHtmlAttributes is not null) _advancedHtmlAttributes.Part = null;
        }
    }

    [Parameter]
    public string? ExportParts
    {
        get => _advancedHtmlAttributes?.ExportParts;
        set
        {
            if (value is not null) (_advancedHtmlAttributes ??= new AdvancedHtmlAttributes()).ExportParts = value;
            else if (_advancedHtmlAttributes is not null) _advancedHtmlAttributes.ExportParts = null;
        }
    }

    [Parameter]
    public string? Popover
    {
        get => _advancedHtmlAttributes?.Popover;
        set
        {
            if (value is not null) (_advancedHtmlAttributes ??= new AdvancedHtmlAttributes()).Popover = value;
            else if (_advancedHtmlAttributes is not null) _advancedHtmlAttributes.Popover = null;
        }
    }

    [Parameter]
    public string? HtmlItemId
    {
        get => _microdataAttributes?.HtmlItemId;
        set
        {
            if (value is not null) (_microdataAttributes ??= new MicrodataAttributes()).HtmlItemId = value;
            else if (_microdataAttributes is not null) _microdataAttributes.HtmlItemId = null;
        }
    }

    [Parameter]
    public string? ItemProp
    {
        get => _microdataAttributes?.ItemProp;
        set
        {
            if (value is not null) (_microdataAttributes ??= new MicrodataAttributes()).ItemProp = value;
            else if (_microdataAttributes is not null) _microdataAttributes.ItemProp = null;
        }
    }

    [Parameter]
    public string? ItemRef
    {
        get => _microdataAttributes?.ItemRef;
        set
        {
            if (value is not null) (_microdataAttributes ??= new MicrodataAttributes()).ItemRef = value;
            else if (_microdataAttributes is not null) _microdataAttributes.ItemRef = null;
        }
    }

    [Parameter]
    public bool? ItemScope
    {
        get => _microdataAttributes?.ItemScope;
        set
        {
            if (value is not null) (_microdataAttributes ??= new MicrodataAttributes()).ItemScope = value;
            else if (_microdataAttributes is not null) _microdataAttributes.ItemScope = null;
        }
    }

    [Parameter]
    public string? ItemType
    {
        get => _microdataAttributes?.ItemType;
        set
        {
            if (value is not null) (_microdataAttributes ??= new MicrodataAttributes()).ItemType = value;
            else if (_microdataAttributes is not null) _microdataAttributes.ItemType = null;
        }
    }

    [Parameter]
    public string? AriaLive
    {
        get => _extendedAriaAttributes?.AriaLive;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaLive = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaLive = null;
        }
    }

    [Parameter]
    public bool? AriaAtomic
    {
        get => _extendedAriaAttributes?.AriaAtomic;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaAtomic = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaAtomic = null;
        }
    }

    [Parameter]
    public bool? AriaBusy
    {
        get => _extendedAriaAttributes?.AriaBusy;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaBusy = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaBusy = null;
        }
    }

    [Parameter]
    public string? AriaRelevant
    {
        get => _extendedAriaAttributes?.AriaRelevant;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaRelevant = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaRelevant = null;
        }
    }

    [Parameter]
    public string? AriaControls
    {
        get => _extendedAriaAttributes?.AriaControls;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaControls = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaControls = null;
        }
    }

    [Parameter]
    public string? AriaDetails
    {
        get => _extendedAriaAttributes?.AriaDetails;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaDetails = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaDetails = null;
        }
    }

    [Parameter]
    public string? AriaErrorMessage
    {
        get => _extendedAriaAttributes?.AriaErrorMessage;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaErrorMessage = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaErrorMessage = null;
        }
    }

    [Parameter]
    public string? AriaFlowTo
    {
        get => _extendedAriaAttributes?.AriaFlowTo;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaFlowTo = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaFlowTo = null;
        }
    }

    [Parameter]
    public string? AriaKeyShortcuts
    {
        get => _extendedAriaAttributes?.AriaKeyShortcuts;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaKeyShortcuts = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaKeyShortcuts = null;
        }
    }

    [Parameter]
    public string? AriaOwns
    {
        get => _extendedAriaAttributes?.AriaOwns;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaOwns = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaOwns = null;
        }
    }

    [Parameter]
    public string? AriaRoleDescription
    {
        get => _extendedAriaAttributes?.AriaRoleDescription;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaRoleDescription = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaRoleDescription = null;
        }
    }

    [Parameter]
    public string? AriaDescription
    {
        get => _extendedAriaAttributes?.AriaDescription;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaDescription = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaDescription = null;
        }
    }

    [Parameter]
    public string? AriaBrailleLabel
    {
        get => _extendedAriaAttributes?.AriaBrailleLabel;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaBrailleLabel = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaBrailleLabel = null;
        }
    }

    [Parameter]
    public string? AriaBrailleRoleDescription
    {
        get => _extendedAriaAttributes?.AriaBrailleRoleDescription;
        set
        {
            if (value is not null) (_extendedAriaAttributes ??= new ExtendedAriaAttributes()).AriaBrailleRoleDescription = value;
            else if (_extendedAriaAttributes is not null) _extendedAriaAttributes.AriaBrailleRoleDescription = null;
        }
    }

    private RenderFragment? _lastChildContentRef;
    private ValueAtomicBool _childContentChanged;

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
            attributes["inert"] = Inert.Value ? QuarkAttributeValues.True : QuarkAttributeValues.False;

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
            attributes["itemscope"] = ItemScope.Value ? QuarkAttributeValues.True : QuarkAttributeValues.False;

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
        _childContentChanged.Value = !ReferenceEquals(cc, _lastChildContentRef);
        _lastChildContentRef = cc;

        base.OnParametersSet();
    }

    protected override bool ShouldRender()
    {
        if (_childContentChanged.Value)
        {
            _childContentChanged.Value = false;
            return true;
        }

        return base.ShouldRender();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (_childContentChanged.Value)
        {
            _lastChildContentRef = ChildContent;
            _childContentChanged.Value = false;
        }

        return base.OnAfterRenderAsync(firstRender);
    }
}

#pragma warning restore BL0007
