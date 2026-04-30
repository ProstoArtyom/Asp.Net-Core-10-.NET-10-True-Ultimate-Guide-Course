using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;
using WebApplication2.Models;
using WebApplication2.Services.Contracts;

namespace WebApplication2.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;
        public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
            _configuration = configuration;
        }

        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public async Task<IActionResult> IndexAsync()
        {
            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
                _tradingOptions.DefaultStockSymbol = "MSFT";

            Dictionary<string, object>? companyProfileDictionary = await _finnhubService.GetCompanyProfileAsync(_tradingOptions.DefaultStockSymbol);
            Dictionary<string, object>? stockQuoteDictionary = await _finnhubService.GetStockPriceQuoteAsync(_tradingOptions.DefaultStockSymbol);

            var stockTrade = new StockTrade
            {
                StockSymbol = _tradingOptions.DefaultStockSymbol
            };

            if (companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade() 
                { 
                    StockSymbol = Convert.ToString(companyProfileDictionary["ticker"], CultureInfo.InvariantCulture), 
                    StockName = Convert.ToString(companyProfileDictionary["name"], CultureInfo.InvariantCulture), 
                    Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString(), CultureInfo.InvariantCulture)
                };
            }

            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }
    }
}
