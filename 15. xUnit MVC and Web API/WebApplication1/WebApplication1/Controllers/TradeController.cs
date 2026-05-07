using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;
using WebApplication1.Models;
using WebApplication1.ServiceContracts;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        private readonly IConfiguration _configuration;
        public TradeController(IOptions<TradingOptions> tradingOptions, IConfiguration configuration,
            IFinnhubService finnhubService, IStocksService stocksService)
        {
            _tradingOptions = tradingOptions.Value;
            _configuration = configuration;

            _finnhubService = finnhubService;
            _stocksService = stocksService;
        }

        [Route("/")]
        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            var companyProfileDictionary = await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
            var stockQuoteDictionary = await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };

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
