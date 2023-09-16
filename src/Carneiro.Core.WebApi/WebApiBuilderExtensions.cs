using Carneiro.Core.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Carneiro.Core.WebApi;

/// <summary>
/// <see cref="WebApiBuilderExtensions"/> Extensions.
/// </summary>
public static class WebApiBuilderExtensions
{
    /// <summary>
    /// Adds the WebApi
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services) => services.AppWebApiOptions();

    /// <summary>
    /// Adds the WebApi
    /// </summary>
    /// <param name="services"></param>
    /// <param name="mvcBuilderAction"></param>
    /// <returns></returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<IMvcBuilder> mvcBuilderAction) => services.AppWebApiOptions(null, null, null, mvcBuilderAction);
    
    /// <summary>
    /// Adds the WebApi
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="jsonOptions">The json options.</param>
    /// <returns></returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcNewtonsoftJsonOptions> jsonOptions) => services.AppWebApiOptions(null, null, jsonOptions);

    /// <summary>
    /// Adds the WebApi
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="mvcOptions">The MVC options.</param>
    /// <returns></returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> mvcOptions) => services.AppWebApiOptions(mvcOptions);

    /// <summary>
    /// Adds the WebApi
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="mvcOptions">The MVC options.</param>
    /// <param name="jsonOptions">The json options.</param>
    /// <returns></returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> mvcOptions, Action<MvcNewtonsoftJsonOptions> jsonOptions) => services.AppWebApiOptions(mvcOptions, null, jsonOptions);

    /// <summary>
    /// Adds the WebApi
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="routeOptions">The route options.</param>
    /// <returns></returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<RouteOptions> routeOptions) => services.AppWebApiOptions(null, routeOptions);

    /// <summary>
    /// Adds the WebApi
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="routeOptions">The route options.</param>
    /// <param name="jsonOptions">The json options.</param>
    /// <returns></returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<RouteOptions> routeOptions, Action<MvcNewtonsoftJsonOptions> jsonOptions) => services.AppWebApiOptions(null, routeOptions, jsonOptions);

    /// <summary>
    /// Adds the WebApi
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="mvcOptions">The MVC options.</param>
    /// <param name="routeOptions">The route options.</param>
    /// <returns></returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> mvcOptions, Action<RouteOptions> routeOptions) => services.AppWebApiOptions(mvcOptions, routeOptions);

    /// <summary>
    /// Adds the WebApi
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="mvcOptions">The MVC options.</param>
    /// <param name="routeOptions">The route options.</param>
    /// <param name="jsonOptions">The json options.</param>
    /// <returns></returns>
    public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> mvcOptions, Action<RouteOptions> routeOptions, Action<MvcNewtonsoftJsonOptions> jsonOptions) => services.AppWebApiOptions(mvcOptions, routeOptions, jsonOptions);

    /// <summary>
    /// Adds the json provider.
    /// </summary>
    /// <param name="mvcBuilder">The MVC builder.</param>
    /// <param name="jsonOptions">The json options.</param>
    /// <returns></returns>
    public static IMvcBuilder AddJsonProvider(this IMvcBuilder mvcBuilder, Action<MvcNewtonsoftJsonOptions> jsonOptions = null)
    {
        mvcBuilder.AddNewtonsoftJson(j =>
        {
            j.SerializerSettings.ContractResolver = JsonHelper.JsonSettings.ContractResolver;
            j.SerializerSettings.DateFormatHandling = JsonHelper.JsonSettings.DateFormatHandling;
            j.SerializerSettings.DateParseHandling = JsonHelper.JsonSettings.DateParseHandling;
            j.SerializerSettings.DateTimeZoneHandling = JsonHelper.JsonSettings.DateTimeZoneHandling;
            j.SerializerSettings.Formatting = JsonHelper.JsonSettings.Formatting;
            j.SerializerSettings.NullValueHandling = JsonHelper.JsonSettings.NullValueHandling;

            foreach (JsonConverter converter in JsonHelper.JsonSettings.Converters)
                j.SerializerSettings.Converters.Add(converter);

            jsonOptions?.Invoke(j);
        });
        return mvcBuilder;
    }

    private static IMvcBuilder AppWebApiOptions(this IServiceCollection services,
        Action<MvcOptions> mvcOptions = null,
        Action<RouteOptions> routeActions = null,
        Action<MvcNewtonsoftJsonOptions> jsonOptions = null,
        Action<IMvcBuilder> mvcBuilderAction = null)
    {
        IMvcBuilder mvcBuilder = services
            .AddRouting(options =>
            {
                options.AppendTrailingSlash = true;
                routeActions?.Invoke(options);
            })
            .AddMvc(config =>
            {
                mvcOptions?.Invoke(config);
            })
            .AddJsonProvider(jsonOptions);

        mvcBuilderAction?.Invoke(mvcBuilder);
        
        return mvcBuilder;
    }
}