using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Carneiro.Core.Web.TagHelpers;

/// <summary>
/// The tag helper that will add the <c>checked</c> attribute for <c>input</c> if the <see cref="IsChecked"/> it set to <c>true</c>.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
[HtmlTargetElement("input")]
public class CheckedTagHelper : TagHelper
{
    private const string CheckedAttributeName = "asp-checked";

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="CheckedTagHelper"/> is readonly.
    /// </summary>
    /// <value>
    ///   <c>true</c> if readonly; otherwise, <c>false</c>.
    /// </value>
    [HtmlAttributeName(CheckedAttributeName)]
    public bool? IsChecked { get; set; }

    /// <inheritdoc/>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (IsChecked == true)
        {
            output.Attributes.SetAttribute("checked", string.Empty);
        }
    }
}