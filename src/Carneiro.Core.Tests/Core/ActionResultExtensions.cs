namespace Carneiro.Core.Tests.Core;

/// <summary>
/// <see cref="IActionResult"/> extension methods.
/// </summary>
public static class ActionResultExtensions
{
    /// <summary>
    /// Gets the view model as <typeparam name="T"></typeparam> based in a <paramref name="actionResult"/> as <see cref="ViewResult"/>.
    /// </summary>
    /// <param name="actionResult">The action result.</param>
    public static T GetViewModel<T>(this IActionResult actionResult) where T : class => ((ViewResult)actionResult).Model as T;

    /// <summary>
    /// Gets the view model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="actionResult">The action result.</param>
    public static T GetViewModel<T>(this ObjectResult actionResult) where T : class => actionResult.Value as T;

    /// <summary>
    /// Gets the view model as <typeparam name="T"></typeparam> based in a <paramref name="actionResult"/> as <see cref="JsonResult"/>.
    /// </summary>
    /// <param name="actionResult">The action result.</param>
    public static T GetJsonModel<T>(this IActionResult actionResult) where T : class => ((JsonResult)actionResult).Value as T;

    /// <summary>
    /// Gets the view model as <c>T</c> based in a <paramref name="actionResult" /> as <see cref="ObjectResult" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="actionResult">The action result.</param>
    public static T GetObjectResult<T, R>(this IActionResult actionResult) where T : class where R : ObjectResult => ((R)actionResult).Value as T;
}