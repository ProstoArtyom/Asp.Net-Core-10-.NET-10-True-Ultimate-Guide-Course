using Microsoft.AspNetCore.Http;
using WebApplication2.Helpers;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        var isValid = true;
        int firstNumber = 0, secondNumber = 0;
        if (context.Request.Query.ContainsKey("firstNumber"))
        {
            if (!int.TryParse(context.Request.Query["firstNumber"], out firstNumber))
            {
                isValid = false;
                context.Response.SetStatusCode(400);
                await context.Response.WriteAsync(ValidationHelper.GetErrorMessage(nameof(firstNumber)));
            }
        }
        else
        {
            isValid = false;
            context.Response.SetStatusCode(400);
            await context.Response.WriteAsync(ValidationHelper.GetErrorMessage(nameof(firstNumber)));
        }

        if (context.Request.Query.ContainsKey("secondNumber"))
        {
            if (!int.TryParse(context.Request.Query["secondNumber"], out secondNumber))
            {
                isValid = false;
                context.Response.SetStatusCode(400);
                await context.Response.WriteAsync(ValidationHelper.GetErrorMessage(nameof(secondNumber)));
            }
        }
        else
        {
            isValid = false;
            context.Response.SetStatusCode(400);
            await context.Response.WriteAsync(ValidationHelper.GetErrorMessage(nameof(secondNumber)));
        }

        var operation = context.Request.Query["operation"].ToString();
        if (string.IsNullOrWhiteSpace(operation) || !ValidationHelper.Operations.Contains(operation))
        {
            isValid = false;
            context.Response.SetStatusCode(400);
            await context.Response.WriteAsync(ValidationHelper.GetErrorMessage(nameof(operation)));
        }

        if (!isValid) return;

        double result = operation switch
        {
            "add" => firstNumber + (double)secondNumber,
            "subtract" => firstNumber - (double)secondNumber,
            "multiply" => firstNumber * (double)secondNumber,
            "divide" => (secondNumber != 0) ? firstNumber / (double)secondNumber : 0,
            "mod" => result = (secondNumber != 0) ? firstNumber % (double)secondNumber : 0
        };

        await context.Response.WriteAsync($"{firstNumber} {operation} {secondNumber} equals {result}.\n");
    }
});

app.Run();