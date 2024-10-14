using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Core.Servecies.Contract;
using System.Text;

namespace Store.APIs.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTime;

        public CachedAttribute(int expireTime)
        {
            _expireTime = expireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


                var cachService=context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GenerateCachKeyFromRequest(context.HttpContext.Request);

               var cachResponse=  await cachService.GetCacheKeyAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cachResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result= contentResult;
                return;
            }


              var executedContext= await next();
            if (executedContext.Result is OkObjectResult response)
            {
                cachService.SetCacheKeyAsync(cacheKey, response, TimeSpan.FromSeconds(_expireTime));

            }


        }



        private string GenerateCachKeyFromRequest(HttpRequest request)
        {

            var cachKey = new StringBuilder();
            cachKey.Append($"{request.Path}");

            foreach (var (key,value) in request.Query.OrderBy(X=>X.Key))
            {
                cachKey.Append($"| {key} - {value} |");
            }

            return cachKey.ToString();
        }
    }
}
