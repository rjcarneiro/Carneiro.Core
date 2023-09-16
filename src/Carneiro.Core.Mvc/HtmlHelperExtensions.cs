using Microsoft.AspNetCore.Mvc.Rendering;

namespace Carneiro.Core.Mvc;

/// <summary>
/// <see cref="IHtmlHelper"/> extension methods.
/// </summary>
public static class HtmlHelperExtensions
{
    /// <summary>
    /// Determines whether the specified controllers is selected.
    /// </summary>
    /// <param name="htmlHelper">The HTML helper.</param>
    /// <param name="controllers">The controllers.</param>
    /// <returns></returns>
    public static string IsSelected(this IHtmlHelper htmlHelper, string controllers)
    {
        var currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

        IEnumerable<string> acceptedControllers = (controllers ?? currentController)?.Split(',') ?? Array.Empty<string>();

        return acceptedControllers.Contains(currentController) ? "active" : string.Empty;
    }

    /// <summary>
    /// Determines whether the specified controllers is selected.
    /// </summary>
    /// <param name="htmlHelper">The HTML helper.</param>
    /// <param name="acceptedControllers">The accepted controllers.</param>
    /// <returns></returns>
    public static string IsSelected(this IHtmlHelper htmlHelper, params string[] acceptedControllers)
    {
        var currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

        return acceptedControllers.Contains(currentController) ? "active" : string.Empty;
    }

    /// <summary>
    /// Determines whether the specified controllers is selected.
    /// </summary>
    /// <param name="htmlHelper">The HTML helper.</param>
    /// <param name="controllers">The controllers.</param>
    /// <param name="actions">The actions.</param>
    /// <returns></returns>
    public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions) => IsSelected(htmlHelper, controllers, new[] { actions });

    /// <summary>
    /// Determines whether the specified controllers is selected.
    /// </summary>
    /// <param name="htmlHelper">The HTML helper.</param>
    /// <param name="controllers">The controllers.</param>
    /// <param name="actions">The actions.</param>
    /// <returns></returns>
    public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, params string[] actions)
    {
        var currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
        var currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

        IEnumerable<string> acceptedControllers = (controllers ?? currentController)?.Split(',') ?? Array.Empty<string>();

        return actions.Contains(currentAction) && acceptedControllers.Contains(currentController) ? "active" : string.Empty;
    }

    /// <summary>
    /// Builds the canonical URL.
    /// </summary>
    /// <param name="html">The HTML.</param>
    /// <returns></returns>
    public static string BuildCanonicalUrl(this IHtmlHelper html) => $"{html.ViewContext.HttpContext.Request.Scheme}://{html.ViewContext.HttpContext.Request.Host}{html.ViewContext.HttpContext.Request.Path}";
}