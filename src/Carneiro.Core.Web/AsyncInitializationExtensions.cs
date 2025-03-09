using System.Diagnostics;
using Carneiro.Core.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Carneiro.Core.Web;

/// <summary>
/// Extensions for <see cref="IAsyncInitializer"/>.
/// </summary>
public static class AsyncInitializationExtensions
{
    /// <summary>
    /// Starts the initialization process.
    /// </summary>
    /// <param name="serviceProvider"></param>
    public static async Task InitAsync(this IServiceProvider serviceProvider)
    {
        var executedInitializers = new List<Type>();

        ILogger<IAsyncInitializer> logger = serviceProvider.GetRequiredService<ILogger<IAsyncInitializer>>();
        IWebHostEnvironment environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        logger.LogInformation("Starting async initialization of '{EnvironmentApplicationName}'", environment.ApplicationName);

        while (true)
        {
            await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

            IAsyncInitializer initializer = scope.ServiceProvider.GetRequiredService<IEnumerable<IAsyncInitializer>>()
                .FirstOrDefault(i => !executedInitializers.Contains(i.GetType()));

            if (initializer is null)
            {
                break;
            }

            executedInitializers.Add(initializer.GetType());

            logger.LogInformation("Starting async initialization for {InitializerType}", initializer.GetType());
            try
            {
                var sw = Stopwatch.StartNew();
                await initializer.InitializeAsync();
                logger.LogInformation("Async initialization for {InitializerType} completed ({Duration} ms) ", initializer.GetType(), sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Async initialization for {InitializerType} failed", initializer.GetType());
                throw;
            }
        }

        logger.LogInformation("Async initialization of '{EnvironmentApplicationName}' completed", environment.ApplicationName);
    }
}