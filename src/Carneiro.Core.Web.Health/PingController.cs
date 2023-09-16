namespace Carneiro.Core.Web.Ping;

/// <summary>
/// Ping Controller
/// </summary>
/// <seealso cref="ControllerBase" />
[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    private readonly VersionModel _versionModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="PingController" /> class.
    /// </summary>
    /// <param name="versionModel">The version view model.</param>
    public PingController(VersionModel versionModel) => _versionModel = versionModel;

    /// <summary>
    /// Gets this instance.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(VersionModel), StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(_versionModel);
}