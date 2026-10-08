using System;
using System.Linq;
using System.Reflection;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Html_attributes_preserve_false_tokens_and_remove_nullable_values_on_update()
    {
        var cut = Render<Div>(p => p.Add(c => c.SpellCheck, false)
            .Add(c => c.AutoCorrect, false).Add(c => c.TranslateContent, false)
            .Add(c => c.HtmlDraggable, false).Add(c => c.Inert, true)
            .Add(c => c.ContentEditable, "plaintext-only").Add(c => c.Lang, "en-US")
            .Add(c => c.Dir, "rtl").Add(c => c.AriaLive, "polite")
            .Add(c => c.AriaAtomic, false).Add(c => c.AriaBusy, true));
        var element = cut.Find("div");
        element.GetAttribute("spellcheck").Should().Be("false");
        element.GetAttribute("autocorrect").Should().Be("off");
        element.GetAttribute("translate").Should().Be("no");
        element.GetAttribute("draggable").Should().Be("false");
        element.GetAttribute("aria-atomic").Should().Be("false");
        element.GetAttribute("contenteditable").Should().Be("plaintext-only");
        element.HasAttribute("inert").Should().BeTrue();
        cut.Render(p => p.Add(c => c.SpellCheck, (bool?)null).Add(c => c.Inert, false)
            .Add(c => c.AriaLive, "assertive").Add(c => c.AriaBusy, false).Add(c => c.Dir, "ltr"));
        element = cut.Find("div");
        element.HasAttribute("spellcheck").Should().BeFalse();
        element.HasAttribute("inert").Should().BeFalse();
        element.GetAttribute("aria-live").Should().Be("assertive");
        element.GetAttribute("aria-busy").Should().Be("false");
        element.GetAttribute("dir").Should().Be("ltr");
    }

    [Test]
    public void Html_attributes_on_inputs_preserve_autofill_tokens_and_native_constraints()
    {
        var cut = Render<Input>(p => p.Add(c => c.AutoComplete, "section-contact email")
            .Add(c => c.MinLength, 0).Add(c => c.MaxLength, 100).Add(c => c.Pattern, "[a-z]+")
            .Add(c => c.Form, "profile").Add(c => c.SpellCheck, false).Add(c => c.EnterKeyHint, "next"));
        var input = cut.Find("input");
        input.GetAttribute("autocomplete").Should().Be("section-contact email");
        input.GetAttribute("minlength").Should().Be("0");
        input.GetAttribute("maxlength").Should().Be("100");
        input.GetAttribute("pattern").Should().Be("[a-z]+");
        input.GetAttribute("form").Should().Be("profile");
        input.GetAttribute("spellcheck").Should().Be("false");
        cut.Render(p => p.Add(c => c.MaxLength, (int?)null).Add(c => c.AutoComplete, "off"));
        cut.Find("input").HasAttribute("maxlength").Should().BeFalse();
        cut.Find("input").GetAttribute("autocomplete").Should().Be("off");
    }

    [Test]
    public void Html_attributes_on_wrapped_inputs_reach_the_native_control()
    {
        var text = Render<TextInput>(p => p.Add(c => c.SpellCheck, false).Add(c => c.AutoComplete, "email")
            .Add(c => c.AriaLive, "polite").Add(c => c.Form, "profile"));
        text.Find("input").GetAttribute("spellcheck").Should().Be("false");
        text.Find("input").GetAttribute("autocomplete").Should().Be("email");
        text.Find("input").GetAttribute("aria-live").Should().Be("polite");
        text.Find("div").HasAttribute("autocomplete").Should().BeFalse();
        var password = Render<PasswordInput>(p => p.Add(c => c.SpellCheck, false)
            .Add(c => c.Form, "login").Add(c => c.MinLength, 12));
        password.Find("input").GetAttribute("autocomplete").Should().Be("current-password");
        password.Find("input").GetAttribute("minlength").Should().Be("12");
        password.Find("input").GetAttribute("form").Should().Be("login");
        var numeric = Render<NumericInput>(p => p.Add(c => c.AutoComplete, "off").Add(c => c.EnterKeyHint, "done"));
        numeric.Find("input").GetAttribute("autocomplete").Should().Be("off");
        numeric.Find("input").GetAttribute("enterkeyhint").Should().Be("done");
        var date = Render<DateInput>(p => p.Add(c => c.AutoComplete, "bday").Add(c => c.SpellCheck, false));
        date.Find("input").GetAttribute("autocomplete").Should().Be("bday");
        date.Find("input").GetAttribute("spellcheck").Should().Be("false");
    }

    [Test]
    public void Html_attributes_on_textareas_and_native_selects_are_scoped_to_the_control()
    {
        var textarea = Render<TextArea>(p => p.Add(c => c.Cols, 40).Add(c => c.Wrap, "hard")
            .Add(c => c.DirName, "message.dir").Add(c => c.AutoComplete, "off"));
        textarea.Find("textarea").GetAttribute("cols").Should().Be("40");
        textarea.Find("textarea").GetAttribute("wrap").Should().Be("hard");
        textarea.Find("textarea").GetAttribute("dirname").Should().Be("message.dir");
        var memo = Render<MemoInput>(p => p.Add(c => c.Cols, 30).Add(c => c.Form, "message"));
        memo.Find("textarea").GetAttribute("cols").Should().Be("30");
        var select = Render<NativeSelect>(p => p.Add(c => c.AutoComplete, "country")
            .Add(c => c.Form, "address").Add(c => c.HtmlSize, 4));
        select.Find("select").GetAttribute("autocomplete").Should().Be("country");
        select.Find("select").GetAttribute("size").Should().Be("4");
        select.Find("div").HasAttribute("form").Should().BeFalse();
    }

    [Test]
    public void Html_attributes_follow_the_rendered_link_or_button_element()
    {
        var link = Render<Anchor>(p => p.Add(c => c.Href, "/download").Add(c => c.Download, "")
            .Add(c => c.Rel, "external").Add(c => c.ReferrerPolicy, "no-referrer"));
        link.Find("a").HasAttribute("download").Should().BeTrue();
        link.Find("a").GetAttribute("referrerpolicy").Should().Be("no-referrer");
        link.Render(p => p.Add(c => c.Href, (string?)null));
        link.Find("span").HasAttribute("download").Should().BeFalse();
        link.Find("span").HasAttribute("rel").Should().BeFalse();
        var button = Render<Button>(p => p.Add(c => c.Type, ButtonType.Submit)
            .Add(c => c.FormAction, "/save").Add(c => c.FormNoValidate, true));
        button.Find("button").GetAttribute("formaction").Should().Be("/save");
        button.Find("button").HasAttribute("formnovalidate").Should().BeTrue();
        button.Render(p => p.Add(c => c.Href, "/next"));
        button.Find("a").HasAttribute("formaction").Should().BeFalse();
        button.Find("a").HasAttribute("formnovalidate").Should().BeFalse();
    }

    [Test]
    public void Html_attributes_support_native_lists_disclosure_groups_and_table_headers()
    {
        var list = Render<OrderedList>(p => p.Add(c => c.Start, 5).Add(c => c.Reversed, true).Add(c => c.Type, "I"));
        list.Find("ol").GetAttribute("start").Should().Be("5");
        list.Find("ol").HasAttribute("reversed").Should().BeTrue();
        var item = Render<OrderedListItem>(p => p.Add(c => c.Value, 10));
        item.Find("li").GetAttribute("value").Should().Be("10");
        var details = Render<Details>(p => p.Add(c => c.Name, "faq").Add(c => c.Open, true));
        details.Find("details").GetAttribute("name").Should().Be("faq");
        var head = Render<ThPrimitive>(p => p.Add(c => c.Scope, "row").Add(c => c.HtmlRowSpan, 0)
            .Add(c => c.Abbr, "Price").Add(c => c.AriaSort, "ascending"));
        head.Find("th").GetAttribute("scope").Should().Be("row");
        head.Find("th").GetAttribute("rowspan").Should().Be("0");
        head.Find("th").GetAttribute("aria-sort").Should().Be("ascending");
    }

    [Test]
    public void Html_attributes_do_not_add_element_specific_properties_to_unrelated_components()
    {
        foreach (var type in new[] { typeof(Div), typeof(Main), typeof(Section), typeof(Anchor) })
        {
            type.GetProperty("AutoComplete").Should().BeNull();
            type.GetProperty("FormAction").Should().BeNull();
            type.GetProperty("MaxLength").Should().BeNull();
        }
        typeof(TextArea).GetProperty("Accept").Should().BeNull();
        typeof(NativeSelect).GetProperty("Pattern").Should().BeNull();
        typeof(Input).GetProperty("HtmlRowSpan").Should().BeNull();
    }

    [Test]
    public void Html_attributes_have_no_duplicate_inherited_Blazor_parameters()
    {
        foreach (var type in typeof(Element).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(Microsoft.AspNetCore.Components.IComponent).IsAssignableFrom(t)))
        {
            var parameters = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.IsDefined(typeof(ParameterAttribute)))
                .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase);
            parameters.Where(g => g.Count() > 1).Select(g => g.Key).Should().BeEmpty(type.FullName);
        }
    }
    [Test]
    public void Html_attributes_respect_input_types_and_update_when_the_type_changes()
    {
        var cut = Render<Input>(p => p.Add(c => c.Type, "file").Add(c => c.Accept, "image/*")
            .Add(c => c.Multiple, true).Add(c => c.MinLength, 3).Add(c => c.FormAction, "/save"));
        cut.Find("input").GetAttribute("accept").Should().Be("image/*");
        cut.Find("input").HasAttribute("multiple").Should().BeTrue();
        cut.Find("input").HasAttribute("minlength").Should().BeFalse();
        cut.Find("input").HasAttribute("formaction").Should().BeFalse();
        cut.Render(p => p.Add(c => c.Type, "text"));
        cut.Find("input").HasAttribute("accept").Should().BeFalse();
        cut.Find("input").HasAttribute("multiple").Should().BeFalse();
        cut.Find("input").GetAttribute("minlength").Should().Be("3");
        cut.Render(p => p.Add(c => c.Type, "submit"));
        cut.Find("input").GetAttribute("formaction").Should().Be("/save");
        cut.Find("input").HasAttribute("minlength").Should().BeFalse();
    }

    [Test]
    public void Html_attributes_render_form_options_and_media_options()
    {
        var form = Render<Form>(p => p.Add(c => c.Model, new object()).Add(c => c.AutoComplete, "off")
            .Add(c => c.NoValidate, true).Add(c => c.AcceptCharset, "UTF-8").Add(c => c.EncType, "multipart/form-data"));
        form.Find("form").GetAttribute("autocomplete").Should().Be("off");
        form.Find("form").HasAttribute("novalidate").Should().BeTrue();
        form.Find("form").GetAttribute("accept-charset").Should().Be("UTF-8");
        var video = Render<Video>(p => p.Add(c => c.IntrinsicWidth, 640).Add(c => c.IntrinsicHeight, 360)
            .Add(c => c.ControlsList, "nodownload"));
        video.Find("video").GetAttribute("width").Should().Be("640");
        video.Find("video").GetAttribute("controlslist").Should().Be("nodownload");
        var audio = Render<Audio>(p => p.Add(c => c.DisableRemotePlayback, true));
        audio.Find("audio").HasAttribute("disableremoteplayback").Should().BeTrue();
    }
    [Test]
    public void Html_attributes_keep_button_links_separate_from_form_buttons()
    {
        var cut = Render<Button>(p => p.Add(c => c.Href, "/report").Add(c => c.Target, Target.Blank)
            .Add(c => c.Rel, "noopener").Add(c => c.Download, "report.pdf").Add(c => c.Form, "report-form")
            .Add(c => c.AriaExpanded, false).Add(c => c.AriaHasPopup, "menu"));
        var link = cut.Find("a");
        link.GetAttribute("target").Should().Be("_blank");
        link.GetAttribute("download").Should().Be("report.pdf");
        link.HasAttribute("form").Should().BeFalse();
        link.GetAttribute("aria-expanded").Should().Be("false");
        cut.Render(p => p.Add(c => c.Href, (string?)null).Add(c => c.Type, ButtonType.Button));
        var button = cut.Find("button");
        button.HasAttribute("target").Should().BeFalse();
        button.HasAttribute("download").Should().BeFalse();
        button.GetAttribute("form").Should().Be("report-form");
    }

    [Test]
    public void Html_attributes_work_on_cancellable_elements_and_preserve_attribute_overrides()
    {
        var score = Render<Score>(p => p.Add(c => c.AriaLive, "polite").Add(c => c.AriaBusy, false)
            .Add(c => c.Lang, "en"));
        score.Find("[role='progressbar']").GetAttribute("aria-live").Should().Be("polite");
        score.Find("[role='progressbar']").GetAttribute("aria-busy").Should().Be("false");
        var div = Render<Div>(p => p.Add(c => c.SpellCheck, false).Add(c => c.AdditionalAttributes, new System.Collections.Generic.Dictionary<string, object> { ["spellcheck"] = "true" }));
        div.Find("div").GetAttribute("spellcheck").Should().Be("true");
    }
}
