namespace Carneiro.Core.Logging;

/// <summary>
/// Logging Extensions for <see cref="IHostBuilder"/>.
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    /// Adds <c>Serilog</c>.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public static IHostBuilder UseLogging(this IHostBuilder builder) => builder.UseLogging(loggingBuilder: null, action: null);

    /// <summary>
    /// Adds <c>Serilog</c>.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="loggingBuilder"></param>
    public static IHostBuilder UseLogging(this IHostBuilder builder, Action<ILoggingBuilder> loggingBuilder) => builder.UseLogging(loggingBuilder: loggingBuilder, action: null);

    /// <summary>
    /// Adds <c>Serilog</c>.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="action"></param>
    public static IHostBuilder UseLogging(this IHostBuilder builder, Action<LoggerConfiguration> action) => builder.UseLogging(loggingBuilder: null, action: action);

    /// <summary>
    /// Adds <c>Serilog</c> and overrides with <paramref name="action"/>.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="loggingBuilder"></param>
    /// <param name="action">The action.</param>
    public static IHostBuilder UseLogging(this IHostBuilder builder, Action<ILoggingBuilder> loggingBuilder, Action<LoggerConfiguration> action)
    {
        builder.ConfigureLogging((context, lb) =>
        {
            lb.ClearProviders();
            lb.AddConfiguration(context.Configuration.GetSection("Logging"));
            lb.AddSerilog();

            loggingBuilder?.Invoke(lb);
        });

        builder.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration);

            action?.Invoke(loggerConfiguration);
        });

        return builder;
    }

    /// <summary>
    /// Adds <c>Serilog</c>.
    /// </summary>
    /// <param name="builder"></param>
    public static IHostApplicationBuilder UseLogging(this IHostApplicationBuilder builder) => builder.UseLogging(action: null);

    /// <summary>
    /// Adds <c>Serilog</c> and overrides with <paramref name="action"/>.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="action"></param>
    public static IHostApplicationBuilder UseLogging(this IHostApplicationBuilder builder, Action<LoggerConfiguration> action) => builder.UseLogging(loggingBuilder: null, action);

    /// <summary>
    /// Adds <c>Serilog</c> and overrides with <paramref name="loggingBuilder"/>.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="loggingBuilder"></param>
    public static IHostApplicationBuilder UseLogging(this IHostApplicationBuilder builder, Action<ILoggingBuilder> loggingBuilder) => builder.UseLogging(loggingBuilder: loggingBuilder, action: null);

    /// <summary>
    /// Adds <c>Serilog</c> and overrides with <paramref name="action"/>.
    /// </summary>
    /// <param name="hostApplicationBuilder"></param>
    /// <param name="loggingBuilder"></param>
    /// <param name="action"></param>
    public static IHostApplicationBuilder UseLogging(this IHostApplicationBuilder hostApplicationBuilder, Action<ILoggingBuilder> loggingBuilder, Action<LoggerConfiguration> action)
    {
        hostApplicationBuilder.Logging
            .ClearProviders()
            .AddConfiguration(hostApplicationBuilder.Configuration.GetSection("Logging"))
            .AddSerilog();

        loggingBuilder?.Invoke(hostApplicationBuilder.Logging);

        hostApplicationBuilder.Services.AddSerilog(loggerConfiguration =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostApplicationBuilder.Configuration);

            action?.Invoke(loggerConfiguration);
        });

        return hostApplicationBuilder;
    }
}