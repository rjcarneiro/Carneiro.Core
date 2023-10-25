using System;
using Microsoft.Extensions.Hosting;
using Serilog;

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
    public static IHostBuilder UseLogging(this IHostBuilder builder) => builder.UseLogging(action: null);

    /// <summary>
    /// Adds <c>Serilog</c> and overrides with <paramref name="action"/>.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="action">The action.</param>
    public static IHostBuilder UseLogging(this IHostBuilder builder, Action<LoggerConfiguration> action)
    {
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
    /// <param name="hostApplicationBuilder"></param>
    public static HostApplicationBuilder UseLogging(this HostApplicationBuilder hostApplicationBuilder) => hostApplicationBuilder.UseLogging(action: null);

    /// <summary>
    /// Adds <c>Serilog</c> and overrides with <paramref name="action"/>.
    /// </summary>
    /// <param name="hostApplicationBuilder"></param>
    /// <param name="action"></param>
    public static HostApplicationBuilder UseLogging(this HostApplicationBuilder hostApplicationBuilder, Action<LoggerConfiguration> action)
    {
        hostApplicationBuilder.Services.AddSerilog(loggerConfiguration =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostApplicationBuilder.Configuration);

            action?.Invoke(loggerConfiguration);
        });

        hostApplicationBuilder.Logging.AddSerilog();

        return hostApplicationBuilder;
    }
}