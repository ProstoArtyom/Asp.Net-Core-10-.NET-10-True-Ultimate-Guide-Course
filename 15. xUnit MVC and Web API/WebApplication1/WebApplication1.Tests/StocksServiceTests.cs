using WebApplication1.ServiceContracts;
using WebApplication1.ServiceContracts.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Tests
{
    public class StocksServiceTests
    {
        private readonly IStocksService _stocksService;
        public StocksServiceTests()
        {
            _stocksService = new StocksService();
        }

        private BuyOrderRequest GetValidBuyOrderRequest()
        {
            return new BuyOrderRequest
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("01-01-2001")
            };
        }

        private SellOrderRequest GetValidSellOrderRequest()
        {
            return new SellOrderRequest
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("01-01-2001")
            };
        }

        #region CreateBuyOrder

        // When you supply BuyOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrderRequest_ToBeArgumentNullException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_QuantityEqualsZero_ToBeArgumentException(uint buyOrderQuantity)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = GetValidBuyOrderRequest();
            buyOrderRequest.Quantity = buyOrderQuantity;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Theory]
        [InlineData(100001)]
        public async Task CreateBuyOrder_QuantityGreaterMaximum_ToBeArgumentException(uint buyOrderQuantity)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = GetValidBuyOrderRequest();
            buyOrderRequest.Quantity = buyOrderQuantity;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply buyOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_PriceEqualsZero_ToBeArgumentException(uint buyOrderPrice)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = GetValidBuyOrderRequest();
            buyOrderRequest.Price = buyOrderPrice;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply buyOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Theory]
        [InlineData(10001)]
        public async Task CreateBuyOrder_PriceGreaterMaximum_ToBeArgumentException(uint buyOrderPrice)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = GetValidBuyOrderRequest();
            buyOrderRequest.Price = buyOrderPrice;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_NullStockSymbol_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = GetValidBuyOrderRequest();
            buyOrderRequest.StockSymbol = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Theory]
        [InlineData("1999-12-31")]
        public async Task CreateBuyOrder_InvalidDateAndTimeOfOrder_ToBeArgumentException(DateTime dateAndTimeOfOrder)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = GetValidBuyOrderRequest();
            buyOrderRequest.DateAndTimeOfOrder = dateAndTimeOfOrder;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // If you supply all valid values, it should be successful and return an object of BuyOrderResponse type with auto-generated BuyOrderID (guid).
        [Fact]
        public async Task CreateBuyOrder_ValidData()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = GetValidBuyOrderRequest();

            // Act
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            // Assert
            Assert.NotEqual(Guid.Empty, buyOrderResponse.BuyOrderID);
        }

        #endregion


        #region CreateSellOrder

        // When you supply SellOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public async Task CreateSellOrder_NullBuyOrderRequest_ToBeArgumentNullException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_QuantityEqualsZero_ToBeArgumentException(uint sellOrderQuantity)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = GetValidSellOrderRequest();
            sellOrderRequest.Quantity = sellOrderQuantity;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // When you supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_QuantityGreaterMaximum_ToBeArgumentException(uint sellOrderQuantity)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = GetValidSellOrderRequest();
            sellOrderRequest.Quantity = sellOrderQuantity;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // When you supply sellOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_PriceEqualsZero_ToBeArgumentException(uint sellOrderPrice)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = GetValidSellOrderRequest();
            sellOrderRequest.Price = sellOrderPrice;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // When you supply sellOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_PriceGreaterMaximum_ToBeArgumentException(uint sellOrderPrice)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = GetValidSellOrderRequest();
            sellOrderRequest.Price = sellOrderPrice;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_NullStockSymbol_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = GetValidSellOrderRequest();
            sellOrderRequest.StockSymbol = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Theory]
        [InlineData("1999-12-31")]
        public async Task CreateSellOrder_InvalidDateAndTimeOfOrder_ToBeArgumentException(DateTime dateAndTimeOfOrder)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = GetValidSellOrderRequest();
            sellOrderRequest.DateAndTimeOfOrder = dateAndTimeOfOrder;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // If you supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID (guid).
        [Fact]
        public async Task CreateSellOrder_ValidData()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = GetValidSellOrderRequest();

            // Act
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            // Assert
            Assert.NotEqual(Guid.Empty, sellOrderResponse.SellOrderID);
        }

        #endregion


        #region GetAllBuyOrders

        // When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public async Task GetAllBuyOrders_DefaultList_ToBeEmpty()
        {
            // Act
            List<BuyOrderResponse> buyOrderResponseList = await _stocksService.GetBuyOrders();

            // Assert
            Assert.Empty(buyOrderResponseList);
        }

        // When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; the returned list should contain all the same buy orders.
        [Fact]
        public async Task GetAllBuyOrders_WithFewBuyOrders_ToBeSuccessful()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("01-01-2001")
            };

            BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 12,
                Quantity = 12,
                DateAndTimeOfOrder = DateTime.Parse("03-03-2003")
            };

            List<BuyOrderRequest> buyOrderRequestList = [buyOrderRequest1, buyOrderRequest2];
            List<BuyOrderResponse> buyOrderResponsesFromAdd = []; 
            foreach (var buyOrderRequest in buyOrderRequestList)
            {
                BuyOrderResponse buyOrderResponseFromAdd = await _stocksService.CreateBuyOrder(buyOrderRequest);
                buyOrderResponsesFromAdd.Add(buyOrderResponseFromAdd);
            }

            // Act
            List<BuyOrderResponse> buyOrderResponsesFromGet = await _stocksService.GetBuyOrders();

            // Assert
            foreach (BuyOrderResponse buyOrderResponseFromAdd in buyOrderResponsesFromAdd)
            {
                Assert.Contains(buyOrderResponseFromAdd, buyOrderResponsesFromGet);
            }
        }

        #endregion


        #region GetAllSellOrders

        // When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public async Task GetAllSellOrders_DefaultList_ToBeEmpty()
        {
            // Act
            List<SellOrderResponse> sellOrderResponseList = await _stocksService.GetSellOrders();

            // Assert
            Assert.Empty(sellOrderResponseList);
        }

        // When you first add few sell orders using CreateSellOrder() method; and then invoke GetAllSellOrders() method; the returned list should contain all the same sell orders.
        [Fact]
        public async Task GetAllSellOrders_WithFewSellOrders_ToBeSuccessful()
        {
            // Arrange
            SellOrderRequest sellOrderRequest1 = new SellOrderRequest
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("01-01-2001")
            };

            SellOrderRequest sellOrderRequest2 = new SellOrderRequest
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 12,
                Quantity = 12,
                DateAndTimeOfOrder = DateTime.Parse("03-03-2003")
            };

            List<SellOrderRequest> sellOrderRequestList = [sellOrderRequest1, sellOrderRequest2];
            List<SellOrderResponse> sellOrderResponsesFromAdd = [];
            foreach (var sellOrderRequest in sellOrderRequestList)
            {
                SellOrderResponse sellOrderResponseFromAdd = await _stocksService.CreateSellOrder(sellOrderRequest);
                sellOrderResponsesFromAdd.Add(sellOrderResponseFromAdd);
            }

            // Act
            List<SellOrderResponse> sellOrderResponsesFromGet = await _stocksService.GetSellOrders();

            // Assert
            foreach (SellOrderResponse sellOrderResponseFromAdd in sellOrderResponsesFromAdd)
            {
                Assert.Contains(sellOrderResponseFromAdd, sellOrderResponsesFromGet);
            }
        }

        #endregion
    }
}
