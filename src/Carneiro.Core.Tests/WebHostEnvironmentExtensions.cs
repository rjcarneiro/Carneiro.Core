using Microsoft.AspNetCore.Hosting;

namespace Carneiro.Core.Tests;

/// <summary>
/// Extensions for <see cref="IWebHostEnvironment"/>.
/// </summary>
public static class WebHostEnvironmentExtensions
{
    /// <summary>
    /// Checks either the current <see cref="IWebHostEnvironment"/> is <see cref="WebHostEnvironmentConstants.Testing"/> or not.
    /// </summary>
    /// <param name="webHostEnvironment"></param>
    /// <returns></returns>
    public static bool IsTestingEnvironment(this IWebHostEnvironment webHostEnvironment) => webHostEnvironment.IsEnvironment(WebHostEnvironmentConstants.Testing);
}