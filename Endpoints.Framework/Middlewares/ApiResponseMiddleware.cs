using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Endpoints.Framework.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Endpoints.Framework.Middlewares
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate next;
        public ApiResponseMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext ctx)
        {
            using (var ms = new MemoryStream())
            {
                // 替换body的MemoryStream
                var originMs = ctx.Response.Body;
                ctx.Response.Body = ms;
                await next.Invoke(ctx);
                using (var reader = new StreamReader(ms))
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    string data = reader.ReadToEnd();
                    if (!data.IsJson())
                    {
                        data = $"\"{data}\"";
                    }
                    object returnObj = JsonConvert.DeserializeObject($"{{\"code\": \"{ ctx.Response.StatusCode }\", \"msg\": \"{ Enum.GetName(typeof(HttpStatusCode), ctx.Response.StatusCode) }\", \"data\": { data }}}");
                    ctx.Response.Body = originMs;
                    await HandleResponse(ctx, returnObj);
                }
            }
        }

        public async Task HandleResponse(HttpContext ctx, object data)
        {
            ctx.Response.ContentType = "application/json; charset=utf-8";
            await ctx.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }

    public static class ApiResponseExtensions
    {
        public static IApplicationBuilder UseApiResponse(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiResponseMiddleware>();
        }
    }
}
