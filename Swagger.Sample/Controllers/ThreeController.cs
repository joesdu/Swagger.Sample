using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swagger.Sample.Attributes;

namespace Swagger.Sample.Controllers;

/// <summary>
/// ThreeController
/// </summary>
[Route("api/[controller]/[action]"), ApiController, ApiGroup("GroupOne", "v1", "第一个分组")]
public class ThreeController : ControllerBase
{
    /// <summary>
    /// ThreeHello
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public string ThreeHello() => "Hello GroupOne";

    /// <summary>
    /// ThreeAuthorize
    /// </summary>
    /// <returns></returns>
    [HttpGet, Authorize]
    public string ThreeAuthorize() => "Hello Authorize";

    /// <summary>
    /// ThreeHidden
    /// </summary>
    /// <returns></returns>
    [HttpGet, HiddenApi]
    public string ThreeHidden() => "Hello Hidden";
}