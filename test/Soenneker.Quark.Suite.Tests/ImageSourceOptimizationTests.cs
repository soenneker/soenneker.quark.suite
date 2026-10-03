using System;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ImageSourceOptimizationTests
{
    [Test]
    public void Srcset_preserves_first_width_order_in_small_and_large_lists()
    {
        int[] large = [960, 480, 960, 480, 960, 480, 960, 480, 960, 480, 960, 480, 960, 480, 960, 480, 960];
        const string expected = "/image-960.png?q=1#view 960w, /image-480.png?q=1#view 480w, /image.png?q=1#view 1200w";
        ImageSources.BuildSrcSet("/image.png?q=1#view", [960, 480, 960], 1200).Should().Be(expected);
        ImageSources.BuildSrcSet("/image.png?q=1#view", large, 1200).Should().Be(expected);
    }

    [Test]
    public void Srcset_formats_full_integer_width_and_validates_filtered_entries()
    {
        ImageSources.BuildSrcSet("/image.png", [int.MaxValue], int.MaxValue).Should().Be("/image.png 2147483647w");
        ImageSources.BuildSrcSet("/image.png", [int.MaxValue]).Should().Be("/image-2147483647.png 2147483647w");
        Action invalid = () => ImageSources.BuildSrcSet("/image.png", [960, 0], 480);
        invalid.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [Arguments("https://example.com")]
    [Arguments("//example.com?q=file.png")]
    [Arguments("/images/.hidden")]
    [Arguments("data:image/png;base64,abc")]
    [Arguments("blob:https://example.com/file.png")]
    public void Non_file_urls_do_not_receive_size_suffixes(string source)
    {
        ImageSources.BuildSrcSet(source, [480]).Should().BeNull();
    }

    [Test]
    public void Unchanged_extension_reuses_source_and_replacement_preserves_suffix()
    {
        const string source = "https://example.com/image.png?q=.jpg#view";
        ImageSources.ApplyExtension(source, " .png ").Should().BeSameAs(source);
        ImageSources.ApplyExtension(source, "..webp").Should().Be("https://example.com/image.webp?q=.jpg#view");
    }
}
