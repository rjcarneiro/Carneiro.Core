using System.Security.Claims;

namespace Carneiro.Core.Web;

/// <summary>
/// Extensions for <see cref="ClaimsPrincipal"/>.
/// </summary>
public static class ClaimsExtensions
{
    /// <summary>
    /// Determines whether the specified principal is authenticated.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns>
    ///   <c>true</c> if the specified principal is authenticated; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsAuthenticated(this ClaimsPrincipal principal) => principal.Identity?.IsAuthenticated ?? false;
}