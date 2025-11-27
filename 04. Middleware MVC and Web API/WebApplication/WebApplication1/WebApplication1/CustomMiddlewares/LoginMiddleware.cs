using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
using WebApplication1.Helpers;

namespace WebApplication1.CustomMiddlewares
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/" && context.Request.Method == "POST")
            {
                var reader = new StreamReader(context.Request.Body);
                string body = await reader.ReadToEndAsync();

                Dictionary<string, StringValues> queryDict = QueryHelpers.ParseQuery(body); 

                var isValid = true;
                var messages = new List<string>();

                if (!queryDict.ContainsKey("email"))
                {
                    messages.Add("Invalid input for 'email'");
                    context.Response.SetStatusCode(400);
                    isValid = false;
                }

                if (!queryDict.ContainsKey("password"))
                {
                    messages.Add("Invalid input for 'password'");
                    context.Response.SetStatusCode(400);
                    isValid = false;
                }

                if (isValid)
                {
                    var email = queryDict["email"];
                    var password = queryDict["password"];

                    if (string.Equals(email, SD.Email) && string.Equals(password, SD.Password))
                    {
                        messages.Add("Successful login");
                    }
                    else
                    {
                        messages.Add("Invalid login");
                        context.Response.SetStatusCode(400);
                    }
                }

                await context.Response.WriteAsync(string.Join('\n', messages));
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
