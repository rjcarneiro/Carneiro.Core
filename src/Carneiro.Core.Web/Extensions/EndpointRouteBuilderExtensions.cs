namespace Carneiro.Core.Web.Extensions;

/// <summary>
/// Extensions for <see cref="IEndpointRouteBuilder"/>. 
/// </summary>
public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps the ping request as <c>GET</c> using <see cref="PingOptions"/>.
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapPing(this IEndpointRouteBuilder endpoints)
    {
        PingOptions pingOptions = endpoints.ServiceProvider.GetRequiredService<IOptions<PingOptions>>().Value 
                                  ?? new PingOptions();

        endpoints.MapGet(pingOptions.Route, (VersionModel versionModel) => pingOptions.ShowVersion ? Results.Ok(versionModel) : Results.NoContent())
            .AllowAnonymous();
        return endpoints;
    }
}