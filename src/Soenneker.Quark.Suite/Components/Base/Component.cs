using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.Extensions.EventCallback;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <inheritdoc cref="IComponent"/>
/// <remarks>Do not use the <c>new</c> keyword to Shadow inherited <see cref="ParameterAttribute"/> members. Blazor treats those names as duplicate parameters and fails at runtime.</remarks>
#pragma warning disable BL0007 // Optional style groups use lazy storage without side effects.
public abstract class Component : RenderComponent, IComponent
{
    private DimensionUtilities? _dimensionUtilities;
    private PositionUtilities? _positionUtilities;
    private DisplayUtilities? _displayUtilities;
    private FontUtilities? _fontUtilities;
    private TextFlowUtilities? _textFlowUtilities;
    private TextDecorationUtilities? _textDecorationUtilities;
    private SpacingUtilities? _spacingUtilities;
    private FlexUtilities? _flexUtilities;
    private AlignmentUtilities? _alignmentUtilities;
    private GridPlacementUtilities? _gridPlacementUtilities;
    private SurfaceUtilities? _surfaceUtilities;
    private MotionUtilities? _motionUtilities;
    private InteractionUtilities? _interactionUtilities;
    private LogicalLayoutUtilities? _logicalLayoutUtilities;
    private BackgroundUtilities? _backgroundUtilities;
    private EffectUtilities? _effectUtilities;
    private AdvancedLayoutUtilities? _advancedLayoutUtilities;
    private PresetParameterBits _explicitParameters;
    private AdvancedTypographyUtilities? _advancedTypographyUtilities;
    private Transform3DUtilities? _transform3DUtilities;
    private MaskUtilities? _maskUtilities;

    [Inject]
    protected ILogger<Component> Logger { get; set; } = null!;

    [Inject]
    protected QuarkOptions QuarkOptions { get; set; } = null!;

    [Parameter]
    public CssValue<AspectRatioBuilder>? AspectRatio
    {
        get => _dimensionUtilities?.AspectRatio;
        set
        {
            if (value.HasValue) (_dimensionUtilities ??= new DimensionUtilities()).AspectRatio = value;
            else if (_dimensionUtilities is not null) _dimensionUtilities.AspectRatio = null;
        }
    }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<BackfaceVisibilityBuilder>? BackfaceVisibility
    {
        get => _transform3DUtilities?.BackfaceVisibility;
        set
        {
            if (value.HasValue)
                (_transform3DUtilities ??= new Transform3DUtilities()).BackfaceVisibility = value;
            else if (_transform3DUtilities is not null)
                _transform3DUtilities.BackfaceVisibility = null;
        }
    }


    [Parameter]
    public CssValue<BackgroundAttachmentBuilder>? BackgroundAttachment
    {
        get => _backgroundUtilities?.BackgroundAttachment;
        set
        {
            if (value.HasValue) (_backgroundUtilities ??= new BackgroundUtilities()).BackgroundAttachment = value;
            else if (_backgroundUtilities is not null) _backgroundUtilities.BackgroundAttachment = null;
        }
    }

    [Parameter]
    public CssValue<BackgroundBlendModeBuilder>? BackgroundBlendMode
    {
        get => _backgroundUtilities?.BackgroundBlendMode;
        set
        {
            if (value.HasValue) (_backgroundUtilities ??= new BackgroundUtilities()).BackgroundBlendMode = value;
            else if (_backgroundUtilities is not null) _backgroundUtilities.BackgroundBlendMode = null;
        }
    }

    [Parameter]
    public CssValue<BackgroundClipBuilder>? BackgroundClip
    {
        get => _backgroundUtilities?.BackgroundClip;
        set
        {
            if (value.HasValue) (_backgroundUtilities ??= new BackgroundUtilities()).BackgroundClip = value;
            else if (_backgroundUtilities is not null) _backgroundUtilities.BackgroundClip = null;
        }
    }

    [Parameter]
    public CssValue<BackgroundImageBuilder>? BackgroundImage
    {
        get => _backgroundUtilities?.BackgroundImage;
        set
        {
            if (value.HasValue) (_backgroundUtilities ??= new BackgroundUtilities()).BackgroundImage = value;
            else if (_backgroundUtilities is not null) _backgroundUtilities.BackgroundImage = null;
        }
    }

    [Parameter]
    public CssValue<BackgroundOriginBuilder>? BackgroundOrigin
    {
        get => _backgroundUtilities?.BackgroundOrigin;
        set
        {
            if (value.HasValue) (_backgroundUtilities ??= new BackgroundUtilities()).BackgroundOrigin = value;
            else if (_backgroundUtilities is not null) _backgroundUtilities.BackgroundOrigin = null;
        }
    }

    [Parameter]
    public CssValue<BackgroundPositionBuilder>? BackgroundPosition
    {
        get => _backgroundUtilities?.BackgroundPosition;
        set
        {
            if (value.HasValue) (_backgroundUtilities ??= new BackgroundUtilities()).BackgroundPosition = value;
            else if (_backgroundUtilities is not null) _backgroundUtilities.BackgroundPosition = null;
        }
    }

    [Parameter]
    public CssValue<BackgroundRepeatBuilder>? BackgroundRepeat
    {
        get => _backgroundUtilities?.BackgroundRepeat;
        set
        {
            if (value.HasValue) (_backgroundUtilities ??= new BackgroundUtilities()).BackgroundRepeat = value;
            else if (_backgroundUtilities is not null) _backgroundUtilities.BackgroundRepeat = null;
        }
    }

    [Parameter]
    public CssValue<BackgroundSizeBuilder>? BackgroundSize
    {
        get => _backgroundUtilities?.BackgroundSize;
        set
        {
            if (value.HasValue) (_backgroundUtilities ??= new BackgroundUtilities()).BackgroundSize = value;
            else if (_backgroundUtilities is not null) _backgroundUtilities.BackgroundSize = null;
        }
    }

    [Parameter]
    public CssValue<BlockSizeBuilder>? BlockSize
    {
        get => _logicalLayoutUtilities?.BlockSize;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).BlockSize = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.BlockSize = null;
        }
    }

    [Parameter]
    public CssValue<BoxDecorationBreakBuilder>? BoxDecorationBreak
    {
        get => _advancedLayoutUtilities?.BoxDecorationBreak;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).BoxDecorationBreak = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.BoxDecorationBreak = null;
        }
    }

    [Parameter]
    public CssValue<BoxSizingBuilder>? BoxSizing
    {
        get => _displayUtilities?.BoxSizing;
        set
        {
            if (value.HasValue) (_displayUtilities ??= new DisplayUtilities()).BoxSizing = value;
            else if (_displayUtilities is not null) _displayUtilities.BoxSizing = null;
        }
    }

    [Parameter]
    public CssValue<BreakAfterBuilder>? BreakAfter
    {
        get => _advancedLayoutUtilities?.BreakAfter;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).BreakAfter = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.BreakAfter = null;
        }
    }

    [Parameter]
    public CssValue<BreakBeforeBuilder>? BreakBefore
    {
        get => _advancedLayoutUtilities?.BreakBefore;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).BreakBefore = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.BreakBefore = null;
        }
    }

    [Parameter]
    public CssValue<BreakInsideBuilder>? BreakInside
    {
        get => _advancedLayoutUtilities?.BreakInside;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).BreakInside = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.BreakInside = null;
        }
    }

    [Parameter]
    public CssValue<ClearBuilder>? FloatClear
    {
        get => _displayUtilities?.FloatClear;
        set
        {
            if (value.HasValue) (_displayUtilities ??= new DisplayUtilities()).FloatClear = value;
            else if (_displayUtilities is not null) _displayUtilities.FloatClear = null;
        }
    }

    [Parameter]
    public CssValue<ClipPathBuilder>? ClipPath
    {
        get => _advancedLayoutUtilities?.ClipPath;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ClipPath = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ClipPath = null;
        }
    }

    [Parameter]
    public CssValue<ColEndBuilder>? ColEnd
    {
        get => _gridPlacementUtilities?.ColEnd;
        set
        {
            if (value.HasValue) (_gridPlacementUtilities ??= new GridPlacementUtilities()).ColEnd = value;
            else if (_gridPlacementUtilities is not null) _gridPlacementUtilities.ColEnd = null;
        }
    }

    [Parameter]
    public CssValue<ColorSchemeBuilder>? ColorScheme
    {
        get => _interactionUtilities?.ColorScheme;
        set
        {
            if (value.HasValue) (_interactionUtilities ??= new InteractionUtilities()).ColorScheme = value;
            else if (_interactionUtilities is not null) _interactionUtilities.ColorScheme = null;
        }
    }

    [Parameter]
    public CssValue<ColumnsBuilder>? ColumnCount
    {
        get => _advancedLayoutUtilities?.ColumnCount;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ColumnCount = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ColumnCount = null;
        }
    }

    [Parameter]
    public CssValue<ContainerTypeBuilder>? ContainerQuery
    {
        get => _displayUtilities?.ContainerQuery;
        set
        {
            if (value.HasValue) (_displayUtilities ??= new DisplayUtilities()).ContainerQuery = value;
            else if (_displayUtilities is not null) _displayUtilities.ContainerQuery = null;
        }
    }

    [Parameter]
    public CssValue<ContainBuilder>? Contain
    {
        get => _advancedLayoutUtilities?.Contain;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).Contain = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.Contain = null;
        }
    }

    [Parameter]
    public CssValue<ContentBuilder>? GeneratedContent
    {
        get => _textDecorationUtilities?.GeneratedContent;
        set
        {
            if (value.HasValue) (_textDecorationUtilities ??= new TextDecorationUtilities()).GeneratedContent = value;
            else if (_textDecorationUtilities is not null) _textDecorationUtilities.GeneratedContent = null;
        }
    }

    [Parameter]
    public CssValue<DecorationColorBuilder>? DecorationColor
    {
        get => _textDecorationUtilities?.DecorationColor;
        set
        {
            if (value.HasValue) (_textDecorationUtilities ??= new TextDecorationUtilities()).DecorationColor = value;
            else if (_textDecorationUtilities is not null) _textDecorationUtilities.DecorationColor = null;
        }
    }

    [Parameter]
    public CssValue<DecorationStyleBuilder>? DecorationStyle
    {
        get => _textDecorationUtilities?.DecorationStyle;
        set
        {
            if (value.HasValue) (_textDecorationUtilities ??= new TextDecorationUtilities()).DecorationStyle = value;
            else if (_textDecorationUtilities is not null) _textDecorationUtilities.DecorationStyle = null;
        }
    }

    [Parameter]
    public CssValue<DecorationThicknessBuilder>? DecorationThickness
    {
        get => _textDecorationUtilities?.DecorationThickness;
        set
        {
            if (value.HasValue) (_textDecorationUtilities ??= new TextDecorationUtilities()).DecorationThickness = value;
            else if (_textDecorationUtilities is not null) _textDecorationUtilities.DecorationThickness = null;
        }
    }

    [Parameter]
    public CssValue<DelayBuilder>? TransitionDelay
    {
        get => _motionUtilities?.TransitionDelay;
        set
        {
            if (value.HasValue) (_motionUtilities ??= new MotionUtilities()).TransitionDelay = value;
            else if (_motionUtilities is not null) _motionUtilities.TransitionDelay = null;
        }
    }

    [Parameter]
    public CssValue<EaseBuilder>? Ease
    {
        get => _motionUtilities?.Ease;
        set
        {
            if (value.HasValue) (_motionUtilities ??= new MotionUtilities()).Ease = value;
            else if (_motionUtilities is not null) _motionUtilities.Ease = null;
        }
    }

    [Parameter]
    public CssValue<FlexBasisBuilder>? FlexBasis
    {
        get => _flexUtilities?.FlexBasis;
        set
        {
            if (value.HasValue) (_flexUtilities ??= new FlexUtilities()).FlexBasis = value;
            else if (_flexUtilities is not null) _flexUtilities.FlexBasis = null;
        }
    }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<FontFeatureSettingsBuilder>? FontFeatureSettings
    {
        get => _advancedTypographyUtilities?.FontFeatureSettings;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).FontFeatureSettings = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.FontFeatureSettings = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<FontSmoothingBuilder>? FontSmoothing
    {
        get => _advancedTypographyUtilities?.FontSmoothing;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).FontSmoothing = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.FontSmoothing = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<FontStretchBuilder>? FontStretch
    {
        get => _advancedTypographyUtilities?.FontStretch;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).FontStretch = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.FontStretch = null;
        }
    }


    [Parameter]
    public CssValue<ForcedColorAdjustBuilder>? ForcedColorAdjust
    {
        get => _advancedLayoutUtilities?.ForcedColorAdjust;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ForcedColorAdjust = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ForcedColorAdjust = null;
        }
    }

    [Parameter]
    public CssValue<GradientBuilder>? BackgroundGradient
    {
        get => _backgroundUtilities?.BackgroundGradient;
        set
        {
            if (value.HasValue) (_backgroundUtilities ??= new BackgroundUtilities()).BackgroundGradient = value;
            else if (_backgroundUtilities is not null) _backgroundUtilities.BackgroundGradient = null;
        }
    }

    [Parameter]
    public CssValue<ColumnSpanBuilder>? ColumnSpan
    {
        get => _advancedLayoutUtilities?.ColumnSpan;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ColumnSpan = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ColumnSpan = null;
        }
    }

    [Parameter]
    public CssValue<HyphenBuilder>? Hyphen
    {
        get => _textFlowUtilities?.Hyphen;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).Hyphen = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.Hyphen = null;
        }
    }

    [Parameter]
    public CssValue<InlineSizeBuilder>? InlineSize
    {
        get => _logicalLayoutUtilities?.InlineSize;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).InlineSize = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.InlineSize = null;
        }
    }

    [Parameter]
    public CssValue<InsetBlockEndBuilder>? InsetBlockEnd
    {
        get => _logicalLayoutUtilities?.InsetBlockEnd;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).InsetBlockEnd = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.InsetBlockEnd = null;
        }
    }

    [Parameter]
    public CssValue<InsetBlockStartBuilder>? InsetBlockStart
    {
        get => _logicalLayoutUtilities?.InsetBlockStart;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).InsetBlockStart = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.InsetBlockStart = null;
        }
    }

    [Parameter]
    public CssValue<InsetEndBuilder>? InsetEnd
    {
        get => _logicalLayoutUtilities?.InsetEnd;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).InsetEnd = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.InsetEnd = null;
        }
    }

    [Parameter]
    public CssValue<InsetRingColorBuilder>? InsetRingColor
    {
        get => _effectUtilities?.InsetRingColor;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).InsetRingColor = value;
            else if (_effectUtilities is not null) _effectUtilities.InsetRingColor = null;
        }
    }

    [Parameter]
    public CssValue<InsetRingBuilder>? InsetRing
    {
        get => _effectUtilities?.InsetRing;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).InsetRing = value;
            else if (_effectUtilities is not null) _effectUtilities.InsetRing = null;
        }
    }

    [Parameter]
    public CssValue<InsetShadowColorBuilder>? InsetShadowColor
    {
        get => _effectUtilities?.InsetShadowColor;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).InsetShadowColor = value;
            else if (_effectUtilities is not null) _effectUtilities.InsetShadowColor = null;
        }
    }

    [Parameter]
    public CssValue<InsetShadowBuilder>? InsetShadow
    {
        get => _effectUtilities?.InsetShadow;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).InsetShadow = value;
            else if (_effectUtilities is not null) _effectUtilities.InsetShadow = null;
        }
    }

    [Parameter]
    public CssValue<InsetStartBuilder>? InsetStart
    {
        get => _logicalLayoutUtilities?.InsetStart;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).InsetStart = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.InsetStart = null;
        }
    }

    [Parameter]
    public CssValue<IsolationBuilder>? Isolation
    {
        get => _advancedLayoutUtilities?.Isolation;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).Isolation = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.Isolation = null;
        }
    }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskClipBuilder>? MaskClip
    {
        get => _maskUtilities?.MaskClip;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskClip = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskClip = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskCompositeBuilder>? MaskComposite
    {
        get => _maskUtilities?.MaskComposite;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskComposite = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskComposite = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskImageBuilder>? MaskImage
    {
        get => _maskUtilities?.MaskImage;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskImage = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskImage = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskModeBuilder>? MaskMode
    {
        get => _maskUtilities?.MaskMode;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskMode = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskMode = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskOriginBuilder>? MaskOrigin
    {
        get => _maskUtilities?.MaskOrigin;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskOrigin = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskOrigin = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskPositionBuilder>? MaskPosition
    {
        get => _maskUtilities?.MaskPosition;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskPosition = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskPosition = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskRepeatBuilder>? MaskRepeat
    {
        get => _maskUtilities?.MaskRepeat;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskRepeat = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskRepeat = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskSizeBuilder>? MaskSize
    {
        get => _maskUtilities?.MaskSize;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskSize = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskSize = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskTypeBuilder>? MaskType
    {
        get => _maskUtilities?.MaskType;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskType = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskType = null;
        }
    }


    [Parameter]
    public CssValue<MaxBlockSizeBuilder>? MaxBlockSize
    {
        get => _logicalLayoutUtilities?.MaxBlockSize;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).MaxBlockSize = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.MaxBlockSize = null;
        }
    }

    [Parameter]
    public CssValue<MaxInlineSizeBuilder>? MaxInlineSize
    {
        get => _logicalLayoutUtilities?.MaxInlineSize;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).MaxInlineSize = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.MaxInlineSize = null;
        }
    }

    [Parameter]
    public CssValue<MinBlockSizeBuilder>? MinBlockSize
    {
        get => _logicalLayoutUtilities?.MinBlockSize;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).MinBlockSize = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.MinBlockSize = null;
        }
    }

    [Parameter]
    public CssValue<MinInlineSizeBuilder>? MinInlineSize
    {
        get => _logicalLayoutUtilities?.MinInlineSize;
        set
        {
            if (value.HasValue) (_logicalLayoutUtilities ??= new LogicalLayoutUtilities()).MinInlineSize = value;
            else if (_logicalLayoutUtilities is not null) _logicalLayoutUtilities.MinInlineSize = null;
        }
    }

    [Parameter]
    public CssValue<MixBlendModeBuilder>? MixBlendMode
    {
        get => _effectUtilities?.MixBlendMode;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).MixBlendMode = value;
            else if (_effectUtilities is not null) _effectUtilities.MixBlendMode = null;
        }
    }

    [Parameter]
    public CssValue<OrderBuilder>? Order
    {
        get => _flexUtilities?.Order;
        set
        {
            if (value.HasValue) (_flexUtilities ??= new FlexUtilities()).Order = value;
            else if (_flexUtilities is not null) _flexUtilities.Order = null;
        }
    }

    [Parameter]
    public CssValue<OriginBuilder>? Origin
    {
        get => _advancedLayoutUtilities?.Origin;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).Origin = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.Origin = null;
        }
    }

    [Parameter]
    public CssValue<OutlineColorBuilder>? OutlineColor
    {
        get => _effectUtilities?.OutlineColor;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).OutlineColor = value;
            else if (_effectUtilities is not null) _effectUtilities.OutlineColor = null;
        }
    }

    [Parameter]
    public CssValue<OutlineOffsetBuilder>? OutlineOffset
    {
        get => _effectUtilities?.OutlineOffset;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).OutlineOffset = value;
            else if (_effectUtilities is not null) _effectUtilities.OutlineOffset = null;
        }
    }

    [Parameter]
    public CssValue<OutlineWidthBuilder>? OutlineWidth
    {
        get => _effectUtilities?.OutlineWidth;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).OutlineWidth = value;
            else if (_effectUtilities is not null) _effectUtilities.OutlineWidth = null;
        }
    }

    [Parameter]
    public CssValue<OverflowWrapBuilder>? OverflowWrap
    {
        get => _textFlowUtilities?.OverflowWrap;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).OverflowWrap = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.OverflowWrap = null;
        }
    }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<PerspectiveOriginBuilder>? PerspectiveOrigin
    {
        get => _transform3DUtilities?.PerspectiveOrigin;
        set
        {
            if (value.HasValue)
                (_transform3DUtilities ??= new Transform3DUtilities()).PerspectiveOrigin = value;
            else if (_transform3DUtilities is not null)
                _transform3DUtilities.PerspectiveOrigin = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<PerspectiveBuilder>? Perspective
    {
        get => _transform3DUtilities?.Perspective;
        set
        {
            if (value.HasValue)
                (_transform3DUtilities ??= new Transform3DUtilities()).Perspective = value;
            else if (_transform3DUtilities is not null)
                _transform3DUtilities.Perspective = null;
        }
    }


    [Parameter]
    public CssValue<PlaceContentAlignBuilder>? PlaceContentAlign
    {
        get => _alignmentUtilities?.PlaceContentAlign;
        set
        {
            if (value.HasValue) (_alignmentUtilities ??= new AlignmentUtilities()).PlaceContentAlign = value;
            else if (_alignmentUtilities is not null) _alignmentUtilities.PlaceContentAlign = null;
        }
    }

    [Parameter]
    public CssValue<PlaceItemsAlignBuilder>? PlaceItemsAlign
    {
        get => _alignmentUtilities?.PlaceItemsAlign;
        set
        {
            if (value.HasValue) (_alignmentUtilities ??= new AlignmentUtilities()).PlaceItemsAlign = value;
            else if (_alignmentUtilities is not null) _alignmentUtilities.PlaceItemsAlign = null;
        }
    }

    [Parameter]
    public CssValue<PlaceSelfAlignBuilder>? PlaceSelfAlign
    {
        get => _alignmentUtilities?.PlaceSelfAlign;
        set
        {
            if (value.HasValue) (_alignmentUtilities ??= new AlignmentUtilities()).PlaceSelfAlign = value;
            else if (_alignmentUtilities is not null) _alignmentUtilities.PlaceSelfAlign = null;
        }
    }

    [Parameter]
    public CssValue<RotateBuilder>? Rotate
    {
        get => _advancedLayoutUtilities?.Rotate;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).Rotate = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.Rotate = null;
        }
    }

    [Parameter]
    public CssValue<RowEndBuilder>? RowEnd
    {
        get => _gridPlacementUtilities?.RowEnd;
        set
        {
            if (value.HasValue) (_gridPlacementUtilities ??= new GridPlacementUtilities()).RowEnd = value;
            else if (_gridPlacementUtilities is not null) _gridPlacementUtilities.RowEnd = null;
        }
    }

    [Parameter]
    public CssValue<ScaleBuilder>? Scale
    {
        get => _motionUtilities?.Scale;
        set
        {
            if (value.HasValue) (_motionUtilities ??= new MotionUtilities()).Scale = value;
            else if (_motionUtilities is not null) _motionUtilities.Scale = null;
        }
    }

    [Parameter]
    public CssValue<ScrollbarGutterBuilder>? ScrollbarGutter
    {
        get => _advancedLayoutUtilities?.ScrollbarGutter;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollbarGutter = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollbarGutter = null;
        }
    }

    [Parameter]
    public CssValue<ScrollbarThumbColorBuilder>? ScrollbarThumbColor
    {
        get => _advancedLayoutUtilities?.ScrollbarThumbColor;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollbarThumbColor = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollbarThumbColor = null;
        }
    }

    [Parameter]
    public CssValue<ScrollbarTrackColorBuilder>? ScrollbarTrackColor
    {
        get => _advancedLayoutUtilities?.ScrollbarTrackColor;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollbarTrackColor = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollbarTrackColor = null;
        }
    }

    [Parameter]
    public CssValue<ScrollbarWidthBuilder>? ScrollbarWidth
    {
        get => _advancedLayoutUtilities?.ScrollbarWidth;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollbarWidth = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollbarWidth = null;
        }
    }

    [Parameter]
    public CssValue<ScrollBehaviorBuilder>? ScrollBehavior
    {
        get => _advancedLayoutUtilities?.ScrollBehavior;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollBehavior = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollBehavior = null;
        }
    }

    [Parameter]
    public CssValue<ScrollSnapAlignBuilder>? ScrollSnapAlign
    {
        get => _advancedLayoutUtilities?.ScrollSnapAlign;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollSnapAlign = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollSnapAlign = null;
        }
    }

    [Parameter]
    public CssValue<ScrollSnapBuilder>? ScrollSnap
    {
        get => _advancedLayoutUtilities?.ScrollSnap;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollSnap = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollSnap = null;
        }
    }

    [Parameter]
    public CssValue<ScrollSnapStopBuilder>? ScrollSnapStop
    {
        get => _advancedLayoutUtilities?.ScrollSnapStop;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollSnapStop = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollSnapStop = null;
        }
    }

    [Parameter]
    public CssValue<ShadowColorBuilder>? ShadowColor
    {
        get => _surfaceUtilities?.ShadowColor;
        set
        {
            if (value.HasValue) (_surfaceUtilities ??= new SurfaceUtilities()).ShadowColor = value;
            else if (_surfaceUtilities is not null) _surfaceUtilities.ShadowColor = null;
        }
    }

    [Parameter]
    public CssValue<SkewBuilder>? Skew
    {
        get => _advancedLayoutUtilities?.Skew;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).Skew = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.Skew = null;
        }
    }

    [Parameter]
    public CssValue<TabSizeBuilder>? TabSize
    {
        get => _textFlowUtilities?.TabSize;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).TabSize = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.TabSize = null;
        }
    }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<TextIndentBuilder>? TextIndent
    {
        get => _advancedTypographyUtilities?.TextIndent;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).TextIndent = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.TextIndent = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<TextShadowColorBuilder>? TextShadowColor
    {
        get => _advancedTypographyUtilities?.TextShadowColor;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).TextShadowColor = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.TextShadowColor = null;
        }
    }


