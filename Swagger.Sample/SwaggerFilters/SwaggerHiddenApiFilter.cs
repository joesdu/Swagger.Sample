﻿using Microsoft.OpenApi.Models;
using Swagger.Sample.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Swagger.Sample.SwaggerFilters;

/// <summary>
/// 在Swagger文档中隐藏接口
/// </summary>
// ReSharper disable once UnusedMember.Global
// ReSharper disable once ClassNeverInstantiated.Global
public sealed class SwaggerHiddenApiFilter : IDocumentFilter
{
    /// <summary>
    /// Apply
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
#pragma warning disable IDE0048
            if (!apiDescription.TryGetMethodInfo(out var method) || (!method.ReflectedType!.IsDefined(typeof(HiddenApiAttribute)) && !method.IsDefined(typeof(HiddenApiAttribute))))
#pragma warning restore IDE0048
                continue;
            var key = $"/{apiDescription.RelativePath}";
            if (key.Contains('?'))
            {
                var index = key.IndexOf("?", StringComparison.Ordinal);
                key = key[..index];
            }
            _ = swaggerDoc.Paths.Remove(key);
        }
    }
}