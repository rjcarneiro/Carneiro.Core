using System.Security.Claims;
using System.Security.Principal;

namespace Carneiro.Core.Web.Extensions;

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

    /// <summary>
    /// Gets the <see cref="Claim"/> value of <see cref="ClaimTypes.Role"/>.
    /// </summary>
    /// <param name="principal"></param>
    public static string GetRole(this IPrincipal principal) => principal.GetClaim(ClaimTypes.Role);

    /// <summary>
    /// Gets the <see cref="Claim"/> value of <see cref="ClaimTypes.Role"/>.
    /// </summary>
    /// <param name="identity"></param>
    public static string GetRole(this IIdentity identity) => identity.GetClaim(ClaimTypes.Role);

    /// <summary>
    /// Gets the <see cref="Claim"/> value of <see cref="ClaimTypes.Role"/>.
    /// </summary>
    /// <param name="principal"></param>
    /// <exception cref="KeyNotFoundException">Case <see cref="ClaimTypes.Role"/> does not exist.</exception>
    public static string GetRole(this ClaimsPrincipal principal) => principal.GetClaim(ClaimTypes.Role);

    /// <summary>
    /// Gets a <paramref name="claim"/> value. Default throws an exception case the claim does not exist.
    /// </summary>
    /// <param name="principal"></param>
    /// <param name="claim"></param>
    /// <param name="throwIfNotFound"></param>
    /// <exception cref="KeyNotFoundException">Case <paramref name="claim"/> does not exist and <paramref name="throwIfNotFound"/> is set to <c>true</c>.</exception>
    public static string GetClaim(this IPrincipal principal, string claim, bool throwIfNotFound = true) => principal.Identity.GetClaim(claim, throwIfNotFound);

    /// <summary>
    /// Gets a <paramref name="claim"/> value. Default throws an exception case the claim does not exist.
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="claim"></param>
    /// <param name="throwIfNotFound"></param>
    /// <exception cref="KeyNotFoundException">Case <paramref name="claim"/> does not exist and <paramref name="throwIfNotFound"/> is set to <c>true</c>.</exception>
    public static string GetClaim(this IIdentity identity, string claim, bool throwIfNotFound = true) => ((ClaimsIdentity)identity).GetClaim(claim, throwIfNotFound);

    /// <summary>
    /// Gets a <paramref name="claim"/> value. Default throws an exception case the claim does not exist.
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="claim"></param>
    /// <param name="throwIfNotFound"></param>
    /// <exception cref="KeyNotFoundException">Case <paramref name="claim"/> does not exist and <paramref name="throwIfNotFound"/> is set to <c>true</c>.</exception>
    public static string GetClaim(this ClaimsIdentity identity, string claim, bool throwIfNotFound = true)
    {
        Claim value = identity.FindFirst(claim);

        if (value is null && throwIfNotFound)
        {
            throw new KeyNotFoundException(claim);
        }

        return value?.Value;
    }
}