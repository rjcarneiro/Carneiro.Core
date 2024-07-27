using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Carneiro.Core.Web.TagHelpers;

/// <summary>
/// The tag helper that will add the <c>readonly</c> attribute for <c>input</c> if the <see cref="IsReadonly"/> it set to <c>true</c>.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
[HtmlTargetElement("input")]
public class ReadonlyTagHelper : TagHelper
{
    private const string ReadonlyAttributeName = "asp-readonly";

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ReadonlyTagHelper"/> is readonly.
    /// </summary>
    /// <value>
    ///   <c>true</c> if readonly; otherwise, <c>false</c>.
    /// </value>
    [HtmlAttributeName(ReadonlyAttributeName)]
    public bool? IsReadonly { get; set; }

    /// <inheritdoc/>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (IsReadonly == true)
        {
            output.Attributes.SetAttribute("readonly", string.Empty);
        }
    }
}