using Microsoft.AspNetCore.Mvc;
using Swagger.Sample.Attributes;

namespace Swagger.Sample.Controllers;

/// <summary>
/// TwoController
/// </summary>
[Route("api/[controller]/[action]"), ApiController, ApiGroup("GroupOne", "v1", "第一个分组")]
public class TwoController : ControllerBase
{
    /// <summary>
    /// TwoHello
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public string TwoHello() => "Hello GroupOne";
}