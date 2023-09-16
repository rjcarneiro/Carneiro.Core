using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using Carneiro.Core.Tests.Scenarios;
using Carneiro.Core.Tests.Scenarios.Builders;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Moq;

namespace Carneiro.Core.Tests.Core;

/// <summary>
/// Controller extension methods.
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Adds anonymous authentication to the controller.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controller">The controller.</param>
    /// <returns></returns>
    public static void WithAnonymousAuthentication<T>(this T controller) where T : ControllerBase
    {
        controller.EnsureHttpContext();
        controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
    }

    /// <summary>
    /// Adds anonymous authentication to the controller.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controller">The controller.</param>
    /// <param name="baseScenario">The scenario.</param>
    /// <param name="controllerBuilderAction">The controller builder action.</param>
    public static void WithAnonymousAuthentication<T>(this T controller, BaseScenario baseScenario, Action<IControllerBuilder> controllerBuilderAction) where T : ControllerBase
    {
        controller.EnsureHttpContext(baseScenario, controllerBuilderAction);
        controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
    }

    /// <summary>
    /// Ensures the HTTP context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controller">The controller.</param>
    /// <returns></returns>
    public static void EnsureHttpContext<T>(this T controller) where T : ControllerBase
    {
        var defaultHttpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(
            defaultHttpContext,
            new RouteData(),
            new ActionDescriptor()
        );

        controller.ControllerContext ??= new ControllerContext();
        controller.ControllerContext.HttpContext ??= defaultHttpContext;
        controller.Url ??= new UrlHelper(actionContext);

        if (controller is Controller c)
            c.TempData ??= new TempDataDictionary(defaultHttpContext, new Mock<ITempDataProvider>().Object);
    }

    /// <summary>
    /// Ensures the HTTP context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controller">The controller.</param>
    /// <param name="baseScenario">The scenario.</param>
    public static void EnsureHttpContext<T>(this T controller, BaseScenario baseScenario) where T : ControllerBase => controller.EnsureHttpContext<T>(baseScenario, controllerBuilder: null);

    /// <summary>
    /// Ensures the HTTP context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controller">The controller.</param>
    /// <param name="baseScenario">The base scenario.</param>
    /// <param name="controllerBuilderAction">The controller builder action.</param>
    public static void EnsureHttpContext<T>(this T controller, BaseScenario baseScenario, Action<IControllerBuilder> controllerBuilderAction) where T : ControllerBase
    {
        IControllerBuilder controllerBuilder = ControllerBuilder.Init();
        controllerBuilderAction?.Invoke(controllerBuilder);

        controller.EnsureHttpContext(baseScenario, controllerBuilder);
    }

    /// <summary>
    /// Ensures the HTTP context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controller">The controller.</param>
    /// <param name="baseScenario">The base scenario.</param>
    /// <param name="controllerBuilder">The controller builder.</param>
    public static void EnsureHttpContext<T>(this T controller, BaseScenario baseScenario, IControllerBuilder controllerBuilder) where T : ControllerBase
    {
        IHttpContextAccessor httpContextAccessor = baseScenario.GetRequiredService<IHttpContextAccessor>();

        var controllerActionDescriptor = new ControllerActionDescriptor();
        var actionContext = new ActionContext(httpContextAccessor.HttpContext!,
            httpContextAccessor.HttpContext.GetRouteData(),
            controllerActionDescriptor
        );

        controller.ControllerContext ??= new ControllerContext(actionContext);
        controller.ControllerContext.HttpContext ??= httpContextAccessor.HttpContext!;
        controller.ControllerContext.RouteData ??= httpContextAccessor.HttpContext.GetRouteData();
        controller.ControllerContext.ActionDescriptor ??= controllerActionDescriptor;

        controller.Url = baseScenario.Url.Object;

        if (controllerBuilder != null)
        {
            IControllerBuilderOptions controllerBuilderOptions = controllerBuilder.Build();
            if (controllerBuilderOptions.Cookies.Count > 0)
            {
                IRequestCookieCollection cookies = MockRequestCookieCollection(controllerBuilderOptions);
                controller.ControllerContext.HttpContext.Request.Cookies = cookies;
            }

            if (controllerBuilderOptions.Form.Count > 0)
            {
                controller.ControllerContext.HttpContext.Request.Form = new FormCollection(controllerBuilderOptions.Form);
            }
        }

        if (controller is Controller c)
            c.TempData ??= new TempDataDictionary(httpContextAccessor.HttpContext, new Mock<ITempDataProvider>().Object);
    }

    /// <summary>
    /// Writes the model state errors for fluent validator <typeparamref name="V"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="controller">The controller.</param>
    /// <param name="viewModel">The view model.</param>
    public static async Task WriteModelStateErrorsAsync<T, V>(this Controller controller, T viewModel)
        where T : class
        where V : AbstractValidator<T>, new()
    {
        var validator = new V();

        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(viewModel);

        foreach (ValidationFailure validationResultError in validationResult.Errors)
        {
            controller.ModelState.AddModelError(validationResultError.PropertyName, validationResultError.ErrorMessage);
        }
    }

    /// <summary>
    /// Writes the model state errors for DataAnnotations validator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controller">The controller.</param>
    /// <param name="viewModel">The view model.</param>
    /// <remarks>Used in Helpdesk for the data annotations validation.</remarks>
    public static void WriteModelStateErrors<T>(this ControllerBase controller, T viewModel) where T : class
    {
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

        bool isValid = Validator.TryValidateObject(viewModel,
            new ValidationContext(viewModel, serviceProvider: null, items: null),
            results,
            validateAllProperties: true);

        if (!isValid)
        {
            foreach (System.ComponentModel.DataAnnotations.ValidationResult r in results)
            {
                foreach (string propertyName in r.MemberNames)
                {
                    // Add validation errors to the ModelState
                    controller.ControllerContext.ModelState.AddModelError(propertyName, r.ErrorMessage ?? string.Empty);
                }
            }
        }
    }

    private static IRequestCookieCollection MockRequestCookieCollection(IControllerBuilderOptions controllerBuilderOptions)
    {
        var sb = new StringBuilder();
        sb.Append(string.Join("; ", controllerBuilderOptions.Cookies
            .Select(keyValuePair => $"{keyValuePair.Key}={WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(keyValuePair.Value))}")));

        var requestFeature = new HttpRequestFeature
        {
            Headers = new HeaderDictionary
            {
                { HeaderNames.Cookie, sb.ToString() }
            }
        };

        var featureCollection = new FeatureCollection();
        featureCollection.Set<IHttpRequestFeature>(requestFeature);

        return new RequestCookiesFeature(featureCollection).Cookies;
    }
}