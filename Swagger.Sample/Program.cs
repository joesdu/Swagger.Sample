using Swagger.Sample.Attributes;
using Swagger.Sample.SwaggerFilters;
using Swagger.Sample.Tools;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 配置Swagger
const string Title = "Test";                    // 文档标题
const string Version = "v1";                    // 版本
const string Name = $"{Title}-{Version}";       // 文档名称
Dictionary<string, string> docsDic = new();     // 用来存储文档分组的信息
Dictionary<string, string> endPointDic = new(); // 存储文档终结点json信息
builder.Services.AddSwaggerGen(c =>
{
    // 配置默认分组
    c.SwaggerDoc(Name, new()
    {
        Title = Title,
        Version = Version,
        Description = "Console.WriteLine(\"🐂🍺\")"
    });
    // 配置文档注释
    var files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
    foreach (var file in files)
    {
        c.IncludeXmlComments(file, true);
    }
    // 获取控制器分组信息和配置分组
    var controllers = AssemblyHelper.FindTypesByAttribute<ApiGroupAttribute>();
    foreach (var ctrl in controllers)
    {
        var attr = ctrl.GetCustomAttribute<ApiGroupAttribute>();
        if (attr is null) continue;
        if (docsDic.ContainsKey(attr.Name)) continue;
        _ = docsDic.TryAdd(attr.Name, attr.Description);
        c.SwaggerDoc(attr.Name, new()
        {
            Title = attr.Title,
            Version = attr.Version,
            Description = attr.Description
        });
    }
    c.DocInclusionPredicate((docName, apiDescription) =>
    {
        //反射拿到值
        var actionList = apiDescription.ActionDescriptor.EndpointMetadata.Where(x => x is ApiGroupAttribute).ToList();
        if (actionList.Count != 0)
        {
            return actionList.FirstOrDefault() is ApiGroupAttribute attr && attr.Name == docName;
        }
        //判断是否包含这个分组
        var not = apiDescription.ActionDescriptor.EndpointMetadata.Where(x => x is not ApiGroupAttribute).ToList();
        return not.Count != 0 && docName == Name;
    });
    c.OperationFilter<SwaggerAuthorizeFilter>(); // 为接口添加锁图标
    c.DocumentFilter<SwaggerHiddenApiFilter>();  // 添加隐藏接口过滤
    c.SchemaFilter<SwaggerSchemaFilter>();       // 添加默认值显示
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // 默认分组
        c.SwaggerEndpoint($"/swagger/{Name}/swagger.json", $"{Title} {Version}");
        // 配置使用ApiGroupAttribute的分组
        var controllers = AssemblyHelper.FindTypesByAttribute<ApiGroupAttribute>();
        foreach (var ctrl in controllers)
        {
            var attr = ctrl.GetCustomAttribute<ApiGroupAttribute>();
            if (attr is null) continue;
            if (endPointDic.ContainsKey(attr.Name)) continue;
            _ = endPointDic.TryAdd(attr.Name, attr.Description);
            c.SwaggerEndpoint($"/swagger/{attr.Name}/swagger.json", $"{attr.Title} {attr.Version}");
        }
    });
}
app.UseAuthorization();
app.MapControllers();
app.Run();