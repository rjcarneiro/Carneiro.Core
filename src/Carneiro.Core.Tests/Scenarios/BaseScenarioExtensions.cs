using Carneiro.Core.Tests.Core;
using Carneiro.Core.Tests.Scenarios.Builders;

namespace Carneiro.Core.Tests.Scenarios;

/// <summary>
/// Extensions methods for <see cref="BaseScenario"/>.
/// </summary>
public static class BaseScenarioExtensions
{
    /// <summary>
    /// Gets the unit of work.
    /// </summary>
    /// <param name="baseScenario">The base scenario.</param>
    /// <returns></returns>
    public static IUnitOfWork<TDbContext> GetUnitOfWork<TDbContext>(this BaseScenario baseScenario) where TDbContext : DbContext => baseScenario.GetService<IUnitOfWork<TDbContext>>();

    /// <summary>
    /// Adds authentication based in the <see cref="BaseScenario"/> account organisation user.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseScenario">The scenario.</param>
    /// <param name="controller">The controller.</param>
    public static void AddAuthentication<T>(this BaseScenario baseScenario, T controller) where T : ControllerBase => controller.EnsureHttpContext(baseScenario);

    /// <summary>
    /// Adds authentication based in the <see cref="BaseScenario" /> account organisation user.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseScenario">The scenario.</param>
    /// <param name="controller">The controller.</param>
    /// <param name="controllerBuilder">The controller builder.</param>
    public static void AddAuthentication<T>(this BaseScenario baseScenario, T controller, Action<IControllerBuilder> controllerBuilder) where T : ControllerBase => controller.EnsureHttpContext(baseScenario, controllerBuilder);

    /// <summary>
    /// Gets the controller with authentication.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseScenario">The scenario.</param>
    /// <returns></returns>
    public static T GetControllerWithAuthentication<T>(this BaseScenario baseScenario) where T : ControllerBase => baseScenario.GetControllerWithAuthentication<T>(controllerBuilder: null);

    /// <summary>
    /// Gets the controller with authentication.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseScenario">The scenario.</param>
    /// <param name="controllerBuilder">The controller builder.</param>
    /// <returns></returns>
    public static T GetControllerWithAuthentication<T>(this BaseScenario baseScenario, Action<IControllerBuilder> controllerBuilder) where T : ControllerBase
    {
        T controller = baseScenario.GetRequiredService<T>();
        baseScenario.AddAuthentication(controller, controllerBuilder);
        return controller;
    }

    /// <summary>
    /// Gets the controller with anonymous authentication.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseScenario">The scenario.</param>
    /// <returns></returns>
    public static T GetControllerWithAnonymousAuthentication<T>(this BaseScenario baseScenario) where T : ControllerBase => baseScenario.GetControllerWithAnonymousAuthentication<T>(controllerBuilderAction: null);

    /// <summary>
    /// Gets the controller with anonymous authentication.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseScenario">The base scenario.</param>
    /// <param name="controllerBuilderAction">The controller builder action.</param>
    /// <returns></returns>
    public static T GetControllerWithAnonymousAuthentication<T>(this BaseScenario baseScenario, Action<IControllerBuilder> controllerBuilderAction) where T : ControllerBase
    {
        T controller = baseScenario.GetRequiredService<T>();

        controller.WithAnonymousAuthentication(baseScenario, controllerBuilderAction);
        return controller;
    }

    /// <summary>
    /// Generates the email.
    /// </summary>
    /// <param name="baseScenario">The base scenario.</param>
    /// <returns></returns>
    public static string GenerateEmail(this BaseScenario baseScenario) => baseScenario.Faker.GenerateEmail();

    /// <summary>
    /// Determines whether [has validate anti forgery token attribute] [the specified expression].
    /// </summary>
    /// <param name="baseScenario">The scenario.</param>
    /// <param name="expression">The expression.</param>
    /// <returns>
    ///   <c>true</c> if [has validate anti forgery token attribute] [the specified expression]; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasValidateAntiForgeryTokenAttribute(this BaseScenario baseScenario, Expression<Action> expression) => baseScenario.HasAttribute<ValidateAntiForgeryTokenAttribute>(expression);

    /// <summary>
    /// Determines whether [has ignore antiforgery token attribute] [the specified expression].
    /// </summary>
    /// <param name="baseScenario">The base scenario.</param>
    /// <param name="expression">The expression.</param>
    /// <returns>
    ///   <c>true</c> if [has ignore antiforgery token attribute] [the specified expression]; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasIgnoreAntiForgeryTokenAttribute(this BaseScenario baseScenario, Expression<Action> expression) => baseScenario.HasAttribute<IgnoreAntiforgeryTokenAttribute>(expression);

    /// <summary>
    /// Determines whether the specified expression has attribute.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseScenario">The scenario.</param>
    /// <param name="expression">The expression.</param>
    /// <returns>
    ///   <c>true</c> if the specified expression has attribute; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasAttribute<T>(this BaseScenario baseScenario, Expression<Action> expression) where T : Attribute
    {
        MethodInfo method = MethodOf(expression);
        return method.GetCustomAttributes(typeof(T), false).Any();
    }


    private static MethodInfo MethodOf(Expression<Action> expression)
    {
        var body = (MethodCallExpression)expression.Body;
        return body.Method;
    }
}