#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<TextShadowBuilder>? TextShadow
    {
        get => _advancedTypographyUtilities?.TextShadow;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).TextShadow = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.TextShadow = null;
        }
    }


    [Parameter]
    public CssValue<TouchActionBuilder>? TouchAction
    {
        get => _advancedLayoutUtilities?.TouchAction;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).TouchAction = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.TouchAction = null;
        }
    }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<TransformStyleBuilder>? TransformStyle
    {
        get => _transform3DUtilities?.TransformStyle;
        set
        {
            if (value.HasValue)
                (_transform3DUtilities ??= new Transform3DUtilities()).TransformStyle = value;
            else if (_transform3DUtilities is not null)
                _transform3DUtilities.TransformStyle = null;
        }
    }


    [Parameter]
    public CssValue<TransitionBehaviorBuilder>? TransitionBehavior
    {
        get => _motionUtilities?.TransitionBehavior;
        set
        {
            if (value.HasValue) (_motionUtilities ??= new MotionUtilities()).TransitionBehavior = value;
            else if (_motionUtilities is not null) _motionUtilities.TransitionBehavior = null;
        }
    }

    [Parameter]
    public CssValue<TranslateBuilder>? Translate
    {
        get => _advancedLayoutUtilities?.Translate;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).Translate = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.Translate = null;
        }
    }

    [Parameter]
    public CssValue<WillChangeBuilder>? WillChange
    {
        get => _advancedLayoutUtilities?.WillChange;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).WillChange = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.WillChange = null;
        }
    }

    [Parameter]
    public CssValue<ZoomBuilder>? Zoom
    {
        get => _motionUtilities?.Zoom;
        set
        {
            if (value.HasValue) (_motionUtilities ??= new MotionUtilities()).Zoom = value;
            else if (_motionUtilities is not null) _motionUtilities.Zoom = null;
        }
    }

    [Parameter]
    public bool Container { get; set; }

    [Parameter]
    public QuarkPresetToken? Preset { get; set; }

    [Parameter]
    public IReadOnlyList<QuarkPresetToken>? Presets { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public bool Hidden { get; set; }

    [Parameter]
    public string? DataSlot { get; set; }

    [Parameter]
    public CssValue<InsetBuilder>? Inset
    {
        get => _positionUtilities?.Inset;
        set
        {
            if (value.HasValue) (_positionUtilities ??= new PositionUtilities()).Inset = value;
            else if (_positionUtilities is not null) _positionUtilities.Inset = null;
        }
    }

    [Parameter]
    public CssValue<TopBuilder>? Top
    {
        get => _positionUtilities?.Top;
        set
        {
            if (value.HasValue) (_positionUtilities ??= new PositionUtilities()).Top = value;
            else if (_positionUtilities is not null) _positionUtilities.Top = null;
        }
    }

    [Parameter]
    public CssValue<RightBuilder>? Right
    {
        get => _positionUtilities?.Right;
        set
        {
            if (value.HasValue) (_positionUtilities ??= new PositionUtilities()).Right = value;
            else if (_positionUtilities is not null) _positionUtilities.Right = null;
        }
    }

    [Parameter]
    public CssValue<BottomBuilder>? Bottom
    {
        get => _positionUtilities?.Bottom;
        set
        {
            if (value.HasValue) (_positionUtilities ??= new PositionUtilities()).Bottom = value;
            else if (_positionUtilities is not null) _positionUtilities.Bottom = null;
        }
    }

    [Parameter]
    public CssValue<LeftBuilder>? Left
    {
        get => _positionUtilities?.Left;
        set
        {
            if (value.HasValue) (_positionUtilities ??= new PositionUtilities()).Left = value;
            else if (_positionUtilities is not null) _positionUtilities.Left = null;
        }
    }

    [Parameter]
    public CssValue<DisplayBuilder>? Display
    {
        get => _displayUtilities?.Display;
        set
        {
            if (value.HasValue) (_displayUtilities ??= new DisplayUtilities()).Display = value;
            else if (_displayUtilities is not null) _displayUtilities.Display = null;
        }
    }

    [Parameter]
    public CssValue<VisibilityBuilder>? Visibility
    {
        get => _displayUtilities?.Visibility;
        set
        {
            if (value.HasValue) (_displayUtilities ??= new DisplayUtilities()).Visibility = value;
            else if (_displayUtilities is not null) _displayUtilities.Visibility = null;
        }
    }

    [Parameter]
    public CssValue<FloatBuilder>? Float
    {
        get => _displayUtilities?.Float;
        set
        {
            if (value.HasValue) (_displayUtilities ??= new DisplayUtilities()).Float = value;
            else if (_displayUtilities is not null) _displayUtilities.Float = null;
        }
    }

    [Parameter]
    public CssValue<VerticalAlignBuilder>? VerticalAlign
    {
        get => _advancedLayoutUtilities?.VerticalAlign;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).VerticalAlign = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.VerticalAlign = null;
        }
    }

    [Parameter]
    public CssValue<TextAlignBuilder>? TextAlign
    {
        get => _textFlowUtilities?.TextAlign;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).TextAlign = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.TextAlign = null;
        }
    }

    [Parameter]
    public CssValue<TextColorBuilder>? TextColor
    {
        get => _surfaceUtilities?.TextColor;
        set
        {
            if (value.HasValue) (_surfaceUtilities ??= new SurfaceUtilities()).TextColor = value;
            else if (_surfaceUtilities is not null) _surfaceUtilities.TextColor = null;
        }
    }

    [Parameter]
    public CssValue<TextSizeBuilder>? TextSize
    {
        get => _fontUtilities?.TextSize;
        set
        {
            if (value.HasValue) (_fontUtilities ??= new FontUtilities()).TextSize = value;
            else if (_fontUtilities is not null) _fontUtilities.TextSize = null;
        }
    }

    [Parameter]
    public CssValue<DecorationLineBuilder>? DecorationLine
    {
        get => _textDecorationUtilities?.DecorationLine;
        set
        {
            if (value.HasValue) (_textDecorationUtilities ??= new TextDecorationUtilities()).DecorationLine = value;
            else if (_textDecorationUtilities is not null) _textDecorationUtilities.DecorationLine = null;
        }
    }

    [Parameter]
    public CssValue<UnderlineOffsetBuilder>? UnderlineOffset
    {
        get => _textDecorationUtilities?.UnderlineOffset;
        set
        {
            if (value.HasValue) (_textDecorationUtilities ??= new TextDecorationUtilities()).UnderlineOffset = value;
            else if (_textDecorationUtilities is not null) _textDecorationUtilities.UnderlineOffset = null;
        }
    }

    [Parameter]
    public CssValue<TextTransformBuilder>? TextTransform
    {
        get => _textFlowUtilities?.TextTransform;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).TextTransform = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.TextTransform = null;
        }
    }

    [Parameter]
    public CssValue<FontFamilyBuilder>? FontFamily
    {
        get => _fontUtilities?.FontFamily;
        set
        {
            if (value.HasValue) (_fontUtilities ??= new FontUtilities()).FontFamily = value;
            else if (_fontUtilities is not null) _fontUtilities.FontFamily = null;
        }
    }

    [Parameter]
    public CssValue<FontWeightBuilder>? FontWeight
    {
        get => _fontUtilities?.FontWeight;
        set
        {
            if (value.HasValue) (_fontUtilities ??= new FontUtilities()).FontWeight = value;
            else if (_fontUtilities is not null) _fontUtilities.FontWeight = null;
        }
    }

    [Parameter]
    public CssValue<FontStyleBuilder>? FontStyle
    {
        get => _fontUtilities?.FontStyle;
        set
        {
            if (value.HasValue) (_fontUtilities ??= new FontUtilities()).FontStyle = value;
            else if (_fontUtilities is not null) _fontUtilities.FontStyle = null;
        }
    }

    [Parameter]
    public CssValue<LeadingBuilder>? Leading
    {
        get => _fontUtilities?.Leading;
        set
        {
            if (value.HasValue) (_fontUtilities ??= new FontUtilities()).Leading = value;
            else if (_fontUtilities is not null) _fontUtilities.Leading = null;
        }
    }

    [Parameter]
    public CssValue<TrackingBuilder>? Tracking
    {
        get => _fontUtilities?.Tracking;
        set
        {
            if (value.HasValue) (_fontUtilities ??= new FontUtilities()).Tracking = value;
            else if (_fontUtilities is not null) _fontUtilities.Tracking = null;
        }
    }

    [Parameter]
    public CssValue<WhitespaceBuilder>? Whitespace
    {
        get => _textFlowUtilities?.Whitespace;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).Whitespace = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.Whitespace = null;
        }
    }

    [Parameter]
    public CssValue<TextWrapBuilder>? TextWrap
    {
        get => _textFlowUtilities?.TextWrap;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).TextWrap = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.TextWrap = null;
        }
    }

    [Parameter]
    public CssValue<WordBreakBuilder>? WordBreak
    {
        get => _textFlowUtilities?.WordBreak;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).WordBreak = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.WordBreak = null;
        }
    }

    [Parameter]
    public CssValue<TextOverflowBuilder>? TextOverflow
    {
        get => _textFlowUtilities?.TextOverflow;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).TextOverflow = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.TextOverflow = null;
        }
    }

    [Parameter]
    public CssValue<TruncateBuilder>? Truncate
    {
        get => _textFlowUtilities?.Truncate;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).Truncate = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.Truncate = null;
        }
    }

    [Parameter]
    public CssValue<LineClampBuilder>? LineClamp
    {
        get => _textFlowUtilities?.LineClamp;
        set
        {
            if (value.HasValue) (_textFlowUtilities ??= new TextFlowUtilities()).LineClamp = value;
            else if (_textFlowUtilities is not null) _textFlowUtilities.LineClamp = null;
        }
    }

    [Parameter]
    public CssValue<FontVariantNumericBuilder>? FontVariantNumeric
    {
        get => _fontUtilities?.FontVariantNumeric;
        set
        {
            if (value.HasValue) (_fontUtilities ??= new FontUtilities()).FontVariantNumeric = value;
            else if (_fontUtilities is not null) _fontUtilities.FontVariantNumeric = null;
        }
    }

    [Parameter]
    public CssValue<MarginBuilder>? Margin
    {
        get => _spacingUtilities?.Margin;
        set
        {
            if (value.HasValue) (_spacingUtilities ??= new SpacingUtilities()).Margin = value;
            else if (_spacingUtilities is not null) _spacingUtilities.Margin = null;
        }
    }

    [Parameter]
    public CssValue<PaddingBuilder>? Padding
    {
        get => _spacingUtilities?.Padding;
        set
        {
            if (value.HasValue) (_spacingUtilities ??= new SpacingUtilities()).Padding = value;
            else if (_spacingUtilities is not null) _spacingUtilities.Padding = null;
        }
    }

    [Parameter]
    public CssValue<PositionBuilder>? Position
    {
        get => _positionUtilities?.Position;
        set
        {
            if (value.HasValue) (_positionUtilities ??= new PositionUtilities()).Position = value;
            else if (_positionUtilities is not null) _positionUtilities.Position = null;
        }
    }

    [Parameter]
    public CssValue<ScrollMarginBuilder>? ScrollMargin
    {
        get => _advancedLayoutUtilities?.ScrollMargin;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollMargin = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollMargin = null;
        }
    }

    [Parameter]
    public CssValue<ScrollPaddingBuilder>? ScrollPadding
    {
        get => _advancedLayoutUtilities?.ScrollPadding;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).ScrollPadding = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.ScrollPadding = null;
        }
    }

    [Parameter]
    public CssValue<SizeBuilder>? Size
    {
        get => _dimensionUtilities?.Size;
        set
        {
            if (value.HasValue) (_dimensionUtilities ??= new DimensionUtilities()).Size = value;
            else if (_dimensionUtilities is not null) _dimensionUtilities.Size = null;
        }
    }

    [Parameter]
    public CssValue<WidthBuilder>? Width
    {
        get => _dimensionUtilities?.Width;
        set
        {
            if (value.HasValue) (_dimensionUtilities ??= new DimensionUtilities()).Width = value;
            else if (_dimensionUtilities is not null) _dimensionUtilities.Width = null;
        }
    }

    [Parameter]
    public CssValue<MinWidthBuilder>? MinWidth
    {
        get => _dimensionUtilities?.MinWidth;
        set
        {
            if (value.HasValue) (_dimensionUtilities ??= new DimensionUtilities()).MinWidth = value;
            else if (_dimensionUtilities is not null) _dimensionUtilities.MinWidth = null;
        }
    }

    [Parameter]
    public CssValue<MaxWidthBuilder>? MaxWidth
    {
        get => _dimensionUtilities?.MaxWidth;
        set
        {
            if (value.HasValue) (_dimensionUtilities ??= new DimensionUtilities()).MaxWidth = value;
            else if (_dimensionUtilities is not null) _dimensionUtilities.MaxWidth = null;
        }
    }

    [Parameter]
    public CssValue<HeightBuilder>? Height
    {
        get => _dimensionUtilities?.Height;
        set
        {
            if (value.HasValue) (_dimensionUtilities ??= new DimensionUtilities()).Height = value;
            else if (_dimensionUtilities is not null) _dimensionUtilities.Height = null;
        }
    }

    [Parameter]
    public CssValue<MinHeightBuilder>? MinHeight
    {
        get => _dimensionUtilities?.MinHeight;
        set
        {
            if (value.HasValue) (_dimensionUtilities ??= new DimensionUtilities()).MinHeight = value;
            else if (_dimensionUtilities is not null) _dimensionUtilities.MinHeight = null;
        }
    }

    [Parameter]
    public CssValue<MaxHeightBuilder>? MaxHeight
    {
        get => _dimensionUtilities?.MaxHeight;
        set
        {
            if (value.HasValue) (_dimensionUtilities ??= new DimensionUtilities()).MaxHeight = value;
            else if (_dimensionUtilities is not null) _dimensionUtilities.MaxHeight = null;
        }
    }

    [Parameter]
    public CssValue<OverflowBuilder>? Overflow
    {
        get => _displayUtilities?.Overflow;
        set
        {
            if (value.HasValue) (_displayUtilities ??= new DisplayUtilities()).Overflow = value;
            else if (_displayUtilities is not null) _displayUtilities.Overflow = null;
        }
    }

    [Parameter]
    public CssValue<OverflowBuilder>? OverflowX
    {
        get => _displayUtilities?.OverflowX;
        set
        {
            if (value.HasValue) (_displayUtilities ??= new DisplayUtilities()).OverflowX = value;
            else if (_displayUtilities is not null) _displayUtilities.OverflowX = null;
        }
    }

    [Parameter]
    public CssValue<OverflowBuilder>? OverflowY
    {
        get => _displayUtilities?.OverflowY;
        set
        {
            if (value.HasValue) (_displayUtilities ??= new DisplayUtilities()).OverflowY = value;
            else if (_displayUtilities is not null) _displayUtilities.OverflowY = null;
        }
    }

    [Parameter]
    public CssValue<OverscrollBuilder>? Overscroll
    {
        get => _advancedLayoutUtilities?.Overscroll;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).Overscroll = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.Overscroll = null;
        }
    }

    [Parameter]
    public CssValue<FlexBuilder>? Flex
    {
        get => _flexUtilities?.Flex;
        set
        {
            if (value.HasValue) (_flexUtilities ??= new FlexUtilities()).Flex = value;
            else if (_flexUtilities is not null) _flexUtilities.Flex = null;
        }
    }

    [Parameter]
    public CssValue<FlexDirectionBuilder>? FlexDirection
    {
        get => _flexUtilities?.FlexDirection;
        set
        {
            if (value.HasValue) (_flexUtilities ??= new FlexUtilities()).FlexDirection = value;
            else if (_flexUtilities is not null) _flexUtilities.FlexDirection = null;
        }
    }

    [Parameter]
    public CssValue<FlexWrapBuilder>? FlexWrap
    {
        get => _flexUtilities?.FlexWrap;
        set
        {
            if (value.HasValue) (_flexUtilities ??= new FlexUtilities()).FlexWrap = value;
            else if (_flexUtilities is not null) _flexUtilities.FlexWrap = null;
        }
    }

    [Parameter]
    public CssValue<GrowBuilder>? Grow
    {
        get => _flexUtilities?.Grow;
        set
        {
            if (value.HasValue) (_flexUtilities ??= new FlexUtilities()).Grow = value;
            else if (_flexUtilities is not null) _flexUtilities.Grow = null;
        }
    }

    [Parameter]
    public CssValue<ShrinkBuilder>? Shrink
    {
        get => _flexUtilities?.Shrink;
        set
        {
            if (value.HasValue) (_flexUtilities ??= new FlexUtilities()).Shrink = value;
            else if (_flexUtilities is not null) _flexUtilities.Shrink = null;
        }
    }

    [Parameter]
    public CssValue<GapBuilder>? Gap
    {
        get => _spacingUtilities?.Gap;
        set
        {
            if (value.HasValue) (_spacingUtilities ??= new SpacingUtilities()).Gap = value;
            else if (_spacingUtilities is not null) _spacingUtilities.Gap = null;
        }
    }

    [Parameter]
    public CssValue<SpaceBuilder>? Space
    {
        get => _spacingUtilities?.Space;
        set
        {
            if (value.HasValue) (_spacingUtilities ??= new SpacingUtilities()).Space = value;
            else if (_spacingUtilities is not null) _spacingUtilities.Space = null;
        }
    }

    [Parameter]
    public CssValue<DivideBuilder>? Divide
    {
        get => _spacingUtilities?.Divide;
        set
        {
            if (value.HasValue) (_spacingUtilities ??= new SpacingUtilities()).Divide = value;
            else if (_spacingUtilities is not null) _spacingUtilities.Divide = null;
        }
    }

    [Parameter]
    public CssValue<ContentAlignBuilder>? ContentAlign
    {
        get => _alignmentUtilities?.ContentAlign;
        set
        {
            if (value.HasValue) (_alignmentUtilities ??= new AlignmentUtilities()).ContentAlign = value;
            else if (_alignmentUtilities is not null) _alignmentUtilities.ContentAlign = null;
        }
    }

    [Parameter]
    public CssValue<ItemsBuilder>? ItemsAlign
    {
        get => _alignmentUtilities?.ItemsAlign;
        set
        {
            if (value.HasValue) (_alignmentUtilities ??= new AlignmentUtilities()).ItemsAlign = value;
            else if (_alignmentUtilities is not null) _alignmentUtilities.ItemsAlign = null;
        }
    }

    [Parameter]
    public CssValue<JustifyBuilder>? Justify
    {
        get => _alignmentUtilities?.Justify;
        set
        {
            if (value.HasValue) (_alignmentUtilities ??= new AlignmentUtilities()).Justify = value;
            else if (_alignmentUtilities is not null) _alignmentUtilities.Justify = null;
        }
    }

    [Parameter]
    public CssValue<SelfBuilder>? SelfAlign
    {
        get => _alignmentUtilities?.SelfAlign;
        set
        {
            if (value.HasValue) (_alignmentUtilities ??= new AlignmentUtilities()).SelfAlign = value;
            else if (_alignmentUtilities is not null) _alignmentUtilities.SelfAlign = null;
        }
    }

    [Parameter]
    public CssValue<JustifyItemsAlignBuilder>? JustifyItemsAlign
    {
        get => _alignmentUtilities?.JustifyItemsAlign;
        set
        {
            if (value.HasValue) (_alignmentUtilities ??= new AlignmentUtilities()).JustifyItemsAlign = value;
            else if (_alignmentUtilities is not null) _alignmentUtilities.JustifyItemsAlign = null;
        }
    }

    [Parameter]
    public CssValue<JustifySelfAlignBuilder>? JustifySelfAlign
    {
        get => _alignmentUtilities?.JustifySelfAlign;
        set
        {
            if (value.HasValue) (_alignmentUtilities ??= new AlignmentUtilities()).JustifySelfAlign = value;
            else if (_alignmentUtilities is not null) _alignmentUtilities.JustifySelfAlign = null;
        }
    }

    [Parameter]
    public CssValue<ColStartBuilder>? ColStart
    {
        get => _gridPlacementUtilities?.ColStart;
        set
        {
            if (value.HasValue) (_gridPlacementUtilities ??= new GridPlacementUtilities()).ColStart = value;
            else if (_gridPlacementUtilities is not null) _gridPlacementUtilities.ColStart = null;
        }
    }

    [Parameter]
    public CssValue<RowSpanBuilder>? RowSpan
    {
        get => _gridPlacementUtilities?.RowSpan;
        set
        {
            if (value.HasValue) (_gridPlacementUtilities ??= new GridPlacementUtilities()).RowSpan = value;
            else if (_gridPlacementUtilities is not null) _gridPlacementUtilities.RowSpan = null;
        }
    }

    [Parameter]
    public CssValue<RowStartBuilder>? RowStart
    {
        get => _gridPlacementUtilities?.RowStart;
        set
        {
            if (value.HasValue) (_gridPlacementUtilities ??= new GridPlacementUtilities()).RowStart = value;
            else if (_gridPlacementUtilities is not null) _gridPlacementUtilities.RowStart = null;
        }
    }

    [Parameter]
    public CssValue<OpacityBuilder>? Opacity
    {
        get => _interactionUtilities?.Opacity;
        set
        {
            if (value.HasValue) (_interactionUtilities ??= new InteractionUtilities()).Opacity = value;
            else if (_interactionUtilities is not null) _interactionUtilities.Opacity = null;
        }
    }

    [Parameter]
    public CssValue<ZIndexBuilder>? ZIndex
    {
        get => _positionUtilities?.ZIndex;
        set
        {
            if (value.HasValue) (_positionUtilities ??= new PositionUtilities()).ZIndex = value;
            else if (_positionUtilities is not null) _positionUtilities.ZIndex = null;
        }
    }

    [Parameter]
    public CssValue<PointerEventsBuilder>? PointerEvents
    {
        get => _interactionUtilities?.PointerEvents;
        set
        {
            if (value.HasValue) (_interactionUtilities ??= new InteractionUtilities()).PointerEvents = value;
            else if (_interactionUtilities is not null) _interactionUtilities.PointerEvents = null;
        }
    }

    [Parameter]
    public CssValue<UserSelectBuilder>? UserSelect
    {
        get => _advancedLayoutUtilities?.UserSelect;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).UserSelect = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.UserSelect = null;
        }
    }

    [Parameter]
    public CssValue<CursorBuilder>? Cursor
    {
        get => _interactionUtilities?.Cursor;
        set
        {
            if (value.HasValue) (_interactionUtilities ??= new InteractionUtilities()).Cursor = value;
            else if (_interactionUtilities is not null) _interactionUtilities.Cursor = null;
        }
    }

    [Parameter]
    public CssValue<ScreenReaderBuilder>? ScreenReader
    {
        get => _interactionUtilities?.ScreenReader;
        set
        {
            if (value.HasValue) (_interactionUtilities ??= new InteractionUtilities()).ScreenReader = value;
            else if (_interactionUtilities is not null) _interactionUtilities.ScreenReader = null;
        }
    }

    [Parameter]
    public CssValue<BackgroundColorBuilder>? BackgroundColor
    {
        get => _surfaceUtilities?.BackgroundColor;
        set
        {
            if (value.HasValue) (_surfaceUtilities ??= new SurfaceUtilities()).BackgroundColor = value;
            else if (_surfaceUtilities is not null) _surfaceUtilities.BackgroundColor = null;
        }
    }

    [Parameter]
    public CssValue<BorderBuilder>? Border
    {
        get => _surfaceUtilities?.Border;
        set
        {
            if (value.HasValue) (_surfaceUtilities ??= new SurfaceUtilities()).Border = value;
            else if (_surfaceUtilities is not null) _surfaceUtilities.Border = null;
        }
    }

    [Parameter]
    public CssValue<BorderStyleBuilder>? BorderStyle
    {
        get => _surfaceUtilities?.BorderStyle;
        set
        {
            if (value.HasValue) (_surfaceUtilities ??= new SurfaceUtilities()).BorderStyle = value;
            else if (_surfaceUtilities is not null) _surfaceUtilities.BorderStyle = null;
        }
    }

    [Parameter]
    public CssValue<BorderColorBuilder>? BorderColor
    {
        get => _surfaceUtilities?.BorderColor;
        set
        {
            if (value.HasValue) (_surfaceUtilities ??= new SurfaceUtilities()).BorderColor = value;
            else if (_surfaceUtilities is not null) _surfaceUtilities.BorderColor = null;
        }
    }

    [Parameter]
    public virtual CssValue<RoundedBuilder>? Rounded { get; set; }

    [Parameter]
    public CssValue<RingBuilder>? Ring
    {
        get => _surfaceUtilities?.Ring;
        set
        {
            if (value.HasValue) (_surfaceUtilities ??= new SurfaceUtilities()).Ring = value;
            else if (_surfaceUtilities is not null) _surfaceUtilities.Ring = null;
        }
    }

    [Parameter]
    public CssValue<RingOffsetBuilder>? RingOffset
    {
        get => _surfaceUtilities?.RingOffset;
        set
        {
            if (value.HasValue) (_surfaceUtilities ??= new SurfaceUtilities()).RingOffset = value;
            else if (_surfaceUtilities is not null) _surfaceUtilities.RingOffset = null;
        }
    }

    [Parameter]
    public CssValue<RingColorBuilder>? RingColor
    {
        get => _surfaceUtilities?.RingColor;
        set
        {
            if (value.HasValue) (_surfaceUtilities ??= new SurfaceUtilities()).RingColor = value;
            else if (_surfaceUtilities is not null) _surfaceUtilities.RingColor = null;
        }
    }

    [Parameter]
    public CssValue<OutlineStyleBuilder>? OutlineStyle
    {
        get => _effectUtilities?.OutlineStyle;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).OutlineStyle = value;
            else if (_effectUtilities is not null) _effectUtilities.OutlineStyle = null;
        }
    }

    [Parameter]
    public virtual CssValue<ShadowBuilder>? Shadow { get; set; }

    [Parameter]
    public CssValue<BackdropFilterBuilder>? BackdropFilter
    {
        get => _effectUtilities?.BackdropFilter;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropFilter = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropFilter = null;
        }
    }

    [Parameter]
    public CssValue<BackdropBlurBuilder>? BackdropBlur
    {
        get => _effectUtilities?.BackdropBlur;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropBlur = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropBlur = null;
        }
    }

    [Parameter]
    public CssValue<BackdropBrightnessBuilder>? BackdropBrightness
    {
        get => _effectUtilities?.BackdropBrightness;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropBrightness = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropBrightness = null;
        }
    }

    [Parameter]
    public CssValue<BackdropContrastBuilder>? BackdropContrast
    {
        get => _effectUtilities?.BackdropContrast;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropContrast = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropContrast = null;
        }
    }

    [Parameter]
    public CssValue<BackdropGrayscaleBuilder>? BackdropGrayscale
    {
        get => _effectUtilities?.BackdropGrayscale;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropGrayscale = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropGrayscale = null;
        }
    }

    [Parameter]
    public CssValue<BackdropHueRotateBuilder>? BackdropHueRotate
    {
        get => _effectUtilities?.BackdropHueRotate;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropHueRotate = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropHueRotate = null;
        }
    }

    [Parameter]
    public CssValue<BackdropInvertBuilder>? BackdropInvert
    {
        get => _effectUtilities?.BackdropInvert;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropInvert = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropInvert = null;
        }
    }

    [Parameter]
    public CssValue<BackdropOpacityBuilder>? BackdropOpacity
    {
        get => _effectUtilities?.BackdropOpacity;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropOpacity = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropOpacity = null;
        }
    }

    [Parameter]
    public CssValue<BackdropSaturateBuilder>? BackdropSaturate
    {
        get => _effectUtilities?.BackdropSaturate;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropSaturate = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropSaturate = null;
        }
    }

    [Parameter]
    public CssValue<BackdropSepiaBuilder>? BackdropSepia
    {
        get => _effectUtilities?.BackdropSepia;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).BackdropSepia = value;
            else if (_effectUtilities is not null) _effectUtilities.BackdropSepia = null;
        }
    }

    [Parameter]
    public CssValue<FilterBuilder>? Filter
    {
        get => _effectUtilities?.Filter;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).Filter = value;
            else if (_effectUtilities is not null) _effectUtilities.Filter = null;
        }
    }

    [Parameter]
    public CssValue<BlurBuilder>? Blur
    {
        get => _effectUtilities?.Blur;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).Blur = value;
            else if (_effectUtilities is not null) _effectUtilities.Blur = null;
        }
    }

    [Parameter]
    public CssValue<BrightnessBuilder>? Brightness
    {
        get => _effectUtilities?.Brightness;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).Brightness = value;
            else if (_effectUtilities is not null) _effectUtilities.Brightness = null;
        }
    }

    [Parameter]
    public CssValue<ContrastBuilder>? Contrast
    {
        get => _effectUtilities?.Contrast;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).Contrast = value;
            else if (_effectUtilities is not null) _effectUtilities.Contrast = null;
        }
    }

    [Parameter]
    public CssValue<DropShadowBuilder>? DropShadow
    {
        get => _effectUtilities?.DropShadow;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).DropShadow = value;
            else if (_effectUtilities is not null) _effectUtilities.DropShadow = null;
        }
    }

    [Parameter]
    public CssValue<DropShadowColorBuilder>? DropShadowColor
    {
        get => _effectUtilities?.DropShadowColor;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).DropShadowColor = value;
            else if (_effectUtilities is not null) _effectUtilities.DropShadowColor = null;
        }
    }

    [Parameter]
    public CssValue<GrayscaleBuilder>? Grayscale
    {
        get => _effectUtilities?.Grayscale;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).Grayscale = value;
            else if (_effectUtilities is not null) _effectUtilities.Grayscale = null;
        }
    }

    [Parameter]
    public CssValue<HueRotateBuilder>? HueRotate
    {
        get => _effectUtilities?.HueRotate;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).HueRotate = value;
            else if (_effectUtilities is not null) _effectUtilities.HueRotate = null;
        }
    }

    [Parameter]
    public CssValue<InvertBuilder>? Invert
    {
        get => _effectUtilities?.Invert;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).Invert = value;
            else if (_effectUtilities is not null) _effectUtilities.Invert = null;
        }
    }

    [Parameter]
    public CssValue<SaturateBuilder>? Saturate
    {
        get => _effectUtilities?.Saturate;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).Saturate = value;
            else if (_effectUtilities is not null) _effectUtilities.Saturate = null;
        }
    }

    [Parameter]
    public CssValue<SepiaBuilder>? Sepia
    {
        get => _effectUtilities?.Sepia;
        set
        {
            if (value.HasValue) (_effectUtilities ??= new EffectUtilities()).Sepia = value;
            else if (_effectUtilities is not null) _effectUtilities.Sepia = null;
        }
    }

    [Parameter]
    public CssValue<ResizeBuilder>? Resize
    {
        get => _advancedLayoutUtilities?.Resize;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).Resize = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.Resize = null;
        }
    }

    [Parameter]
    public CssValue<TransformBuilder>? Transform
    {
        get => _advancedLayoutUtilities?.Transform;
        set
        {
            if (value.HasValue) (_advancedLayoutUtilities ??= new AdvancedLayoutUtilities()).Transform = value;
            else if (_advancedLayoutUtilities is not null) _advancedLayoutUtilities.Transform = null;
        }
    }

    [Parameter]
    public CssValue<AnimationBuilder>? Animation
    {
        get => _motionUtilities?.Animation;
        set
        {
            if (value.HasValue) (_motionUtilities ??= new MotionUtilities()).Animation = value;
            else if (_motionUtilities is not null) _motionUtilities.Animation = null;
        }
    }

    [Parameter]
    public CssValue<DurationBuilder>? Duration
    {
        get => _motionUtilities?.Duration;
        set
        {
            if (value.HasValue) (_motionUtilities ??= new MotionUtilities()).Duration = value;
            else if (_motionUtilities is not null) _motionUtilities.Duration = null;
        }
    }

    [Parameter]
    public CssValue<TransitionBuilder>? Transition
    {
        get => _motionUtilities?.Transition;
        set
        {
            if (value.HasValue) (_motionUtilities ??= new MotionUtilities()).Transition = value;
            else if (_motionUtilities is not null) _motionUtilities.Transition = null;
        }
    }

    [Parameter]
    public EventCallback<ElementReference> OnElementRefReady { get; set; }

    protected ElementReference ElementRef { get; set; }

    /// <summary>
    /// Gets the preset context already applied during the current attribute build.
    /// </summary>
    protected QuarkPresetContext? AppliedPresetContext { get; private set; }

    protected override bool AlwaysRender => QuarkOptions.AlwaysRender;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        var resolvedPreset = Preset;
        var resolvedPresets = Presets;

        bool foundPreset = false;
        bool foundPresets = false;
        foreach (var parameter in parameters)
        {
            if (!foundPreset && parameter.Name == nameof(Preset))
            {
                resolvedPreset = (QuarkPresetToken?)parameter.Value;
                foundPreset = true;
            }
            else if (!foundPresets && parameter.Name == nameof(Presets))
            {
                resolvedPresets = (IReadOnlyList<QuarkPresetToken>?)parameter.Value;
                foundPresets = true;
            }

            if (foundPreset && foundPresets)
                break;
        }

        // Explicit-parameter tracking is only needed while presets are active. Avoid a second
        // ParameterView enumeration and HashSet work for the overwhelmingly common no-preset path.
        if (resolvedPreset is null && (resolvedPresets is null || resolvedPresets.Count == 0))
        {
            _explicitParameters = default;
            return base.SetParametersAsync(parameters);
        }

        if (resolvedPreset is { IsFrozen: false }) InvalidateRender();
        if (resolvedPresets is not null)
            foreach (var preset in resolvedPresets)
                if (!preset.IsFrozen) { InvalidateRender(); break; }

        _explicitParameters = default;

        foreach (var parameter in parameters)
        {
            int slot = GetPresetParameterSlot(parameter.Name);
            if (slot >= 0)
            {
                _explicitParameters[slot / 64] |= 1UL << (slot % 64);
            }
        }

        return base.SetParametersAsync(parameters);
    }

    protected bool HasExplicitParameter(string parameterName)
    {
        int slot = GetPresetParameterSlot(parameterName);
        return HasExplicitPresetSlot(slot);
    }

    private bool HasExplicitPresetSlot(int slot) => slot >= 0 && (_explicitParameters[slot / 64] & (1UL << (slot % 64))) != 0;

    private static int GetPresetParameterSlot(string name) => name switch
    {
        nameof(OutlineStyle) => (int)PresetProperty.OutlineStyle,
        nameof(PresetProperty.AccentColor) => (int)PresetProperty.AccentColor,
        nameof(PresetProperty.NativeAppearance) => (int)PresetProperty.NativeAppearance,
        nameof(AspectRatio) => (int)PresetProperty.AspectRatio,
        nameof(PresetProperty.AutoCols) => (int)PresetProperty.AutoCols,
        nameof(PresetProperty.AutoRows) => (int)PresetProperty.AutoRows,
        nameof(BackfaceVisibility) => (int)PresetProperty.BackfaceVisibility,
        nameof(BackgroundAttachment) => (int)PresetProperty.BackgroundAttachment,
        nameof(BackgroundBlendMode) => (int)PresetProperty.BackgroundBlendMode,
        nameof(BackgroundClip) => (int)PresetProperty.BackgroundClip,
        nameof(BackgroundImage) => (int)PresetProperty.BackgroundImage,
        nameof(BackgroundOrigin) => (int)PresetProperty.BackgroundOrigin,
        nameof(BackgroundPosition) => (int)PresetProperty.BackgroundPosition,
        nameof(BackgroundRepeat) => (int)PresetProperty.BackgroundRepeat,
        nameof(BackgroundSize) => (int)PresetProperty.BackgroundSize,
        nameof(BlockSize) => (int)PresetProperty.BlockSize,
        nameof(PresetProperty.BorderCollapse) => (int)PresetProperty.BorderCollapse,
        nameof(PresetProperty.BorderSpacing) => (int)PresetProperty.BorderSpacing,
        nameof(BoxDecorationBreak) => (int)PresetProperty.BoxDecorationBreak,
        nameof(BoxSizing) => (int)PresetProperty.BoxSizing,
        nameof(BreakAfter) => (int)PresetProperty.BreakAfter,
        nameof(BreakBefore) => (int)PresetProperty.BreakBefore,
        nameof(BreakInside) => (int)PresetProperty.BreakInside,
        nameof(PresetProperty.CaptionSide) => (int)PresetProperty.CaptionSide,
        nameof(PresetProperty.CaretColor) => (int)PresetProperty.CaretColor,
        nameof(FloatClear) => (int)PresetProperty.FloatClear,
        nameof(ClipPath) => (int)PresetProperty.ClipPath,
        nameof(ColEnd) => (int)PresetProperty.ColEnd,
        nameof(ColorScheme) => (int)PresetProperty.ColorScheme,
        nameof(ColumnCount) => (int)PresetProperty.ColumnCount,
        nameof(ContainerQuery) => (int)PresetProperty.ContainerQuery,
        nameof(Contain) => (int)PresetProperty.Contain,
        nameof(GeneratedContent) => (int)PresetProperty.GeneratedContent,
        nameof(DecorationColor) => (int)PresetProperty.DecorationColor,
        nameof(DecorationStyle) => (int)PresetProperty.DecorationStyle,
        nameof(DecorationThickness) => (int)PresetProperty.DecorationThickness,
        nameof(TransitionDelay) => (int)PresetProperty.TransitionDelay,
        nameof(Ease) => (int)PresetProperty.Ease,
        nameof(PresetProperty.FieldSizing) => (int)PresetProperty.FieldSizing,
        nameof(PresetProperty.FillRule) => (int)PresetProperty.FillRule,
        nameof(PresetProperty.Fill) => (int)PresetProperty.Fill,
        nameof(FlexBasis) => (int)PresetProperty.FlexBasis,
        nameof(FontFeatureSettings) => (int)PresetProperty.FontFeatureSettings,
        nameof(FontSmoothing) => (int)PresetProperty.FontSmoothing,
        nameof(FontStretch) => (int)PresetProperty.FontStretch,
        nameof(ForcedColorAdjust) => (int)PresetProperty.ForcedColorAdjust,
        nameof(BackgroundGradient) => (int)PresetProperty.BackgroundGradient,
        nameof(PresetProperty.GridAutoFlow) => (int)PresetProperty.GridAutoFlow,
        nameof(PresetProperty.GridColumns) => (int)PresetProperty.GridColumns,
        nameof(PresetProperty.GridRows) => (int)PresetProperty.GridRows,
        nameof(ColumnSpan) => (int)PresetProperty.ColumnSpan,
        nameof(Hyphen) => (int)PresetProperty.Hyphen,
        nameof(InlineSize) => (int)PresetProperty.InlineSize,
        nameof(InsetBlockEnd) => (int)PresetProperty.InsetBlockEnd,
        nameof(InsetBlockStart) => (int)PresetProperty.InsetBlockStart,
        nameof(InsetEnd) => (int)PresetProperty.InsetEnd,
        nameof(InsetRingColor) => (int)PresetProperty.InsetRingColor,
        nameof(InsetRing) => (int)PresetProperty.InsetRing,
        nameof(InsetShadowColor) => (int)PresetProperty.InsetShadowColor,
        nameof(InsetShadow) => (int)PresetProperty.InsetShadow,
        nameof(InsetStart) => (int)PresetProperty.InsetStart,
        nameof(Isolation) => (int)PresetProperty.Isolation,
        nameof(PresetProperty.ListStyleImage) => (int)PresetProperty.ListStyleImage,
        nameof(PresetProperty.ListStylePosition) => (int)PresetProperty.ListStylePosition,
        nameof(PresetProperty.ListStyleType) => (int)PresetProperty.ListStyleType,
        nameof(MaskClip) => (int)PresetProperty.MaskClip,
        nameof(MaskComposite) => (int)PresetProperty.MaskComposite,
        nameof(MaskImage) => (int)PresetProperty.MaskImage,
        nameof(MaskMode) => (int)PresetProperty.MaskMode,
        nameof(MaskOrigin) => (int)PresetProperty.MaskOrigin,
        nameof(MaskPosition) => (int)PresetProperty.MaskPosition,
        nameof(MaskRepeat) => (int)PresetProperty.MaskRepeat,
        nameof(MaskSize) => (int)PresetProperty.MaskSize,
        nameof(MaskType) => (int)PresetProperty.MaskType,
        nameof(MaxBlockSize) => (int)PresetProperty.MaxBlockSize,
        nameof(MaxInlineSize) => (int)PresetProperty.MaxInlineSize,
        nameof(MinBlockSize) => (int)PresetProperty.MinBlockSize,
        nameof(MinInlineSize) => (int)PresetProperty.MinInlineSize,
        nameof(MixBlendMode) => (int)PresetProperty.MixBlendMode,
        nameof(PresetProperty.ObjectFit) => (int)PresetProperty.ObjectFit,
        nameof(PresetProperty.ObjectPosition) => (int)PresetProperty.ObjectPosition,
        nameof(Order) => (int)PresetProperty.Order,
        nameof(Origin) => (int)PresetProperty.Origin,
        nameof(OutlineColor) => (int)PresetProperty.OutlineColor,
        nameof(OutlineOffset) => (int)PresetProperty.OutlineOffset,
        nameof(OutlineWidth) => (int)PresetProperty.OutlineWidth,
        nameof(OverflowWrap) => (int)PresetProperty.OverflowWrap,
        nameof(PerspectiveOrigin) => (int)PresetProperty.PerspectiveOrigin,
        nameof(Perspective) => (int)PresetProperty.Perspective,
        nameof(PlaceContentAlign) => (int)PresetProperty.PlaceContentAlign,
        nameof(PlaceItemsAlign) => (int)PresetProperty.PlaceItemsAlign,
        nameof(PlaceSelfAlign) => (int)PresetProperty.PlaceSelfAlign,
        nameof(Rotate) => (int)PresetProperty.Rotate,
        nameof(RowEnd) => (int)PresetProperty.RowEnd,
        nameof(Scale) => (int)PresetProperty.Scale,
        nameof(ScrollbarGutter) => (int)PresetProperty.ScrollbarGutter,
        nameof(ScrollbarThumbColor) => (int)PresetProperty.ScrollbarThumbColor,
        nameof(ScrollbarTrackColor) => (int)PresetProperty.ScrollbarTrackColor,
        nameof(ScrollbarWidth) => (int)PresetProperty.ScrollbarWidth,
        nameof(ScrollBehavior) => (int)PresetProperty.ScrollBehavior,
        nameof(ScrollSnapAlign) => (int)PresetProperty.ScrollSnapAlign,
        nameof(ScrollSnap) => (int)PresetProperty.ScrollSnap,
        nameof(ScrollSnapStop) => (int)PresetProperty.ScrollSnapStop,
        nameof(ShadowColor) => (int)PresetProperty.ShadowColor,
        nameof(Skew) => (int)PresetProperty.Skew,
        nameof(PresetProperty.StrokeLineCap) => (int)PresetProperty.StrokeLineCap,
        nameof(PresetProperty.StrokeLineJoin) => (int)PresetProperty.StrokeLineJoin,
        nameof(PresetProperty.Stroke) => (int)PresetProperty.Stroke,
        nameof(PresetProperty.SvgStrokeWidth) => (int)PresetProperty.SvgStrokeWidth,
        nameof(PresetProperty.TableLayout) => (int)PresetProperty.TableLayout,
        nameof(TabSize) => (int)PresetProperty.TabSize,
        nameof(TextIndent) => (int)PresetProperty.TextIndent,
        nameof(TextShadowColor) => (int)PresetProperty.TextShadowColor,
        nameof(TextShadow) => (int)PresetProperty.TextShadow,
        nameof(TouchAction) => (int)PresetProperty.TouchAction,
        nameof(TransformStyle) => (int)PresetProperty.TransformStyle,
        nameof(TransitionBehavior) => (int)PresetProperty.TransitionBehavior,
        nameof(Translate) => (int)PresetProperty.Translate,
        nameof(WillChange) => (int)PresetProperty.WillChange,
        nameof(Zoom) => (int)PresetProperty.Zoom,
        nameof(Inset) => (int)PresetProperty.Inset,
        nameof(Top) => (int)PresetProperty.Top,
        nameof(Right) => (int)PresetProperty.Right,
        nameof(Bottom) => (int)PresetProperty.Bottom,
        nameof(Left) => (int)PresetProperty.Left,
        nameof(Display) => (int)PresetProperty.Display,
        nameof(Visibility) => (int)PresetProperty.Visibility,
        nameof(Float) => (int)PresetProperty.Float,
        nameof(VerticalAlign) => (int)PresetProperty.VerticalAlign,
        nameof(TextAlign) => (int)PresetProperty.TextAlign,
        nameof(TextColor) => (int)PresetProperty.TextColor,
        nameof(TextSize) => (int)PresetProperty.TextSize,
        nameof(DecorationLine) => (int)PresetProperty.DecorationLine,
        nameof(UnderlineOffset) => (int)PresetProperty.UnderlineOffset,
        nameof(TextTransform) => (int)PresetProperty.TextTransform,
        nameof(FontFamily) => (int)PresetProperty.FontFamily,
        nameof(FontWeight) => (int)PresetProperty.FontWeight,
        nameof(FontStyle) => (int)PresetProperty.FontStyle,
        nameof(Leading) => (int)PresetProperty.Leading,
        nameof(Tracking) => (int)PresetProperty.Tracking,
        nameof(Whitespace) => (int)PresetProperty.Whitespace,
        nameof(TextWrap) => (int)PresetProperty.TextWrap,
        nameof(WordBreak) => (int)PresetProperty.WordBreak,
        nameof(TextOverflow) => (int)PresetProperty.TextOverflow,
        nameof(Truncate) => (int)PresetProperty.Truncate,
        nameof(LineClamp) => (int)PresetProperty.LineClamp,
        nameof(FontVariantNumeric) => (int)PresetProperty.FontVariantNumeric,
        nameof(Margin) => (int)PresetProperty.Margin,
        nameof(Padding) => (int)PresetProperty.Padding,
        nameof(Position) => (int)PresetProperty.Position,
        nameof(ScrollMargin) => (int)PresetProperty.ScrollMargin,
        nameof(ScrollPadding) => (int)PresetProperty.ScrollPadding,
        nameof(Size) => (int)PresetProperty.Size,
        nameof(Width) => (int)PresetProperty.Width,
        nameof(MinWidth) => (int)PresetProperty.MinWidth,
        nameof(MaxWidth) => (int)PresetProperty.MaxWidth,
        nameof(Height) => (int)PresetProperty.Height,
        nameof(MinHeight) => (int)PresetProperty.MinHeight,
        nameof(MaxHeight) => (int)PresetProperty.MaxHeight,
        nameof(Overflow) => (int)PresetProperty.Overflow,
        nameof(OverflowX) => (int)PresetProperty.OverflowX,
        nameof(OverflowY) => (int)PresetProperty.OverflowY,
        nameof(Overscroll) => (int)PresetProperty.Overscroll,
        nameof(Flex) => (int)PresetProperty.Flex,
        nameof(FlexDirection) => (int)PresetProperty.FlexDirection,
        nameof(FlexWrap) => (int)PresetProperty.FlexWrap,
        nameof(Grow) => (int)PresetProperty.Grow,
        nameof(Shrink) => (int)PresetProperty.Shrink,
        nameof(Gap) => (int)PresetProperty.Gap,
        nameof(Space) => (int)PresetProperty.Space,
        nameof(Divide) => (int)PresetProperty.Divide,
        nameof(ContentAlign) => (int)PresetProperty.ContentAlign,
        nameof(ItemsAlign) => (int)PresetProperty.ItemsAlign,
        nameof(Justify) => (int)PresetProperty.Justify,
        nameof(SelfAlign) => (int)PresetProperty.SelfAlign,
        nameof(JustifyItemsAlign) => (int)PresetProperty.JustifyItemsAlign,
        nameof(JustifySelfAlign) => (int)PresetProperty.JustifySelfAlign,
        nameof(ColStart) => (int)PresetProperty.ColStart,
        nameof(RowSpan) => (int)PresetProperty.RowSpan,
        nameof(RowStart) => (int)PresetProperty.RowStart,
        nameof(Opacity) => (int)PresetProperty.Opacity,
        nameof(ZIndex) => (int)PresetProperty.ZIndex,
        nameof(PointerEvents) => (int)PresetProperty.PointerEvents,
        nameof(UserSelect) => (int)PresetProperty.UserSelect,
        nameof(Cursor) => (int)PresetProperty.Cursor,
        nameof(ScreenReader) => (int)PresetProperty.ScreenReader,
        nameof(BackgroundColor) => (int)PresetProperty.BackgroundColor,
        nameof(BorderColor) => (int)PresetProperty.BorderColor,
        nameof(Border) => (int)PresetProperty.Border,
        nameof(BorderStyle) => (int)PresetProperty.BorderStyle,
        nameof(Rounded) => (int)PresetProperty.Rounded,
        nameof(RingColor) => (int)PresetProperty.RingColor,
        nameof(Ring) => (int)PresetProperty.Ring,
        nameof(RingOffset) => (int)PresetProperty.RingOffset,
        nameof(Shadow) => (int)PresetProperty.Shadow,
        nameof(BackdropFilter) => (int)PresetProperty.BackdropFilter,
        nameof(BackdropBlur) => (int)PresetProperty.BackdropBlur,
        nameof(BackdropBrightness) => (int)PresetProperty.BackdropBrightness,
        nameof(BackdropContrast) => (int)PresetProperty.BackdropContrast,
        nameof(BackdropGrayscale) => (int)PresetProperty.BackdropGrayscale,
        nameof(BackdropHueRotate) => (int)PresetProperty.BackdropHueRotate,
        nameof(BackdropInvert) => (int)PresetProperty.BackdropInvert,
        nameof(BackdropOpacity) => (int)PresetProperty.BackdropOpacity,
        nameof(BackdropSaturate) => (int)PresetProperty.BackdropSaturate,
        nameof(BackdropSepia) => (int)PresetProperty.BackdropSepia,
        nameof(Filter) => (int)PresetProperty.Filter,
        nameof(Blur) => (int)PresetProperty.Blur,
        nameof(Brightness) => (int)PresetProperty.Brightness,
        nameof(Contrast) => (int)PresetProperty.Contrast,
        nameof(DropShadow) => (int)PresetProperty.DropShadow,
        nameof(DropShadowColor) => (int)PresetProperty.DropShadowColor,
        nameof(Grayscale) => (int)PresetProperty.Grayscale,
        nameof(HueRotate) => (int)PresetProperty.HueRotate,
        nameof(Invert) => (int)PresetProperty.Invert,
        nameof(Saturate) => (int)PresetProperty.Saturate,
        nameof(Sepia) => (int)PresetProperty.Sepia,
        nameof(Resize) => (int)PresetProperty.Resize,
        nameof(Transform) => (int)PresetProperty.Transform,
        nameof(Animation) => (int)PresetProperty.Animation,
        nameof(Duration) => (int)PresetProperty.Duration,
        nameof(Transition) => (int)PresetProperty.Transition,
        nameof(Class) => (int)PresetProperty.Class,
        nameof(Style) => (int)PresetProperty.Style,
        _ => -1
    };

    protected CssValue<T>? ResolvePresetValue<T>(CssValue<T>? value, CssValue<T>? presetValue, string parameterName) where T : class, ICssBuilder =>
        ResolvePresetSlot(value, presetValue, GetPresetParameterSlot(parameterName));

    private CssValue<T>? ResolvePresetSlot<T>(CssValue<T>? value, CssValue<T>? presetValue, int slot) where T : class, ICssBuilder =>
        presetValue is null || HasExplicitPresetSlot(slot) ? value : presetValue;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private bool TryAppendPresetClass(ref PooledStringBuilder classes, QuarkPresetContext? preset, PresetProperty property)
    {
        // Presets already contain rendered CSS. Preserve explicit-null suppression without
        // constructing a nullable CssValue and converting it back to the same string.
        if (preset is null || preset.GetClass(property) is not { } value || HasExplicitPresetSlot((int)property))
            return false;

        AppendClass(ref classes, value);
        return true;
    }

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);

        if (Title.HasContent())
            attrs["title"] = Title!;

        if (Hidden)
            attrs["hidden"] = QuarkAttributeValues.True;

    }

    protected override void BuildFinalAttributes(Dictionary<string, object> attrs)
    {
        base.BuildFinalAttributes(attrs);

        if (!QuarkOptions.RespectReducedMotion)
            attrs["data-quark-motion"] = "always";

        if (DataSlot.HasContent() && !HasExplicitDataSlotAttribute())
        {
            attrs["data-slot"] = DataSlot!;
            return;
        }

    }

    protected virtual void BuildDefaultClasses(ref PooledStringBuilder cls) { }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);
        var preset = BuildPresetContext();
        AppliedPresetContext = preset;
        BuildDefaultClasses(ref cls);

        BuildTypographyClassAndStyle(ref sty, ref cls, preset);
        BuildLayoutClassAndStyle(ref sty, ref cls, preset);
        BuildInteractionClassAndStyle(ref sty, ref cls, preset);
        BuildVisualClassAndStyle(ref sty, ref cls, preset);
        BuildAdditionalUtilities1(ref cls, preset);
        BuildAdditionalUtilities2(ref cls, preset);
        BuildAdditionalUtilities3(ref cls, preset);
        BuildAdditionalUtilities4(ref cls, preset);
        BuildAdditionalUtilities5(ref cls, preset);
        BuildAdditionalUtilities6(ref cls, preset);
        if (_advancedTypographyUtilities is not null || preset is not null)
            BuildAdvancedTypographyUtilities(ref cls, preset);
        if (_transform3DUtilities is not null || preset is not null)
            BuildTransform3DUtilities(ref cls, preset);
        if (_maskUtilities is not null || preset is not null)
            BuildMaskUtilities(ref cls, preset);

        // Explicit inline CSS follows builder-provided styles; the browser resolves precedence.
        if (Style.HasContent())
            AppendStyleDecl(ref sty, Style!);

        if (Container)
            AppendClass(ref cls, "container");

        if (preset?.Class.HasContent() == true)
            AppendClass(ref cls, preset.Class!);

        if (Class.HasContent())
            AppendClass(ref cls, Class!);
    }

    // Test storage fields directly before fetching a CssValue. The public getters return a
    // large nullable struct, which need not be copied just to check whether a slot is populated.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities1(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_dimensionUtilities is not null && _dimensionUtilities.AspectRatio.HasValue || preset is not null && preset.HasValue(PresetProperty.AspectRatio))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.AspectRatio))
                AddCss(ref cls, AspectRatio);
        }
        if (_backgroundUtilities is not null || preset is not null)
        {
            if (_backgroundUtilities is not null && _backgroundUtilities.BackgroundAttachment.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundAttachment))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackgroundAttachment))
                    AddCss(ref cls, BackgroundAttachment);
            }
            if (_backgroundUtilities is not null && _backgroundUtilities.BackgroundBlendMode.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundBlendMode))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackgroundBlendMode))
                    AddCss(ref cls, BackgroundBlendMode);
            }
            if (_backgroundUtilities is not null && _backgroundUtilities.BackgroundClip.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundClip))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackgroundClip))
                    AddCss(ref cls, BackgroundClip);
            }
            if (_backgroundUtilities is not null && _backgroundUtilities.BackgroundImage.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundImage))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackgroundImage))
                    AddCss(ref cls, BackgroundImage);
            }
            if (_backgroundUtilities is not null && _backgroundUtilities.BackgroundOrigin.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundOrigin))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackgroundOrigin))
                    AddCss(ref cls, BackgroundOrigin);
            }
            if (_backgroundUtilities is not null && _backgroundUtilities.BackgroundPosition.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundPosition))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackgroundPosition))
                    AddCss(ref cls, BackgroundPosition);
            }
            if (_backgroundUtilities is not null && _backgroundUtilities.BackgroundRepeat.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundRepeat))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackgroundRepeat))
                    AddCss(ref cls, BackgroundRepeat);
            }
            if (_backgroundUtilities is not null && _backgroundUtilities.BackgroundSize.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundSize))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackgroundSize))
                    AddCss(ref cls, BackgroundSize);
            }
        }
        if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.BlockSize.HasValue || preset is not null && preset.HasValue(PresetProperty.BlockSize))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BlockSize))
                AddCss(ref cls, BlockSize);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.BoxDecorationBreak.HasValue || preset is not null && preset.HasValue(PresetProperty.BoxDecorationBreak))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BoxDecorationBreak))
                AddCss(ref cls, BoxDecorationBreak);
        }
        if (_displayUtilities is not null && _displayUtilities.BoxSizing.HasValue || preset is not null && preset.HasValue(PresetProperty.BoxSizing))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BoxSizing))
                AddCss(ref cls, BoxSizing);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.BreakAfter.HasValue || preset is not null && preset.HasValue(PresetProperty.BreakAfter))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BreakAfter))
                AddCss(ref cls, BreakAfter);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities2(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.BreakBefore.HasValue || preset is not null && preset.HasValue(PresetProperty.BreakBefore))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BreakBefore))
                AddCss(ref cls, BreakBefore);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.BreakInside.HasValue || preset is not null && preset.HasValue(PresetProperty.BreakInside))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BreakInside))
                AddCss(ref cls, BreakInside);
        }
        if (_displayUtilities is not null && _displayUtilities.FloatClear.HasValue || preset is not null && preset.HasValue(PresetProperty.FloatClear))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FloatClear))
                AddCss(ref cls, FloatClear);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ClipPath.HasValue || preset is not null && preset.HasValue(PresetProperty.ClipPath))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ClipPath))
                AddCss(ref cls, ClipPath);
        }
        if (_gridPlacementUtilities is not null && _gridPlacementUtilities.ColEnd.HasValue || preset is not null && preset.HasValue(PresetProperty.ColEnd))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ColEnd))
                AddCss(ref cls, ColEnd);
        }
        if (_interactionUtilities is not null && _interactionUtilities.ColorScheme.HasValue || preset is not null && preset.HasValue(PresetProperty.ColorScheme))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ColorScheme))
                AddCss(ref cls, ColorScheme);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ColumnCount.HasValue || preset is not null && preset.HasValue(PresetProperty.ColumnCount))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ColumnCount))
                AddCss(ref cls, ColumnCount);
        }
        if (_displayUtilities is not null && _displayUtilities.ContainerQuery.HasValue || preset is not null && preset.HasValue(PresetProperty.ContainerQuery))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ContainerQuery))
                AddCss(ref cls, ContainerQuery);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.Contain.HasValue || preset is not null && preset.HasValue(PresetProperty.Contain))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Contain))
                AddCss(ref cls, Contain);
        }
        if (_textDecorationUtilities is not null || preset is not null)
        {
            if (_textDecorationUtilities is not null && _textDecorationUtilities.GeneratedContent.HasValue || preset is not null && preset.HasValue(PresetProperty.GeneratedContent))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.GeneratedContent))
                    AddCss(ref cls, GeneratedContent);
            }
            if (_textDecorationUtilities is not null && _textDecorationUtilities.DecorationColor.HasValue || preset is not null && preset.HasValue(PresetProperty.DecorationColor))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.DecorationColor))
                    AddCss(ref cls, DecorationColor);
            }
            if (_textDecorationUtilities is not null && _textDecorationUtilities.DecorationStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.DecorationStyle))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.DecorationStyle))
                    AddCss(ref cls, DecorationStyle);
            }
            if (_textDecorationUtilities is not null && _textDecorationUtilities.DecorationThickness.HasValue || preset is not null && preset.HasValue(PresetProperty.DecorationThickness))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.DecorationThickness))
                    AddCss(ref cls, DecorationThickness);
            }
        }
        if (_motionUtilities is not null && _motionUtilities.TransitionDelay.HasValue || preset is not null && preset.HasValue(PresetProperty.TransitionDelay))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TransitionDelay))
                AddCss(ref cls, TransitionDelay);
        }
        if (_motionUtilities is not null && _motionUtilities.Ease.HasValue || preset is not null && preset.HasValue(PresetProperty.Ease))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Ease))
                AddCss(ref cls, Ease);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities3(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_flexUtilities is not null && _flexUtilities.FlexBasis.HasValue || preset is not null && preset.HasValue(PresetProperty.FlexBasis))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FlexBasis))
                AddCss(ref cls, FlexBasis);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ForcedColorAdjust.HasValue || preset is not null && preset.HasValue(PresetProperty.ForcedColorAdjust))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ForcedColorAdjust))
                AddCss(ref cls, ForcedColorAdjust);
        }
        if (_backgroundUtilities is not null && _backgroundUtilities.BackgroundGradient.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundGradient))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackgroundGradient))
                AddCss(ref cls, BackgroundGradient);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ColumnSpan.HasValue || preset is not null && preset.HasValue(PresetProperty.ColumnSpan))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ColumnSpan))
                AddCss(ref cls, ColumnSpan);
        }
        if (_textFlowUtilities is not null && _textFlowUtilities.Hyphen.HasValue || preset is not null && preset.HasValue(PresetProperty.Hyphen))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Hyphen))
                AddCss(ref cls, Hyphen);
        }
        if (_logicalLayoutUtilities is not null || preset is not null)
        {
            if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.InlineSize.HasValue || preset is not null && preset.HasValue(PresetProperty.InlineSize))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.InlineSize))
                    AddCss(ref cls, InlineSize);
            }
            if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.InsetBlockEnd.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetBlockEnd))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.InsetBlockEnd))
                    AddCss(ref cls, InsetBlockEnd);
            }
            if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.InsetBlockStart.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetBlockStart))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.InsetBlockStart))
                    AddCss(ref cls, InsetBlockStart);
            }
            if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.InsetEnd.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetEnd))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.InsetEnd))
                    AddCss(ref cls, InsetEnd);
            }
        }
        if (_effectUtilities is not null || preset is not null)
        {
            if (_effectUtilities is not null && _effectUtilities.InsetRingColor.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetRingColor))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.InsetRingColor))
                    AddCss(ref cls, InsetRingColor);
            }
            if (_effectUtilities is not null && _effectUtilities.InsetRing.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetRing))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.InsetRing))
                    AddCss(ref cls, InsetRing);
            }
            if (_effectUtilities is not null && _effectUtilities.InsetShadowColor.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetShadowColor))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.InsetShadowColor))
                    AddCss(ref cls, InsetShadowColor);
            }
            if (_effectUtilities is not null && _effectUtilities.InsetShadow.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetShadow))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.InsetShadow))
                    AddCss(ref cls, InsetShadow);
            }
        }
        if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.InsetStart.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetStart))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.InsetStart))
                AddCss(ref cls, InsetStart);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities4(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.Isolation.HasValue || preset is not null && preset.HasValue(PresetProperty.Isolation))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Isolation))
                AddCss(ref cls, Isolation);
        }
        if (_logicalLayoutUtilities is not null || preset is not null)
        {
            if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.MaxBlockSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MaxBlockSize))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaxBlockSize))
                    AddCss(ref cls, MaxBlockSize);
            }
            if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.MaxInlineSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MaxInlineSize))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaxInlineSize))
                    AddCss(ref cls, MaxInlineSize);
            }
            if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.MinBlockSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MinBlockSize))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MinBlockSize))
                    AddCss(ref cls, MinBlockSize);
            }
            if (_logicalLayoutUtilities is not null && _logicalLayoutUtilities.MinInlineSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MinInlineSize))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MinInlineSize))
                    AddCss(ref cls, MinInlineSize);
            }
        }
        if (_effectUtilities is not null && _effectUtilities.MixBlendMode.HasValue || preset is not null && preset.HasValue(PresetProperty.MixBlendMode))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MixBlendMode))
                AddCss(ref cls, MixBlendMode);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities5(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_flexUtilities is not null && _flexUtilities.Order.HasValue || preset is not null && preset.HasValue(PresetProperty.Order))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Order))
                AddCss(ref cls, Order);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.Origin.HasValue || preset is not null && preset.HasValue(PresetProperty.Origin))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Origin))
                AddCss(ref cls, Origin);
        }
        if (_effectUtilities is not null || preset is not null)
        {
            if (_effectUtilities is not null && _effectUtilities.OutlineColor.HasValue || preset is not null && preset.HasValue(PresetProperty.OutlineColor))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.OutlineColor))
                    AddCss(ref cls, OutlineColor);
            }
            if (_effectUtilities is not null && _effectUtilities.OutlineOffset.HasValue || preset is not null && preset.HasValue(PresetProperty.OutlineOffset))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.OutlineOffset))
                    AddCss(ref cls, OutlineOffset);
            }
            if (_effectUtilities is not null && _effectUtilities.OutlineWidth.HasValue || preset is not null && preset.HasValue(PresetProperty.OutlineWidth))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.OutlineWidth))
                    AddCss(ref cls, OutlineWidth);
            }
        }
        if (_textFlowUtilities is not null && _textFlowUtilities.OverflowWrap.HasValue || preset is not null && preset.HasValue(PresetProperty.OverflowWrap))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.OverflowWrap))
                AddCss(ref cls, OverflowWrap);
        }
        if (_alignmentUtilities is not null || preset is not null)
        {
            if (_alignmentUtilities is not null && _alignmentUtilities.PlaceContentAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.PlaceContentAlign))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.PlaceContentAlign))
                    AddCss(ref cls, PlaceContentAlign);
            }
            if (_alignmentUtilities is not null && _alignmentUtilities.PlaceItemsAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.PlaceItemsAlign))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.PlaceItemsAlign))
                    AddCss(ref cls, PlaceItemsAlign);
            }
            if (_alignmentUtilities is not null && _alignmentUtilities.PlaceSelfAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.PlaceSelfAlign))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.PlaceSelfAlign))
                    AddCss(ref cls, PlaceSelfAlign);
            }
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.Rotate.HasValue || preset is not null && preset.HasValue(PresetProperty.Rotate))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Rotate))
                AddCss(ref cls, Rotate);
        }
        if (_gridPlacementUtilities is not null && _gridPlacementUtilities.RowEnd.HasValue || preset is not null && preset.HasValue(PresetProperty.RowEnd))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.RowEnd))
                AddCss(ref cls, RowEnd);
        }
        if (_motionUtilities is not null && _motionUtilities.Scale.HasValue || preset is not null && preset.HasValue(PresetProperty.Scale))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Scale))
                AddCss(ref cls, Scale);
        }
        if (_advancedLayoutUtilities is not null || preset is not null)
        {
            if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollbarGutter.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollbarGutter))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollbarGutter))
                    AddCss(ref cls, ScrollbarGutter);
            }
            if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollbarThumbColor.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollbarThumbColor))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollbarThumbColor))
                    AddCss(ref cls, ScrollbarThumbColor);
            }
            if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollbarTrackColor.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollbarTrackColor))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollbarTrackColor))
                    AddCss(ref cls, ScrollbarTrackColor);
            }
            if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollbarWidth.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollbarWidth))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollbarWidth))
                    AddCss(ref cls, ScrollbarWidth);
            }
            if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollBehavior.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollBehavior))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollBehavior))
                    AddCss(ref cls, ScrollBehavior);
            }
            if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollSnapAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollSnapAlign))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollSnapAlign))
                    AddCss(ref cls, ScrollSnapAlign);
            }
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities6(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollSnap.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollSnap))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollSnap))
                AddCss(ref cls, ScrollSnap);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollSnapStop.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollSnapStop))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollSnapStop))
                AddCss(ref cls, ScrollSnapStop);
        }
        if (_surfaceUtilities is not null && _surfaceUtilities.ShadowColor.HasValue || preset is not null && preset.HasValue(PresetProperty.ShadowColor))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ShadowColor))
                AddCss(ref cls, ShadowColor);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.Skew.HasValue || preset is not null && preset.HasValue(PresetProperty.Skew))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Skew))
                AddCss(ref cls, Skew);
        }
        if (_textFlowUtilities is not null && _textFlowUtilities.TabSize.HasValue || preset is not null && preset.HasValue(PresetProperty.TabSize))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TabSize))
                AddCss(ref cls, TabSize);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.TouchAction.HasValue || preset is not null && preset.HasValue(PresetProperty.TouchAction))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TouchAction))
                AddCss(ref cls, TouchAction);
        }
        if (_motionUtilities is not null && _motionUtilities.TransitionBehavior.HasValue || preset is not null && preset.HasValue(PresetProperty.TransitionBehavior))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TransitionBehavior))
                AddCss(ref cls, TransitionBehavior);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.Translate.HasValue || preset is not null && preset.HasValue(PresetProperty.Translate))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Translate))
                AddCss(ref cls, Translate);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.WillChange.HasValue || preset is not null && preset.HasValue(PresetProperty.WillChange))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.WillChange))
                AddCss(ref cls, WillChange);
        }
        if (_motionUtilities is not null && _motionUtilities.Zoom.HasValue || preset is not null && preset.HasValue(PresetProperty.Zoom))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Zoom))
                AddCss(ref cls, Zoom);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildMaskUtilities(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_maskUtilities is not null && _maskUtilities.MaskClip.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskClip))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaskClip))
                AddCss(ref cls, MaskClip);
        }
        if (_maskUtilities is not null && _maskUtilities.MaskComposite.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskComposite))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaskComposite))
                AddCss(ref cls, MaskComposite);
        }
        if (_maskUtilities is not null && _maskUtilities.MaskImage.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskImage))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaskImage))
                AddCss(ref cls, MaskImage);
        }
        if (_maskUtilities is not null && _maskUtilities.MaskMode.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskMode))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaskMode))
                AddCss(ref cls, MaskMode);
        }
        if (_maskUtilities is not null && _maskUtilities.MaskOrigin.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskOrigin))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaskOrigin))
                AddCss(ref cls, MaskOrigin);
        }
        if (_maskUtilities is not null && _maskUtilities.MaskPosition.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskPosition))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaskPosition))
                AddCss(ref cls, MaskPosition);
        }
        if (_maskUtilities is not null && _maskUtilities.MaskRepeat.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskRepeat))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaskRepeat))
                AddCss(ref cls, MaskRepeat);
        }
        if (_maskUtilities is not null && _maskUtilities.MaskSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskSize))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaskSize))
                AddCss(ref cls, MaskSize);
        }
        if (_maskUtilities is not null && _maskUtilities.MaskType.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskType))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaskType))
                AddCss(ref cls, MaskType);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildTransform3DUtilities(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_transform3DUtilities is not null && _transform3DUtilities.BackfaceVisibility.HasValue || preset is not null && preset.HasValue(PresetProperty.BackfaceVisibility))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackfaceVisibility))
                AddCss(ref cls, BackfaceVisibility);
        }
        if (_transform3DUtilities is not null && _transform3DUtilities.PerspectiveOrigin.HasValue || preset is not null && preset.HasValue(PresetProperty.PerspectiveOrigin))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.PerspectiveOrigin))
                AddCss(ref cls, PerspectiveOrigin);
        }
        if (_transform3DUtilities is not null && _transform3DUtilities.Perspective.HasValue || preset is not null && preset.HasValue(PresetProperty.Perspective))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Perspective))
                AddCss(ref cls, Perspective);
        }
        if (_transform3DUtilities is not null && _transform3DUtilities.TransformStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.TransformStyle))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TransformStyle))
                AddCss(ref cls, TransformStyle);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdvancedTypographyUtilities(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_advancedTypographyUtilities is not null && _advancedTypographyUtilities.FontFeatureSettings.HasValue || preset is not null && preset.HasValue(PresetProperty.FontFeatureSettings))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FontFeatureSettings))
                AddCss(ref cls, FontFeatureSettings);
        }
        if (_advancedTypographyUtilities is not null && _advancedTypographyUtilities.FontSmoothing.HasValue || preset is not null && preset.HasValue(PresetProperty.FontSmoothing))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FontSmoothing))
                AddCss(ref cls, FontSmoothing);
        }
        if (_advancedTypographyUtilities is not null && _advancedTypographyUtilities.FontStretch.HasValue || preset is not null && preset.HasValue(PresetProperty.FontStretch))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FontStretch))
                AddCss(ref cls, FontStretch);
        }
        if (_advancedTypographyUtilities is not null && _advancedTypographyUtilities.TextIndent.HasValue || preset is not null && preset.HasValue(PresetProperty.TextIndent))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TextIndent))
                AddCss(ref cls, TextIndent);
        }
        if (_advancedTypographyUtilities is not null && _advancedTypographyUtilities.TextShadowColor.HasValue || preset is not null && preset.HasValue(PresetProperty.TextShadowColor))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TextShadowColor))
                AddCss(ref cls, TextShadowColor);
        }
        if (_advancedTypographyUtilities is not null && _advancedTypographyUtilities.TextShadow.HasValue || preset is not null && preset.HasValue(PresetProperty.TextShadow))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TextShadow))
                AddCss(ref cls, TextShadow);
        }
    }

    // Keep each group as a separate native frame. In particular, do not combine these methods
    // or allow them to inline: CssValue<T>? is a large nullable struct containing references,
    // and a single method covering every CSS property exceeds safe Mono AOT code/frame sizes.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildTypographyClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_displayUtilities is not null || preset is not null)
        {
            if (_displayUtilities is not null && _displayUtilities.Display.HasValue || preset is not null && preset.HasValue(PresetProperty.Display))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Display))
                    AddCss(ref cls, Display);
            }
            if (_displayUtilities is not null && _displayUtilities.Visibility.HasValue || preset is not null && preset.HasValue(PresetProperty.Visibility))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Visibility))
                    AddCss(ref cls, Visibility);
            }
            if (_displayUtilities is not null && _displayUtilities.Float.HasValue || preset is not null && preset.HasValue(PresetProperty.Float))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Float))
                    AddCss(ref cls, Float);
            }
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.VerticalAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.VerticalAlign))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.VerticalAlign))
                AddCss(ref cls, VerticalAlign);
        }
        if (_textFlowUtilities is not null && _textFlowUtilities.TextAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.TextAlign))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TextAlign))
                AddCss(ref cls, TextAlign);
        }
        ApplyTextColor(ref sty, ref cls, preset?.TextColor);
        if (_fontUtilities is not null && _fontUtilities.TextSize.HasValue || preset is not null && preset.HasValue(PresetProperty.TextSize))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TextSize))
                AddCss(ref cls, TextSize);
        }
        if (_textDecorationUtilities is not null && _textDecorationUtilities.DecorationLine.HasValue || preset is not null && preset.HasValue(PresetProperty.DecorationLine))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.DecorationLine))
                AddCss(ref cls, DecorationLine);
        }
        if (_textDecorationUtilities is not null && _textDecorationUtilities.UnderlineOffset.HasValue || preset is not null && preset.HasValue(PresetProperty.UnderlineOffset))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.UnderlineOffset))
                AddCss(ref cls, UnderlineOffset);
        }
        if (_textFlowUtilities is not null && _textFlowUtilities.TextTransform.HasValue || preset is not null && preset.HasValue(PresetProperty.TextTransform))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TextTransform))
                AddCss(ref cls, TextTransform);
        }
        if (_fontUtilities is not null || preset is not null)
        {
            if (_fontUtilities is not null && _fontUtilities.FontFamily.HasValue || preset is not null && preset.HasValue(PresetProperty.FontFamily))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FontFamily))
                    AddCss(ref cls, FontFamily);
            }
            if (_fontUtilities is not null && _fontUtilities.FontWeight.HasValue || preset is not null && preset.HasValue(PresetProperty.FontWeight))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FontWeight))
                    AddCss(ref cls, FontWeight);
            }
            if (_fontUtilities is not null && _fontUtilities.FontStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.FontStyle))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FontStyle))
                    AddCss(ref cls, FontStyle);
            }
            if (_fontUtilities is not null && _fontUtilities.Leading.HasValue || preset is not null && preset.HasValue(PresetProperty.Leading))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Leading))
                    AddCss(ref cls, Leading);
            }
            if (_fontUtilities is not null && _fontUtilities.Tracking.HasValue || preset is not null && preset.HasValue(PresetProperty.Tracking))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Tracking))
                    AddCss(ref cls, Tracking);
            }
        }
        if (_textFlowUtilities is not null || preset is not null)
        {
            if (_textFlowUtilities is not null && _textFlowUtilities.Whitespace.HasValue || preset is not null && preset.HasValue(PresetProperty.Whitespace))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Whitespace))
                    AddCss(ref cls, Whitespace);
            }
            if (_textFlowUtilities is not null && _textFlowUtilities.TextWrap.HasValue || preset is not null && preset.HasValue(PresetProperty.TextWrap))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TextWrap))
                    AddCss(ref cls, TextWrap);
            }
            if (_textFlowUtilities is not null && _textFlowUtilities.WordBreak.HasValue || preset is not null && preset.HasValue(PresetProperty.WordBreak))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.WordBreak))
                    AddCss(ref cls, WordBreak);
            }
            if (_textFlowUtilities is not null && _textFlowUtilities.TextOverflow.HasValue || preset is not null && preset.HasValue(PresetProperty.TextOverflow))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.TextOverflow))
                    AddCss(ref cls, TextOverflow);
            }
            if (_textFlowUtilities is not null && _textFlowUtilities.Truncate.HasValue || preset is not null && preset.HasValue(PresetProperty.Truncate))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Truncate))
                    AddCss(ref cls, Truncate);
            }
            if (_textFlowUtilities is not null && _textFlowUtilities.LineClamp.HasValue || preset is not null && preset.HasValue(PresetProperty.LineClamp))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.LineClamp))
                    AddCss(ref cls, LineClamp);
            }
        }
        if (_fontUtilities is not null && _fontUtilities.FontVariantNumeric.HasValue || preset is not null && preset.HasValue(PresetProperty.FontVariantNumeric))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FontVariantNumeric))
                AddCss(ref cls, FontVariantNumeric);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildLayoutClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_spacingUtilities is not null && _spacingUtilities.Margin.HasValue || preset is not null && preset.HasValue(PresetProperty.Margin))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Margin))
                AddCss(ref cls, Margin);
        }
        if (_spacingUtilities is not null && _spacingUtilities.Padding.HasValue || preset is not null && preset.HasValue(PresetProperty.Padding))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Padding))
                AddCss(ref cls, Padding);
        }
        if (_positionUtilities is not null || preset is not null)
        {
            if (_positionUtilities is not null && _positionUtilities.Inset.HasValue || preset is not null && preset.HasValue(PresetProperty.Inset))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Inset))
                    AddCss(ref cls, Inset);
            }
            if (_positionUtilities is not null && _positionUtilities.Top.HasValue || preset is not null && preset.HasValue(PresetProperty.Top))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Top))
                    AddCss(ref cls, Top);
            }
            if (_positionUtilities is not null && _positionUtilities.Right.HasValue || preset is not null && preset.HasValue(PresetProperty.Right))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Right))
                    AddCss(ref cls, Right);
            }
            if (_positionUtilities is not null && _positionUtilities.Bottom.HasValue || preset is not null && preset.HasValue(PresetProperty.Bottom))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Bottom))
                    AddCss(ref cls, Bottom);
            }
            if (_positionUtilities is not null && _positionUtilities.Left.HasValue || preset is not null && preset.HasValue(PresetProperty.Left))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Left))
                    AddCss(ref cls, Left);
            }
            if (_positionUtilities is not null && _positionUtilities.Position.HasValue || preset is not null && preset.HasValue(PresetProperty.Position))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Position))
                    AddCss(ref cls, Position);
            }
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollMargin.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollMargin))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollMargin))
                AddCss(ref cls, ScrollMargin);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.ScrollPadding.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollPadding))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScrollPadding))
                AddCss(ref cls, ScrollPadding);
        }
        if (_dimensionUtilities is not null || preset is not null)
        {
            if (_dimensionUtilities is not null && _dimensionUtilities.Size.HasValue || preset is not null && preset.HasValue(PresetProperty.Size))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Size))
                    AddCss(ref cls, Size);
            }
            if (_dimensionUtilities is not null && _dimensionUtilities.Width.HasValue || preset is not null && preset.HasValue(PresetProperty.Width))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Width))
                    AddCss(ref cls, Width);
            }
            if (_dimensionUtilities is not null && _dimensionUtilities.MinWidth.HasValue || preset is not null && preset.HasValue(PresetProperty.MinWidth))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MinWidth))
                    AddCss(ref cls, MinWidth);
            }
            if (_dimensionUtilities is not null && _dimensionUtilities.MaxWidth.HasValue || preset is not null && preset.HasValue(PresetProperty.MaxWidth))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaxWidth))
                    AddCss(ref cls, MaxWidth);
            }
            if (_dimensionUtilities is not null && _dimensionUtilities.Height.HasValue || preset is not null && preset.HasValue(PresetProperty.Height))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Height))
                    AddCss(ref cls, Height);
            }
            if (_dimensionUtilities is not null && _dimensionUtilities.MinHeight.HasValue || preset is not null && preset.HasValue(PresetProperty.MinHeight))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MinHeight))
                    AddCss(ref cls, MinHeight);
            }
            if (_dimensionUtilities is not null && _dimensionUtilities.MaxHeight.HasValue || preset is not null && preset.HasValue(PresetProperty.MaxHeight))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.MaxHeight))
                    AddCss(ref cls, MaxHeight);
            }
        }
        if (_displayUtilities is not null || preset is not null)
        {
            if (_displayUtilities is not null && _displayUtilities.Overflow.HasValue || preset is not null && preset.HasValue(PresetProperty.Overflow))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Overflow))
                    AddCss(ref cls, Overflow);
            }
            if (_displayUtilities is not null && _displayUtilities.OverflowX.HasValue || preset is not null && preset.HasValue(PresetProperty.OverflowX))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.OverflowX))
                    AddCss(ref cls, OverflowX);
            }
            if (_displayUtilities is not null && _displayUtilities.OverflowY.HasValue || preset is not null && preset.HasValue(PresetProperty.OverflowY))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.OverflowY))
                    AddCss(ref cls, OverflowY);
            }
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.Overscroll.HasValue || preset is not null && preset.HasValue(PresetProperty.Overscroll))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Overscroll))
                AddCss(ref cls, Overscroll);
        }
        if (_flexUtilities is not null || preset is not null)
        {
            if (_flexUtilities is not null && _flexUtilities.Flex.HasValue || preset is not null && preset.HasValue(PresetProperty.Flex))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Flex))
                    AddCss(ref cls, Flex);
            }
            if (_flexUtilities is not null && _flexUtilities.FlexDirection.HasValue || preset is not null && preset.HasValue(PresetProperty.FlexDirection))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FlexDirection))
                    AddCss(ref cls, FlexDirection);
            }
            if (_flexUtilities is not null && _flexUtilities.FlexWrap.HasValue || preset is not null && preset.HasValue(PresetProperty.FlexWrap))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.FlexWrap))
                    AddCss(ref cls, FlexWrap);
            }
            if (_flexUtilities is not null && _flexUtilities.Grow.HasValue || preset is not null && preset.HasValue(PresetProperty.Grow))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Grow))
                    AddCss(ref cls, Grow);
            }
            if (_flexUtilities is not null && _flexUtilities.Shrink.HasValue || preset is not null && preset.HasValue(PresetProperty.Shrink))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Shrink))
                    AddCss(ref cls, Shrink);
            }
        }
        if (_spacingUtilities is not null || preset is not null)
        {
            if (_spacingUtilities is not null && _spacingUtilities.Gap.HasValue || preset is not null && preset.HasValue(PresetProperty.Gap))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Gap))
                    AddCss(ref cls, Gap);
            }
            if (_spacingUtilities is not null && _spacingUtilities.Space.HasValue || preset is not null && preset.HasValue(PresetProperty.Space))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Space))
                    AddCss(ref cls, Space);
            }
            if (_spacingUtilities is not null && _spacingUtilities.Divide.HasValue || preset is not null && preset.HasValue(PresetProperty.Divide))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Divide))
                    AddCss(ref cls, Divide);
            }
        }
        if (_alignmentUtilities is not null || preset is not null)
        {
            if (_alignmentUtilities is not null && _alignmentUtilities.ContentAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.ContentAlign))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ContentAlign))
                    AddCss(ref cls, ContentAlign);
            }
            if (_alignmentUtilities is not null && _alignmentUtilities.ItemsAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.ItemsAlign))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ItemsAlign))
                    AddCss(ref cls, ItemsAlign);
            }
            if (_alignmentUtilities is not null && _alignmentUtilities.Justify.HasValue || preset is not null && preset.HasValue(PresetProperty.Justify))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Justify))
                    AddCss(ref cls, Justify);
            }
            if (_alignmentUtilities is not null && _alignmentUtilities.SelfAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.SelfAlign))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.SelfAlign))
                    AddCss(ref cls, SelfAlign);
            }
            if (_alignmentUtilities is not null && _alignmentUtilities.JustifyItemsAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.JustifyItemsAlign))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.JustifyItemsAlign))
                    AddCss(ref cls, JustifyItemsAlign);
            }
            if (_alignmentUtilities is not null && _alignmentUtilities.JustifySelfAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.JustifySelfAlign))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.JustifySelfAlign))
                    AddCss(ref cls, JustifySelfAlign);
            }
        }
        if (_gridPlacementUtilities is not null || preset is not null)
        {
            if (_gridPlacementUtilities is not null && _gridPlacementUtilities.ColStart.HasValue || preset is not null && preset.HasValue(PresetProperty.ColStart))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ColStart))
                    AddCss(ref cls, ColStart);
            }
            if (_gridPlacementUtilities is not null && _gridPlacementUtilities.RowSpan.HasValue || preset is not null && preset.HasValue(PresetProperty.RowSpan))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.RowSpan))
                    AddCss(ref cls, RowSpan);
            }
            if (_gridPlacementUtilities is not null && _gridPlacementUtilities.RowStart.HasValue || preset is not null && preset.HasValue(PresetProperty.RowStart))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.RowStart))
                    AddCss(ref cls, RowStart);
            }
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildInteractionClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_interactionUtilities is not null && _interactionUtilities.Opacity.HasValue || preset is not null && preset.HasValue(PresetProperty.Opacity))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Opacity))
                AddCss(ref cls, Opacity);
        }
        if (_positionUtilities is not null && _positionUtilities.ZIndex.HasValue || preset is not null && preset.HasValue(PresetProperty.ZIndex))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ZIndex))
                AddCss(ref cls, ZIndex);
        }
        if (_interactionUtilities is not null && _interactionUtilities.PointerEvents.HasValue || preset is not null && preset.HasValue(PresetProperty.PointerEvents))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.PointerEvents))
                AddCss(ref cls, PointerEvents);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.UserSelect.HasValue || preset is not null && preset.HasValue(PresetProperty.UserSelect))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.UserSelect))
                AddCss(ref cls, UserSelect);
        }
        if (_interactionUtilities is not null && _interactionUtilities.Cursor.HasValue || preset is not null && preset.HasValue(PresetProperty.Cursor))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Cursor))
                AddCss(ref cls, Cursor);
        }
        if (_interactionUtilities is not null && _interactionUtilities.ScreenReader.HasValue || preset is not null && preset.HasValue(PresetProperty.ScreenReader))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.ScreenReader))
                AddCss(ref cls, ScreenReader);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildVisualClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (_surfaceUtilities is not null && _surfaceUtilities.Border.HasValue || preset is not null && preset.HasValue(PresetProperty.Border))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Border))
                AddCss(ref cls, Border);
        }
        if (_surfaceUtilities is not null && _surfaceUtilities.BorderStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.BorderStyle))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BorderStyle))
                AddCss(ref cls, BorderStyle);
        }
        ApplyBorderColor(ref sty, ref cls, preset?.BorderColor);
        ApplyBackgroundColor(ref sty, ref cls, preset?.BackgroundColor);
        if (Rounded.HasValue || preset is not null && preset.HasValue(PresetProperty.Rounded))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Rounded))
                AddCss(ref cls, Rounded);
        }
        if (_surfaceUtilities is not null || preset is not null)
        {
            if (_surfaceUtilities is not null && _surfaceUtilities.RingColor.HasValue || preset is not null && preset.HasValue(PresetProperty.RingColor))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.RingColor))
                    AddCss(ref cls, RingColor);
            }
            if (_surfaceUtilities is not null && _surfaceUtilities.Ring.HasValue || preset is not null && preset.HasValue(PresetProperty.Ring))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Ring))
                    AddCss(ref cls, Ring);
            }
            if (_surfaceUtilities is not null && _surfaceUtilities.RingOffset.HasValue || preset is not null && preset.HasValue(PresetProperty.RingOffset))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.RingOffset))
                    AddCss(ref cls, RingOffset);
            }
        }
        if (_effectUtilities is not null && _effectUtilities.OutlineStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.OutlineStyle))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.OutlineStyle))
                AddCss(ref cls, OutlineStyle);
        }
        if (Shadow.HasValue || preset is not null && preset.HasValue(PresetProperty.Shadow))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Shadow))
                AddCss(ref cls, Shadow);
        }
        if (_effectUtilities is not null || preset is not null)
        {
            if (_effectUtilities is not null && _effectUtilities.BackdropFilter.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropFilter))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropFilter))
                    AddCss(ref cls, BackdropFilter);
            }
            if (_effectUtilities is not null && _effectUtilities.BackdropBlur.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropBlur))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropBlur))
                    AddCss(ref cls, BackdropBlur);
            }
            if (_effectUtilities is not null && _effectUtilities.BackdropBrightness.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropBrightness))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropBrightness))
                    AddCss(ref cls, BackdropBrightness);
            }
            if (_effectUtilities is not null && _effectUtilities.BackdropContrast.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropContrast))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropContrast))
                    AddCss(ref cls, BackdropContrast);
            }
            if (_effectUtilities is not null && _effectUtilities.BackdropGrayscale.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropGrayscale))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropGrayscale))
                    AddCss(ref cls, BackdropGrayscale);
            }
            if (_effectUtilities is not null && _effectUtilities.BackdropHueRotate.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropHueRotate))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropHueRotate))
                    AddCss(ref cls, BackdropHueRotate);
            }
            if (_effectUtilities is not null && _effectUtilities.BackdropInvert.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropInvert))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropInvert))
                    AddCss(ref cls, BackdropInvert);
            }
            if (_effectUtilities is not null && _effectUtilities.BackdropOpacity.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropOpacity))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropOpacity))
                    AddCss(ref cls, BackdropOpacity);
            }
            if (_effectUtilities is not null && _effectUtilities.BackdropSaturate.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropSaturate))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropSaturate))
                    AddCss(ref cls, BackdropSaturate);
            }
            if (_effectUtilities is not null && _effectUtilities.BackdropSepia.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropSepia))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.BackdropSepia))
                    AddCss(ref cls, BackdropSepia);
            }
            if (_effectUtilities is not null && _effectUtilities.Filter.HasValue || preset is not null && preset.HasValue(PresetProperty.Filter))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Filter))
                    AddCss(ref cls, Filter);
            }
            if (_effectUtilities is not null && _effectUtilities.Blur.HasValue || preset is not null && preset.HasValue(PresetProperty.Blur))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Blur))
                    AddCss(ref cls, Blur);
            }
            if (_effectUtilities is not null && _effectUtilities.Brightness.HasValue || preset is not null && preset.HasValue(PresetProperty.Brightness))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Brightness))
                    AddCss(ref cls, Brightness);
            }
            if (_effectUtilities is not null && _effectUtilities.Contrast.HasValue || preset is not null && preset.HasValue(PresetProperty.Contrast))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Contrast))
                    AddCss(ref cls, Contrast);
            }
            if (_effectUtilities is not null && _effectUtilities.DropShadow.HasValue || preset is not null && preset.HasValue(PresetProperty.DropShadow))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.DropShadow))
                    AddCss(ref cls, DropShadow);
            }
            if (_effectUtilities is not null && _effectUtilities.DropShadowColor.HasValue || preset is not null && preset.HasValue(PresetProperty.DropShadowColor))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.DropShadowColor))
                    AddCss(ref cls, DropShadowColor);
            }
            if (_effectUtilities is not null && _effectUtilities.Grayscale.HasValue || preset is not null && preset.HasValue(PresetProperty.Grayscale))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Grayscale))
                    AddCss(ref cls, Grayscale);
            }
            if (_effectUtilities is not null && _effectUtilities.HueRotate.HasValue || preset is not null && preset.HasValue(PresetProperty.HueRotate))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.HueRotate))
                    AddCss(ref cls, HueRotate);
            }
            if (_effectUtilities is not null && _effectUtilities.Invert.HasValue || preset is not null && preset.HasValue(PresetProperty.Invert))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Invert))
                    AddCss(ref cls, Invert);
            }
            if (_effectUtilities is not null && _effectUtilities.Saturate.HasValue || preset is not null && preset.HasValue(PresetProperty.Saturate))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Saturate))
                    AddCss(ref cls, Saturate);
            }
            if (_effectUtilities is not null && _effectUtilities.Sepia.HasValue || preset is not null && preset.HasValue(PresetProperty.Sepia))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Sepia))
                    AddCss(ref cls, Sepia);
            }
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.Resize.HasValue || preset is not null && preset.HasValue(PresetProperty.Resize))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Resize))
                AddCss(ref cls, Resize);
        }
        if (_advancedLayoutUtilities is not null && _advancedLayoutUtilities.Transform.HasValue || preset is not null && preset.HasValue(PresetProperty.Transform))
        {
            if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Transform))
                AddCss(ref cls, Transform);
        }
        if (_motionUtilities is not null || preset is not null)
        {
            if (_motionUtilities is not null && _motionUtilities.Animation.HasValue || preset is not null && preset.HasValue(PresetProperty.Animation))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Animation))
                    AddCss(ref cls, Animation);
            }
            if (_motionUtilities is not null && _motionUtilities.Duration.HasValue || preset is not null && preset.HasValue(PresetProperty.Duration))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Duration))
                    AddCss(ref cls, Duration);
            }
            if (_motionUtilities is not null && _motionUtilities.Transition.HasValue || preset is not null && preset.HasValue(PresetProperty.Transition))
            {
                if (preset is null || !TryAppendPresetClass(ref cls, preset, PresetProperty.Transition))
                    AddCss(ref cls, Transition);
            }
        }
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            _ = OnElementRefReady.InvokeIfHasDelegate(ElementRef);

        return Task.CompletedTask;
    }

    private PresetEvaluationState? _presetState;

    protected QuarkPresetContext? BuildPresetContext()
    {
        var preset = Preset;
        var presets = Presets;

        if (preset is null && (presets is null || presets.Count == 0))
            return null;

        return (_presetState ??= new PresetEvaluationState()).Evaluate(preset, presets);
    }

    private bool HasExplicitDataSlotAttribute()
    {
        return HasDataSlotAttribute(AdditionalAttributes) || HasDataSlotAttribute(Attributes);
    }

    private static bool HasDataSlotAttribute(IReadOnlyDictionary<string, object>? attributes)
    {
        if (attributes is null)
            return false;

        if (attributes is Dictionary<string, object> dictionary)
        {
            if (dictionary.ContainsKey("data-slot"))
                return true;

            foreach (var key in dictionary.Keys)
            {
                if (key.Equals("data-slot", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        foreach (var key in attributes.Keys)
        {
            if (key.Equals("data-slot", StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    protected override void ApplyBorderColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BorderColorBuilder>? value)
    {
        base.ApplyBorderColor(ref sty, ref cls, ResolvePresetSlot(BorderColor, value, (int)PresetProperty.BorderColor));
    }

    protected override void ApplyBackgroundColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BackgroundColorBuilder>? value)
    {
        base.ApplyBackgroundColor(ref sty, ref cls, ResolvePresetSlot(BackgroundColor, value, (int)PresetProperty.BackgroundColor));
    }

    protected override void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<TextColorBuilder>? value)
    {
        base.ApplyTextColor(ref sty, ref cls, ResolvePresetSlot(TextColor, value, (int)PresetProperty.TextColor));
    }

    protected new void BuildClassAttribute(Dictionary<string, object> attrs, BuildClassAction builder)
    {
        var cls = new PooledStringBuilder(stackalloc char[64]);
        try
        {
            builder(ref cls);
            attrs.TryGetValue("class", out var existing);
            var existingString = existing?.ToString();
            if (cls.Length == 0)
            {
                if (!string.IsNullOrEmpty(existingString))
                    attrs["class"] = existingString;
                return;
            }

            if (!string.IsNullOrEmpty(existingString))
                AppendClass(ref cls, existingString);
            attrs["class"] = cls.ToString();
        }
        finally
        {
            cls.Dispose();
        }
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        if (_advancedTypographyUtilities is not null)
        {
            AddIf(ref hc, FontFeatureSettings);
            AddIf(ref hc, FontSmoothing);
            AddIf(ref hc, FontStretch);
            AddIf(ref hc, TextIndent);
            AddIf(ref hc, TextShadowColor);
            AddIf(ref hc, TextShadow);
        }

        if (_transform3DUtilities is not null)
        {
            AddIf(ref hc, BackfaceVisibility);
            AddIf(ref hc, PerspectiveOrigin);
            AddIf(ref hc, Perspective);
            AddIf(ref hc, TransformStyle);
        }

        if (_maskUtilities is not null)
        {
            AddIf(ref hc, MaskClip);
            AddIf(ref hc, MaskComposite);
            AddIf(ref hc, MaskImage);
            AddIf(ref hc, MaskMode);
            AddIf(ref hc, MaskOrigin);
            AddIf(ref hc, MaskPosition);
            AddIf(ref hc, MaskRepeat);
            AddIf(ref hc, MaskSize);
            AddIf(ref hc, MaskType);
        }

        hc.Add(Container);
        hc.Add(Preset);

        if (Presets is { Count: > 0 })
        {
            for (var i = 0; i < Presets.Count; i++)
            {
                hc.Add(Presets[i]);
            }
        }

        hc.Add(Class);
        hc.Add(Style);
        hc.Add(Title);
        hc.Add(Hidden);
        hc.Add(DataSlot);
        AddIf(ref hc, Display);
        AddIf(ref hc, Visibility);
        AddIf(ref hc, Float);
        AddIf(ref hc, VerticalAlign);
        AddIf(ref hc, TextAlign);
        AddIf(ref hc, TextColor);
        AddIf(ref hc, TextSize);
        AddIf(ref hc, DecorationLine);
        AddIf(ref hc, UnderlineOffset);
        AddIf(ref hc, TextTransform);
        AddIf(ref hc, FontFamily);
        AddIf(ref hc, FontWeight);
        AddIf(ref hc, FontStyle);
        AddIf(ref hc, Leading);
        AddIf(ref hc, Tracking);
        AddIf(ref hc, Whitespace);
        AddIf(ref hc, TextWrap);
        AddIf(ref hc, WordBreak);
        AddIf(ref hc, TextOverflow);
        AddIf(ref hc, Truncate);
        AddIf(ref hc, LineClamp);
        AddIf(ref hc, FontVariantNumeric);
        AddIf(ref hc, Margin);
        AddIf(ref hc, Padding);
        AddIf(ref hc, AspectRatio);
        AddIf(ref hc, BackgroundAttachment);
        AddIf(ref hc, BackgroundBlendMode);
        AddIf(ref hc, BackgroundClip);
        AddIf(ref hc, BackgroundImage);
        AddIf(ref hc, BackgroundOrigin);
        AddIf(ref hc, BackgroundPosition);
        AddIf(ref hc, BackgroundRepeat);
        AddIf(ref hc, BackgroundSize);
        AddIf(ref hc, BlockSize);
        AddIf(ref hc, BoxDecorationBreak);
        AddIf(ref hc, BoxSizing);
        AddIf(ref hc, BreakAfter);
        AddIf(ref hc, BreakBefore);
        AddIf(ref hc, BreakInside);
        AddIf(ref hc, FloatClear);
        AddIf(ref hc, ClipPath);
        AddIf(ref hc, ColEnd);
        AddIf(ref hc, ColorScheme);
        AddIf(ref hc, ColumnCount);
        AddIf(ref hc, ContainerQuery);
        AddIf(ref hc, Contain);
        AddIf(ref hc, GeneratedContent);
        AddIf(ref hc, DecorationColor);
        AddIf(ref hc, DecorationStyle);
        AddIf(ref hc, DecorationThickness);
        AddIf(ref hc, TransitionDelay);
        AddIf(ref hc, Ease);
        AddIf(ref hc, FlexBasis);
        AddIf(ref hc, ForcedColorAdjust);
        AddIf(ref hc, BackgroundGradient);
        AddIf(ref hc, ColumnSpan);
        AddIf(ref hc, Hyphen);
        AddIf(ref hc, InlineSize);
        AddIf(ref hc, InsetBlockEnd);
        AddIf(ref hc, InsetBlockStart);
        AddIf(ref hc, InsetEnd);
        AddIf(ref hc, InsetRingColor);
        AddIf(ref hc, InsetRing);
        AddIf(ref hc, InsetShadowColor);
        AddIf(ref hc, InsetShadow);
        AddIf(ref hc, InsetStart);
        AddIf(ref hc, Isolation);
        AddIf(ref hc, MaxBlockSize);
        AddIf(ref hc, MaxInlineSize);
        AddIf(ref hc, MinBlockSize);
        AddIf(ref hc, MinInlineSize);
        AddIf(ref hc, MixBlendMode);
        AddIf(ref hc, Order);
        AddIf(ref hc, Origin);
        AddIf(ref hc, OutlineColor);
        AddIf(ref hc, OutlineOffset);
        AddIf(ref hc, OutlineWidth);
        AddIf(ref hc, OverflowWrap);
        AddIf(ref hc, PlaceContentAlign);
        AddIf(ref hc, PlaceItemsAlign);
        AddIf(ref hc, PlaceSelfAlign);
        AddIf(ref hc, Rotate);
        AddIf(ref hc, RowEnd);
        AddIf(ref hc, Scale);
        AddIf(ref hc, ScrollbarGutter);
        AddIf(ref hc, ScrollbarThumbColor);
        AddIf(ref hc, ScrollbarTrackColor);
        AddIf(ref hc, ScrollbarWidth);
        AddIf(ref hc, ScrollBehavior);
        AddIf(ref hc, ScrollSnapAlign);
        AddIf(ref hc, ScrollSnap);
        AddIf(ref hc, ScrollSnapStop);
        AddIf(ref hc, ShadowColor);
        AddIf(ref hc, Skew);
        AddIf(ref hc, TabSize);
        AddIf(ref hc, TouchAction);
        AddIf(ref hc, TransitionBehavior);
        AddIf(ref hc, Translate);
        AddIf(ref hc, WillChange);
        AddIf(ref hc, Zoom);
        AddIf(ref hc, Inset);
        AddIf(ref hc, Top);
        AddIf(ref hc, Right);
        AddIf(ref hc, Bottom);
        AddIf(ref hc, Left);
        AddIf(ref hc, Position);
        AddIf(ref hc, ScrollMargin);
        AddIf(ref hc, ScrollPadding);
        AddIf(ref hc, Size);
        AddIf(ref hc, Width);
        AddIf(ref hc, MinWidth);
        AddIf(ref hc, MaxWidth);
        AddIf(ref hc, Height);
        AddIf(ref hc, MinHeight);
        AddIf(ref hc, MaxHeight);
        AddIf(ref hc, Overflow);
        AddIf(ref hc, OverflowX);
        AddIf(ref hc, OverflowY);
        AddIf(ref hc, Overscroll);
        AddIf(ref hc, Flex);
        AddIf(ref hc, FlexDirection);
        AddIf(ref hc, FlexWrap);
        AddIf(ref hc, Grow);
        AddIf(ref hc, Shrink);
        AddIf(ref hc, Gap);
        AddIf(ref hc, Space);
        AddIf(ref hc, Divide);
        AddIf(ref hc, ContentAlign);
        AddIf(ref hc, ItemsAlign);
        AddIf(ref hc, Justify);
        AddIf(ref hc, SelfAlign);
        AddIf(ref hc, JustifyItemsAlign);
        AddIf(ref hc, JustifySelfAlign);
        AddIf(ref hc, ColStart);
        AddIf(ref hc, RowSpan);
        AddIf(ref hc, RowStart);
        AddIf(ref hc, Opacity);
        AddIf(ref hc, ZIndex);
        AddIf(ref hc, PointerEvents);
        AddIf(ref hc, UserSelect);
        AddIf(ref hc, Cursor);
        AddIf(ref hc, ScreenReader);
        AddIf(ref hc, BackgroundColor);
        AddIf(ref hc, Border);
        AddIf(ref hc, BorderStyle);
        AddIf(ref hc, BorderColor);
        AddIf(ref hc, Rounded);
        AddIf(ref hc, RingColor);
        AddIf(ref hc, Ring);
        AddIf(ref hc, RingOffset);
        AddIf(ref hc, OutlineStyle);
        AddIf(ref hc, Shadow);
        AddIf(ref hc, BackdropFilter);
        AddIf(ref hc, BackdropBlur);
        AddIf(ref hc, BackdropBrightness);
        AddIf(ref hc, BackdropContrast);
        AddIf(ref hc, BackdropGrayscale);
        AddIf(ref hc, BackdropHueRotate);
        AddIf(ref hc, BackdropInvert);
        AddIf(ref hc, BackdropOpacity);
        AddIf(ref hc, BackdropSaturate);
        AddIf(ref hc, BackdropSepia);
        AddIf(ref hc, Filter);
        AddIf(ref hc, Blur);
        AddIf(ref hc, Brightness);
        AddIf(ref hc, Contrast);
        AddIf(ref hc, DropShadow);
        AddIf(ref hc, DropShadowColor);
        AddIf(ref hc, Grayscale);
        AddIf(ref hc, HueRotate);
        AddIf(ref hc, Invert);
        AddIf(ref hc, Saturate);
        AddIf(ref hc, Sepia);
        AddIf(ref hc, Resize);
        AddIf(ref hc, Transform);
        AddIf(ref hc, Animation);
        AddIf(ref hc, Duration);
        AddIf(ref hc, Transition);
    }
}
