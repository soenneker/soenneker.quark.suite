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

        if (_editingHtmlAttributes is not null)
        {
            if (_editingHtmlAttributes.ContentEditable is not null)
                attributes["contenteditable"] = _editingHtmlAttributes.ContentEditable;

            if (_editingHtmlAttributes.SpellCheck.HasValue)
                attributes["spellcheck"] = _editingHtmlAttributes.SpellCheck.Value ? "true" : "false";

            if (_editingHtmlAttributes.AutoCapitalize is not null)
                attributes["autocapitalize"] = _editingHtmlAttributes.AutoCapitalize;

            if (_editingHtmlAttributes.AutoCorrect.HasValue)
                attributes["autocorrect"] = _editingHtmlAttributes.AutoCorrect.Value ? "on" : "off";

            if (_editingHtmlAttributes.EnterKeyHint is not null)
                attributes["enterkeyhint"] = _editingHtmlAttributes.EnterKeyHint;

            if (_editingHtmlAttributes.HtmlInputMode is not null)
                attributes["inputmode"] = _editingHtmlAttributes.HtmlInputMode;

            if (_editingHtmlAttributes.HtmlDraggable.HasValue)
                attributes["draggable"] = _editingHtmlAttributes.HtmlDraggable.Value ? "true" : "false";

            if (_editingHtmlAttributes.TranslateContent.HasValue)
                attributes["translate"] = _editingHtmlAttributes.TranslateContent.Value ? "yes" : "no";
        }

        if (Inert.HasValue)
            attributes["inert"] = Inert.Value ? QuarkAttributeValues.True : QuarkAttributeValues.False;

        if (AutoFocus)
            attributes["autofocus"] = QuarkAttributeValues.True;

        if (_advancedHtmlAttributes is not null)
        {
            if (_advancedHtmlAttributes.HtmlSlot is not null)
                attributes["slot"] = _advancedHtmlAttributes.HtmlSlot;

            if (_advancedHtmlAttributes.Part is not null)
                attributes["part"] = _advancedHtmlAttributes.Part;

            if (_advancedHtmlAttributes.ExportParts is not null)
                attributes["exportparts"] = _advancedHtmlAttributes.ExportParts;

            if (_advancedHtmlAttributes.Popover is not null)
                attributes["popover"] = _advancedHtmlAttributes.Popover;
        }

        if (_microdataAttributes is not null)
        {
            if (_microdataAttributes.HtmlItemId is not null)
                attributes["itemid"] = _microdataAttributes.HtmlItemId;

            if (_microdataAttributes.ItemProp is not null)
                attributes["itemprop"] = _microdataAttributes.ItemProp;

            if (_microdataAttributes.ItemRef is not null)
                attributes["itemref"] = _microdataAttributes.ItemRef;

            if (_microdataAttributes.ItemScope.HasValue)
                attributes["itemscope"] = _microdataAttributes.ItemScope.Value ? QuarkAttributeValues.True : QuarkAttributeValues.False;

            if (_microdataAttributes.ItemType is not null)
                attributes["itemtype"] = _microdataAttributes.ItemType;
        }

        if (_extendedAriaAttributes is not null)
        {
            if (_extendedAriaAttributes.AriaLive is not null)
                attributes["aria-live"] = _extendedAriaAttributes.AriaLive;

            if (_extendedAriaAttributes.AriaAtomic.HasValue)
                attributes["aria-atomic"] = _extendedAriaAttributes.AriaAtomic.Value ? "true" : "false";

            if (_extendedAriaAttributes.AriaBusy.HasValue)
                attributes["aria-busy"] = _extendedAriaAttributes.AriaBusy.Value ? "true" : "false";

            if (_extendedAriaAttributes.AriaRelevant is not null)
                attributes["aria-relevant"] = _extendedAriaAttributes.AriaRelevant;

            if (_extendedAriaAttributes.AriaControls is not null)
                attributes["aria-controls"] = _extendedAriaAttributes.AriaControls;

            if (_extendedAriaAttributes.AriaDetails is not null)
                attributes["aria-details"] = _extendedAriaAttributes.AriaDetails;

            if (_extendedAriaAttributes.AriaErrorMessage is not null)
                attributes["aria-errormessage"] = _extendedAriaAttributes.AriaErrorMessage;

            if (_extendedAriaAttributes.AriaFlowTo is not null)
                attributes["aria-flowto"] = _extendedAriaAttributes.AriaFlowTo;

            if (_extendedAriaAttributes.AriaKeyShortcuts is not null)
                attributes["aria-keyshortcuts"] = _extendedAriaAttributes.AriaKeyShortcuts;

            if (_extendedAriaAttributes.AriaOwns is not null)
                attributes["aria-owns"] = _extendedAriaAttributes.AriaOwns;

            if (_extendedAriaAttributes.AriaRoleDescription is not null)
                attributes["aria-roledescription"] = _extendedAriaAttributes.AriaRoleDescription;

            if (_extendedAriaAttributes.AriaDescription is not null)
                attributes["aria-description"] = _extendedAriaAttributes.AriaDescription;

            if (_extendedAriaAttributes.AriaBrailleLabel is not null)
                attributes["aria-braillelabel"] = _extendedAriaAttributes.AriaBrailleLabel;

            if (_extendedAriaAttributes.AriaBrailleRoleDescription is not null)
                attributes["aria-brailleroledescription"] = _extendedAriaAttributes.AriaBrailleRoleDescription;
        }

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
