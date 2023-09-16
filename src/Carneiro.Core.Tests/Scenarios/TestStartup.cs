using Microsoft.AspNetCore.Builder;

namespace Carneiro.Core.Tests.Scenarios;

/// <summary>
/// Simple startup class
/// </summary>
public class TestStartup
{
    /// <summary>
    /// Use this method to add services to the container.
    /// </summary>
    /// <param name="services">The services.</param>
    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    /// <summary>
    /// Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The application.</param>
    public virtual void Configure(IApplicationBuilder app)
    {
    }
}