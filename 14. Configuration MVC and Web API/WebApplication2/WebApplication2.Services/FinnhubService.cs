using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WebApplication2.Services.Contracts;

namespace WebApplication2.Models
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfileAsync(string stockSymbol)
        {
            var Uri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}");
            var responseDict = await GetResponseDictAsync(stockSymbol, Uri);
            return responseDict;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuoteAsync(string stockSymbol)
        {
            var Uri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}");
            var responseDict = await GetResponseDictAsync(stockSymbol, Uri);
            return responseDict;
        }

        private async Task<Dictionary<string, object>?> GetResponseDictAsync(string stockSymbol, Uri uri)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.GetAsync(uri);
            var json = await httpResponseMessage.Content.ReadAsStringAsync();

            var responseDict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            if (responseDict == null)
                throw new InvalidOperationException("No response from server");

            if (responseDict.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDict["error"]));

            return responseDict;
        }
    }
}
