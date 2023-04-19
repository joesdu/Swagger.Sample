using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

// ReSharper disable ClassNeverInstantiated.Global

namespace Swagger.Sample.Controllers;

/// <summary>
/// ��һ��������
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
/// Put����ģ��
/// </summary>
public class PutParameter
{
    /// <summary>
    /// Gender
    /// </summary>
    public EGender Gender { get; set; }
}

/// <summary>
/// POST����ģ��
/// </summary>
public class PostParameter : PutParameter
{
    /// <summary>
    /// Name
    /// </summary>
    [DefaultValue("����")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Age
    /// </summary>
    [DefaultValue(20)]
    public int Age { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    [DefaultValue(typeof(DateTime), "2023-04-01")]
    public DateTime Birthday { get; set; } = DateTime.Now;
}

/// <summary>
/// �Ա�ö��
/// </summary>
public enum EGender
{
    /// <summary>
    /// ��
    /// </summary>
    ��,

    /// <summary>
    /// Ů
    /// </summary>
    Ů
}