using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess;
using WebApplication1.Entities;
using WebApplication1.RepositoryContracts;

namespace WebApplication1.Repositories
{
 public class StocksRepository : IStocksRepository
 {
  private readonly ApplicationDbContext _db;
  public StocksRepository(ApplicationDbContext stockMarketDbContext)
  {
   _db = stockMarketDbContext;
  }

  public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
  {
   _db.BuyOrders.Add(buyOrder);
   await _db.SaveChangesAsync();

   return buyOrder;
  }

  public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
  {
   _db.SellOrders.Add(sellOrder);
   await _db.SaveChangesAsync();

   return sellOrder;
  }

  public async Task<List<BuyOrder>> GetBuyOrders()
  {
   List<BuyOrder> buyOrders = await _db.BuyOrders
    .OrderByDescending(temp => temp.DateAndTimeOfOrder)
    .ToListAsync();

   return buyOrders;
  }
  public async Task<List<SellOrder>> GetSellOrders()
  {
   List<SellOrder> sellOrders = await _db.SellOrders
    .OrderByDescending(temp => temp.DateAndTimeOfOrder)
    .ToListAsync();

   return sellOrders;
  }
 }
}


