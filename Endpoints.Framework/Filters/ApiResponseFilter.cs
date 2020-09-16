using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Endpoints.Framework.Filters
{
    public class ApiResponseFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ModelStateEntry _unvalidEntry = context.ModelState.Values.First(item => item.Errors.Count > 0);
                throw new ApplicationException(_unvalidEntry.Errors[0].ErrorMessage);
            }
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.Filters.OfType<IgnoreApiResponseFilterAttribute>().Any())
            {
                if (context.Result != null)
                {
                    if (!(context.Result is IStatusCodeActionResult))
                    {
                        base.OnActionExecuted(context);
                        return;
                    }

                    IStatusCodeActionResult result = (IStatusCodeActionResult)context.Result;
                    int code = result.StatusCode ?? 200;
                    // 在System.Net这个命名空间下有一个枚举类 HttpStatusCode 其中定义了http返回状态码
                    string msg = Enum.GetName(typeof(HttpStatusCode), code);
                    object data = (context.Result as ObjectResult)?.Value == null ? "" : (context.Result as ObjectResult).Value;
                    context.Result = new JsonResult(new
                    {
                        code,
                        msg,
                        data
                    });
                    context.HttpContext.Response.StatusCode = code;
                }
            }
        }
    }

    public class IgnoreApiResponseFilterAttribute : Attribute, IFilterMetadata
    {

    }
}
