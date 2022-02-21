using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var thereIsApiKeyName = context
                                    .HttpContext
                                    .Request.Query
                                    .TryGetValue(
                                        Configuration.ApiKeyName,
                                        out var extractecApiKey
                                    );
        if (!thereIsApiKeyName)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey não encontrada."
            };
            return;
        };
        var thereIsApiKey = Configuration.ApiKey.Equals(extractecApiKey);
        if (!thereIsApiKey)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Acesso não autorizado"
            };
            return;
        }
        await next();
    }
}