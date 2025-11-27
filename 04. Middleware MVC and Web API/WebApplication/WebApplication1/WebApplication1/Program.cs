using WebApplication1.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMyCustomMiddleware();

app.Run(async context => {
    await context.Response.WriteAsync("No response");
});

app.Run();
