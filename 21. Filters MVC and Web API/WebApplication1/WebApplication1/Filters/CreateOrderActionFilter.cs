using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.ServiceContracts.DTOs;

namespace WebApplication1.Filters
{
    public class CreateOrderActionFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is not TradeController tradeController)
            {
                await next();
                return;
            }

            if (context.ActionArguments["orderRequest"] is not IOrderRequest orderRequest)
            {
                await next();
                return;
            }

            orderRequest.DateAndTimeOfOrder = DateTime.Now;

            tradeController.ModelState.Clear();
            tradeController.TryValidateModel(orderRequest);

            if (!tradeController.ModelState.IsValid)
            {
                tradeController.ViewBag.Errors = tradeController.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                StockTrade stockTrade = new StockTrade
                {
                    StockName = orderRequest.StockName,
                    Quantity = orderRequest.Quantity,
                    StockSymbol = orderRequest.StockSymbol
                };

                context.Result = tradeController.View("Index", stockTrade);
                return;
            }

            await next();
        }
    }
}
