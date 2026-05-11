using WebApplication1.DataAccess;
using WebApplication1.Entities;
using WebApplication1.ServiceContracts;
using WebApplication1.ServiceContracts.DTOs;
using WebApplication1.Services.Helpers;

namespace WebApplication1.Services
{
    public class StocksService : IStocksService
    {
        private readonly StockMarketDbContext _dbContext;
        public StocksService(StockMarketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderID = Guid.NewGuid();

            await _dbContext.BuyOrders.AddAsync(buyOrder);
            await _dbContext.SaveChangesAsync();

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderID = Guid.NewGuid();

            await _dbContext.SellOrders.AddAsync(sellOrder);
            await _dbContext.SaveChangesAsync();

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return _dbContext.BuyOrders
                .Select(x => x.ToBuyOrderResponse())
                .ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return _dbContext.SellOrders
                .Select(x => x.ToSellOrderResponse())
                .ToList();
        }
    }
}
