using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WebApplication1.ServiceContracts;

namespace WebApplication1.Services
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

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            var Uri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}");
            return await GetResponseDictAsync(stockSymbol, Uri);
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            var Uri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}");
            return await GetResponseDictAsync(stockSymbol, Uri);
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
