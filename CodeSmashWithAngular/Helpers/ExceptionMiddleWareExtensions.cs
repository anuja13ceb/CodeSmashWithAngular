

using CodeSmashWithAngular.Error;

namespace CodeSmashWithAngular.Helpers
{
    public class ExceptionMiddleWareExtensions
    {
        private readonly RequestDelegate _next;
        

        public ExceptionMiddleWareExtensions(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                 httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync(ex.Message);
            }
            
        }
        
    }
    //public static class MiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<ExceptionMiddleWareExtensions>();
    //    }
    //}
}
