using WebApplication2;
using WebApplication2.Models;
using WebApplication2.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection(nameof(TradingOptions)));

builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
