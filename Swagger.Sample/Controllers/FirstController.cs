using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

// ReSharper disable ClassNeverInstantiated.Global

namespace Swagger.Sample.Controllers;

/// <summary>
/// 第一个控制器
/// </summary>
[ApiController, Route("[controller]/[action]")]
public class FirstController : ControllerBase
{
    /// <summary>
    /// Hello {name}
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet]
    public string Hello(string name) => $"Hello {name}";

    /// <summary>
    /// Hello World
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public string HelloWorld() => "Hello World";

    /// <summary>
    /// PostOne
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    [HttpPost]
    public PostParameter PostOne(PostParameter parameter) => parameter;

    /// <summary>
    /// PutOne
    /// </summary>
    /// <param name="id"></param>
    /// <param name="put"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public string PutOne(string id, PutParameter put) => $"update:{id},{put.Gender}";

    /// <summary>
    /// DeleteOne
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public string DeleteOne(string id) => $"delete {id}";
}

/// <summary>
/// Put参数模拟
/// </summary>
public class PutParameter
{
    /// <summary>
    /// Gender
    /// </summary>
    public EGender Gender { get; set; }
}

/// <summary>
/// POST参数模拟
/// </summary>
public class PostParameter : PutParameter
{
    /// <summary>
    /// Name
    /// </summary>
    [DefaultValue("张三")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Age
    /// </summary>
    [DefaultValue(20)]
    public int Age { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    [DefaultValue(typeof(DateTime), "2023-04-01")]
    public DateTime Birthday { get; set; } = DateTime.Now;
}

/// <summary>
/// 性别枚举
/// </summary>
public enum EGender
{
    /// <summary>
    /// 男
    /// </summary>
    男,

    /// <summary>
    /// 女
    /// </summary>
    女
}