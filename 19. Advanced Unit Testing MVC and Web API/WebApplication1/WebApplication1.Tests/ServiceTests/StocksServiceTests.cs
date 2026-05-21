using AutoFixture;
using FluentAssertions;
using Moq;
using WebApplication1.Entities;
using WebApplication1.Repositories;
using WebApplication1.RepositoryContracts;
using WebApplication1.ServiceContracts;
using WebApplication1.ServiceContracts.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Tests.ServiceTests
{
    public class StocksServiceTests
    {
        private readonly Mock<IStocksRepository> _stockRepositoryMock;
        private readonly IStocksService _stocksService;

        private readonly IFixture _fixture;
        public StocksServiceTests()
        {
            _stockRepositoryMock = new Mock<IStocksRepository>();
            _stocksService = new StocksService(_stockRepositoryMock.Object);

            _fixture = new Fixture();
        }

        #region CreateBuyOrder

        // When you supply BuyOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrderRequest_ToBeArgumentNullException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;

            // Mock
            var buyOrderFixture = _fixture.Create<BuyOrder>();
            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrderFixture);

            // Act
            var action = async () => 
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        // When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_QuantityEqualsZero_ToBeArgumentException(uint buyOrderQuantity)
        {
            // Arrange
            var buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, buyOrderQuantity)
                .Create();

            // Mock
            var buyOrderFixture = buyOrderRequest.ToBuyOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Theory]
        [InlineData(100001)]
        public async Task CreateBuyOrder_QuantityGreaterMaximum_ToBeArgumentException(uint buyOrderQuantity)
        {
            // Arrange
            var buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, buyOrderQuantity)
                .Create();

            // Mock
            var buyOrderFixture = buyOrderRequest.ToBuyOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply buyOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_PriceEqualsZero_ToBeArgumentException(uint buyOrderPrice)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, buyOrderPrice)
                .Create();

            // Mock
            var buyOrderFixture = buyOrderRequest.ToBuyOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply buyOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Theory]
        [InlineData(10001)]
        public async Task CreateBuyOrder_PriceGreaterMaximum_ToBeArgumentException(uint buyOrderPrice)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Price, buyOrderPrice)
                .Create();

            // Mock
            var buyOrderFixture = buyOrderRequest.ToBuyOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_NullStockSymbol_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            // Mock
            var buyOrderFixture = buyOrderRequest.ToBuyOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Theory]
        [InlineData("1999-12-31")]
        public async Task CreateBuyOrder_InvalidDateAndTimeOfOrder_ToBeArgumentException(DateTime dateAndTimeOfOrder)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, dateAndTimeOfOrder)
                .Create();

            // Mock
            var buyOrderFixture = buyOrderRequest.ToBuyOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply all valid values, it should be successful and return an object of BuyOrderResponse type with auto-generated BuyOrderID (guid).
        [Fact]
        public async Task CreateBuyOrder_ValidData()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>().Create();

            // Mock
            var buyOrderFixture = buyOrderRequest.ToBuyOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrderFixture);

            // Act
            BuyOrderResponse buyOrderResponseFromCreate = await _stocksService.CreateBuyOrder(buyOrderRequest);

            // Assert
            buyOrderFixture.BuyOrderID = buyOrderResponseFromCreate.BuyOrderID;
            BuyOrderResponse buyOrderResponseExpected = buyOrderFixture.ToBuyOrderResponse();
            buyOrderResponseFromCreate.BuyOrderID.Should().NotBeEmpty();
            buyOrderResponseFromCreate.Should().Be(buyOrderResponseExpected);
        }

        #endregion


        #region CreateSellOrder

        // When you supply SellOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public async Task CreateSellOrder_NullBuyOrderRequest_ToBeArgumentNullException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Mock
            var sellOrderFixture = _fixture.Create<SellOrder>();
            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        // When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_QuantityEqualsZero_ToBeArgumentException(uint sellOrderQuantity)
        {
            // Arrange
            var sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, sellOrderQuantity)
                .Create();

            // Mock
            var sellOrderFixture = sellOrderRequest.ToSellOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_QuantityGreaterMaximum_ToBeArgumentException(uint sellOrderQuantity)
        {
            // Arrange
            var sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, sellOrderQuantity)
                .Create();

            // Mock
            var sellOrderFixture = sellOrderRequest.ToSellOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply sellOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_PriceEqualsZero_ToBeArgumentException(uint sellOrderPrice)
        {
            // Arrange
            var sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, sellOrderPrice)
                .Create();

            // Mock
            var sellOrderFixture = sellOrderRequest.ToSellOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply sellOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_PriceGreaterMaximum_ToBeArgumentException(uint sellOrderPrice)
        {
            // Arrange
            var sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, sellOrderPrice)
                .Create();

            // Mock
            var sellOrderFixture = sellOrderRequest.ToSellOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_NullStockSymbol_ToBeArgumentException()
        {
            // Arrange
            var sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            // Mock
            var sellOrderFixture = sellOrderRequest.ToSellOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Theory]
        [InlineData("1999-12-31")]
        public async Task CreateSellOrder_InvalidDateAndTimeOfOrder_ToBeArgumentException(DateTime dateAndTimeOfOrder)
        {
            // Arrange
            var sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, dateAndTimeOfOrder)
                .Create();

            // Mock
            var sellOrderFixture = sellOrderRequest.ToSellOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrderFixture);

            // Act
            var action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID (guid).
        [Fact]
        public async Task CreateSellOrder_ValidData()
        {
            // Arrange
            var sellOrderRequest = _fixture.Create<SellOrderRequest>();

            // Mock
            var sellOrderFixture = sellOrderRequest.ToSellOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrderFixture);

            // Act
            var sellOrderResponseFromCreate = await _stocksService.CreateSellOrder(sellOrderRequest);

            // Assert
            sellOrderFixture.SellOrderID = sellOrderResponseFromCreate.SellOrderID;
            var sellOrderResponseFromExpected = sellOrderFixture.ToSellOrderResponse();
            sellOrderResponseFromCreate.SellOrderID.Should().NotBeEmpty();
            sellOrderResponseFromCreate.Should().Be(sellOrderResponseFromExpected);
        }

        #endregion


        #region GetAllBuyOrders

        // When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public async Task GetAllBuyOrders_DefaultList_ToBeEmpty()
        {
            // Arrange
            var buyOrderList = new List<BuyOrder>();

            // Mock
            _stockRepositoryMock.Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(buyOrderList);

            // Act
            var buyOrderResponseList = await _stocksService.GetBuyOrders();

            // Assert
            buyOrderResponseList.Should().BeEmpty();
        }

        // When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; the returned list should contain all the same buy orders.
        [Fact]
        public async Task GetAllBuyOrders_WithFewBuyOrders_ToBeSuccessful()
        {
            // Arrange
            var buyOrderListFixture = _fixture.Create<List<BuyOrder>>();
            var buyOrderResponsesExpected = buyOrderListFixture
                .Select(temp => temp.ToBuyOrderResponse())
                .ToList();

            // Mock
            _stockRepositoryMock.Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(buyOrderListFixture);

            // Act
            var buyOrderResponsesFromGet = await _stocksService.GetBuyOrders();

            // Assert
            buyOrderResponsesFromGet.Should().BeEquivalentTo(buyOrderResponsesExpected);
        }

        #endregion


        #region GetAllSellOrders

        // When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public async Task GetAllSellOrders_DefaultList_ToBeEmpty()
        {
            // Arrange
            var sellOrderList = new List<SellOrder>();

            // Mock
            _stockRepositoryMock.Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(sellOrderList);

            // Act
            var sellOrderResponseList = await _stocksService.GetSellOrders();

            // Assert
            sellOrderResponseList.Should().BeEmpty();
        }

        //// When you first add few sell orders using CreateSellOrder() method; and then invoke GetAllSellOrders() method; the returned list should contain all the same sell orders.
        [Fact]
        public async Task GetAllSellOrders_WithFewSellOrders_ToBeSuccessful()
        {
            // Arrange
            var sellOrderRequestListFixture = _fixture.Create<List<SellOrder>>();
            var sellOrderRequestListExpected = sellOrderRequestListFixture
                .Select(temp => temp.ToSellOrderResponse())
                .ToList();

            // Mock
            _stockRepositoryMock.Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(sellOrderRequestListFixture);

            // Act
            var sellOrderResponsesFromGet = await _stocksService.GetSellOrders();

            // Assert
            sellOrderResponsesFromGet.Should().BeEquivalentTo(sellOrderRequestListExpected);
        }

        #endregion
    }
}
