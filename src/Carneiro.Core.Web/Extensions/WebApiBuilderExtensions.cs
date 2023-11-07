namespace Carneiro.Core.Web.Extensions;

/// <summary>
/// <see cref="WebApiBuilderExtensions"/> Extensions.
/// </summary>
public static class WebApiBuilderExtensions
{
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

    /// <summary>
    /// Adds the default WebApi settings.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="mvcBuilder"></param>
    /// <param name="mvcOptions"></param>
    /// <param name="jsonOptions"></param>
    /// <param name="routeOptions"></param>
    /// <param name="apiBehaviorOptions"></param>
    public static IMvcBuilder AddWebApi(this IServiceCollection services,
        Action<IMvcBuilder> mvcBuilder = null,
        Action<MvcOptions> mvcOptions = null,
        Action<MvcNewtonsoftJsonOptions> jsonOptions = null,
        Action<RouteOptions> routeOptions = null,
        Action<ApiBehaviorOptions> apiBehaviorOptions = null)
    {
        IMvcBuilder builder = services
            .AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new StringTrimModelBinderProvider());
                mvcOptions?.Invoke(options);
            })
            .AddJsonProvider(jsonOptions);

        mvcBuilder?.Invoke(builder);

        services.AddRouting(options =>
        {
            options.AppendTrailingSlash = true;
            options.LowercaseQueryStrings = false;
            options.LowercaseUrls = true;

            routeOptions?.Invoke(options);
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context => new ValidationFailedResult(context.ModelState);
            apiBehaviorOptions?.Invoke(options);
        });

        return builder;
    }
}