﻿using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace ThreeJs
{
    /// <summary>
    /// 隐藏接口，不生成到swagger文档展示  
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Class)]
    public partial class HiddenApiAttribute : System.Attribute { }
    /// <summary>
    /// 隐藏接口，不生成到swagger文档展示  
    /// </summary>
    public class HiddenApiFilter : IDocumentFilter
    {
        /// <summary>  
        /// 重写Apply方法，移除隐藏接口的生成  
        /// </summary>  
        /// <param name="swaggerDoc">swagger文档文件</param>  
        /// <param name="schemaRegistry"></param>  
        /// <param name="apiExplorer">api接口集合</param>  
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            foreach (ApiDescription apiDescription in apiExplorer.ApiDescriptions)
            {
                if (Enumerable.OfType<HiddenApiAttribute>(apiDescription.GetControllerAndActionAttributes<HiddenApiAttribute>()).Any())
                {
                    string key = "/" + apiDescription.RelativePath;
                    if (key.Contains("?"))
                    {
                        int idx = key.IndexOf("?", System.StringComparison.Ordinal);
                        key = key.Substring(0, idx);
                    }
                    swaggerDoc.paths.Remove(key);
                }
            }
        }
    }
}