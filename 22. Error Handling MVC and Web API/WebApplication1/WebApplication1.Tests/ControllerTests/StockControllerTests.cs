using Microsoft.Extensions.Options;
using System.Collections;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using WebApplication1.Controllers;
using WebApplication1.ServiceContracts;
using WebApplication1.Models;

namespace WebApplication1.Tests.ControllerTests
{
    public class StockControllerTests
    {
        private readonly Mock<IFinnhubService> _finnhubServiceMock;
        private readonly IFinnhubService _finnhubService;

        private readonly IFixture _fixture;
        public StockControllerTests()
        {
            _finnhubServiceMock = new Mock<IFinnhubService>();
            _finnhubService = _finnhubServiceMock.Object;

            _fixture = new Fixture();
        }

        private List<Dictionary<string, string>> CreateMockStocksData()
        {
            return new List<Dictionary<string, string>>
            {
                new() { ["currency"] = "USD", ["description"] = "APPLE INC", ["displaySymbol"] = "AAPL", ["symbol"] = "AAPL" },
                new() { ["currency"] = "USD", ["description"] = "MICROSOFT CORP", ["displaySymbol"] = "MSFT", ["symbol"] = "MSFT" },
                new() { ["currency"] = "USD", ["description"] = "AMAZON.COM INC", ["displaySymbol"] = "AMZN", ["symbol"] = "AMZN" },
                new() { ["currency"] = "USD", ["description"] = "TESLA INC", ["displaySymbol"] = "TSLA", ["symbol"] = "TSLA" },
                new() { ["currency"] = "USD", ["description"] = "ALPHABET INC-CL A", ["displaySymbol"] = "GOOGL", ["symbol"] = "GOOGL" }
            };
        }

        #region Index

        [Fact]
        public async Task Explore_StockIsNull_ShouldReturnExploreViewWithStocksList()
        {
            //Arrange
            var tradingOptions = Options.Create(
                new TradingOptions
                {
                    DefaultOrderQuantity = 100, 
                    Top25PopularStocks = "AAPL,MSFT,AMZN,TSLA,GOOGL,GOOG,NVDA,BRK.B,META,UNH,JNJ,JPM,V,PG,XOM,HD,CVX,MA,BAC,ABBV,PFE,AVGO,COST,DIS,KO"
                });

            var stocksController = new StocksController(tradingOptions, _finnhubService);

            var stocksDict = CreateMockStocksData();

            var expectedStocks = stocksDict!
                .Select(temp => new Stock
                {
                    StockName = Convert.ToString(temp["description"]),
                    StockSymbol = Convert.ToString(temp["symbol"])
                })
                .ToList();

            // Mock
            _finnhubServiceMock
             .Setup(temp => temp.GetStocks())
             .ReturnsAsync(stocksDict);

            //Act
            var result = await stocksController.Explore(null, true);

            //Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<Stock>>();
            viewResult.ViewData.Model.Should().BeEquivalentTo(expectedStocks);
        }

        #endregion
    }
}
