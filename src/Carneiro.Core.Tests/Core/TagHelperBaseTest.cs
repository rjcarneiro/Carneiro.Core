namespace Carneiro.Core.Tests.Core;

/// <summary>
/// The base tag helper test. Works very well to test custom <see cref="TagHelper"/>.
/// </summary>
public abstract class TagHelperBaseTest
{
    /// <summary>
    /// Generates and processes the <paramref name="tagHelper"/> with a new <see cref="TagHelperOutput"/> and <see cref="TagHelperContext"/> using <c>input</c> as default element.
    /// </summary>
    /// <param name="tagHelper"></param>
    /// <param name="action"></param>
    /// <remarks>Uses <c>input</c> as default element.</remarks>
    protected virtual void GenerateTagHelperOutput(TagHelper tagHelper, Action<TagHelperContext, TagHelperOutput> action) => GenerateTagHelperOutput("input", tagHelper, action);

    /// <summary>
    /// Generates and processes the <paramref name="tagHelper"/> with a new <see cref="TagHelperOutput"/> and <see cref="TagHelperContext"/>.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="tagHelper"></param>
    /// <param name="action"></param>
    protected virtual void GenerateTagHelperOutput(string element, TagHelper tagHelper, Action<TagHelperContext, TagHelperOutput> action)
    {
        var output = new TagHelperOutput(element, [], (_, _) =>
        {
            var tagHelperContent = new DefaultTagHelperContent();
            return Task.FromResult<TagHelperContent>(tagHelperContent.SetContent(string.Empty));
        });

        var context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("N"));

        tagHelper.Process(context, output);

        action(context, output);
    }

    /// <summary>
    /// Verifies if the <typeparamref name="T"/> is using <see cref="HtmlAttributeNameAttribute"/> for its properties.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="property"></param>
    /// <param name="htmlAttributeName"></param>
    protected virtual void VerifyHtmlAttribute<T>(string property, string htmlAttributeName) where T : TagHelper
    {
        Type t = typeof(T);
        PropertyInfo pi = t.GetProperty(property);

        Assert.That(Attribute.IsDefined(pi, typeof(HtmlAttributeNameAttribute)), Is.True);

        var propertyAttribute = Attribute.GetCustomAttribute(pi, typeof(HtmlAttributeNameAttribute));
        Assert.That(propertyAttribute, Is.Not.Null);
        Assert.That(propertyAttribute, Is.InstanceOf<HtmlAttributeNameAttribute>());

        var htmlAttribute = (HtmlAttributeNameAttribute)propertyAttribute;
        Assert.That(htmlAttribute.Name, Is.EqualTo(htmlAttributeName));
    }

    /// <summary>
    /// Verifies all <see cref="HtmlTargetElementAttribute"/> within <typeparamref name="T"/>, calling an <paramref name="action"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    protected virtual void VerifyHtmlTargetElementAttribute<T>(Action<HtmlTargetElementAttribute[]> action) where T : TagHelper
    {
        Type t = typeof(T);
        Attribute[] attrs = Attribute.GetCustomAttributes(t, typeof(HtmlTargetElementAttribute));
        Assert.That(attrs, Is.Not.Null);

        action((HtmlTargetElementAttribute[])attrs);
    }
}