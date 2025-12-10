using System.Net;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/countries", async (HttpContext context) =>
{
    var countries = SD.ContriesDict.Select(u => $"{u.Key}, {u.Value}").ToList();
    await context.Response.WriteAsync(string.Join("\n", countries));
});

app.MapGet("/countries/{countryID:int:range(1, 100)}", async (HttpContext context) =>
{
    var countryID = Convert.ToInt32(context.Request.RouteValues["countryID"]);
    if (countryID > 5)
    {
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        await context.Response.WriteAsync("[No Country]");
    }

    await context.Response.WriteAsync(SD.ContriesDict[countryID]);
});

app.MapGet("/countries/{countryID}", async (HttpContext context) =>
{
    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    await context.Response.WriteAsync("The CountryID should be between 1 and 100");
});

app.Run();
