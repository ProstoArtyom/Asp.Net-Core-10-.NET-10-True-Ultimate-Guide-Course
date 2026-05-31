using WebApplication1.Entities;
using WebApplication1.RepositoryContracts;
using WebApplication1.ServiceContracts;
using WebApplication1.ServiceContracts.DTOs;
using WebApplication1.Services.Helpers;

namespace WebApplication1.Services
{
 public class StocksService : IStocksService
 {
  private readonly IStocksRepository _stocksRepository;
  public StocksService(IStocksRepository stocksRepository)
  {
   _stocksRepository = stocksRepository;
  }

  public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
  {
   if (buyOrderRequest == null)
    throw new ArgumentNullException(nameof(buyOrderRequest));

   ValidationHelper.ModelValidation(buyOrderRequest);

   BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
   buyOrder.BuyOrderID = Guid.NewGuid();

   await _stocksRepository.CreateBuyOrder(buyOrder);

   return buyOrder.ToBuyOrderResponse();
  }

  public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
  {
   if (sellOrderRequest == null)
    throw new ArgumentNullException(nameof(sellOrderRequest));

   ValidationHelper.ModelValidation(sellOrderRequest);

   SellOrder sellOrder = sellOrderRequest.ToSellOrder();
   sellOrder.SellOrderID = Guid.NewGuid();

   await _stocksRepository.CreateSellOrder(sellOrder);

   return sellOrder.ToSellOrderResponse();
  }

  public async Task<List<BuyOrderResponse>> GetBuyOrders()
  {
   List<BuyOrder> buyOrders = await _stocksRepository.GetBuyOrders();
   return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
  }

  public async Task<List<SellOrderResponse>> GetSellOrders()
  {
   List<SellOrder> sellOrders = await _stocksRepository.GetSellOrders();
   return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
  }
 }
}