using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using PhotoCloud.Utility;
using PhotoCloud.Model.Core;

namespace PhotoCloud.WebAPI.Filter
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                Log.WriteException(context.Exception);
                var message = context.Exception.Message;
                if (context.Exception.InnerException != null)
                    message = $"{message} -- {context.Exception.InnerException.Message}";
                ResultModel result = new ResultModel
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = message
                };
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonSerializer.Serialize(result)
                };
            }
            context.ExceptionHandled = true;
        }
    }
}
