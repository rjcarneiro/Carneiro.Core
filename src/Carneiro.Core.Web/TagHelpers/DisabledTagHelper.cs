using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Carneiro.Core.Web.TagHelpers;

/// <summary>
/// The tag helper that will add the <c>disabled</c> attribute for <c>input</c> if the <see cref="IsDisabled"/> it set to <c>true</c>.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
[HtmlTargetElement("input")]
[HtmlTargetElement("select")]
[HtmlTargetElement("button")]
public class DisabledTagHelper : TagHelper
{
    private const string DisabledAttributeName = "asp-disabled";

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="CheckedTagHelper"/> is readonly.
    /// </summary>
    /// <value>
    ///   <c>true</c> if readonly; otherwise, <c>false</c>.
    /// </value>
    [HtmlAttributeName(DisabledAttributeName)]
    public bool? IsDisabled { get; set; }

    /// <inheritdoc/>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (IsDisabled == true)
        {
            output.Attributes.SetAttribute("disabled", string.Empty);
        }
    }
}