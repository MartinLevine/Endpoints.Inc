using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Endpoints.Framework.Authentication.Hooks
{
    public class PrefixHook
    {
        public static Task GetHook(MessageReceivedContext ctx, string prefix)
        {
            ctx.Token = ctx.Request.Headers["Authorization"];

            // token为空，直接返回
            if (string.IsNullOrEmpty(ctx.Token))
            {
                ctx.NoResult();
                return Task.CompletedTask;
            }

            if (ctx.Token.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                ctx.Token = ctx.Token.Substring(prefix.Length).Trim();
            }

            if (string.IsNullOrEmpty(ctx.Token))
            {
                ctx.NoResult();
            }

            return Task.CompletedTask;
        }
    }
}
