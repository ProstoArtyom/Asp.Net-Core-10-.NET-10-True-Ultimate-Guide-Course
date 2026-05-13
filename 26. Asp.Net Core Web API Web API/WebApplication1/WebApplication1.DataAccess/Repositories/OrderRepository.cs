using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts.Interfaces.IRepositories;
using WebApplication1.Entities;

namespace WebApplication1.DataAccess.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly DbSet<OrderItem> _orderItemsDb;
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _orderItemsDb = dbContext.OrderItems;
        }

        public async Task<Order> GetWithItemsAsync(Guid id)
        {
            var order = await _db.AsNoTracking()
                .Include(temp => temp.OrderItems)
                .FirstOrDefaultAsync(temp => temp.Id == id);

            if (order == null)
                throw new KeyNotFoundException($"Order with id {id} not found");

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllWithItemsAsync()
        {
            return await _db.AsNoTracking()
                .Include(temp => temp.OrderItems)
                .ToListAsync();
        }

        public override async Task UpdateAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var existing = await _db
                .Include(temp => temp.OrderItems)
                .FirstOrDefaultAsync(temp => temp.Id == order.Id);

            if (existing == null)
                throw new KeyNotFoundException($"Order with id {order.Id} not found");
            
            _orderItemsDb.RemoveRange(existing.OrderItems);

            existing.CustomerName = order.CustomerName;
            existing.OrderDate = order.OrderDate;
            existing.OrderNumber = order.OrderNumber;
            existing.TotalAmount = order.OrderItems.Sum(i => i.TotalPrice);

            await _orderItemsDb.AddRangeAsync(order.OrderItems);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(Guid orderId, Guid itemId)
        {
            var isOrderExist = await ExistsAsync(orderId);
            if (!isOrderExist)
                throw new KeyNotFoundException($"Order with id {orderId} not found");

            var orderItem = await _orderItemsDb.AsNoTracking()
                .Where(temp => temp.OrderId == orderId)
                .FirstOrDefaultAsync(temp => temp.Id == itemId);

            if (orderItem == null)
                throw new KeyNotFoundException($"Order item with id {itemId} not found");

            return orderItem;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync(Guid orderId)
        {
            return await _orderItemsDb.AsNoTracking()
                .Where(temp => temp.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            if (orderItem.OrderId == Guid.Empty)
                throw new ArgumentException(nameof(orderItem.OrderId));

            var isOrderExist = await ExistsAsync(orderItem.OrderId);
            if (!isOrderExist)
                throw new KeyNotFoundException($"Order with id {orderItem.OrderId} not found");

            await _orderItemsDb.AddAsync(orderItem);
            await _dbContext.SaveChangesAsync();

            return orderItem;
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            var existingItem = await _orderItemsDb.FindAsync(orderItem.Id);
            if (existingItem == null || existingItem.OrderId != orderItem.OrderId)
                throw new KeyNotFoundException($"Order item with id {orderItem.Id} not found");

            _dbContext.Entry(existingItem).CurrentValues.SetValues(orderItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(Guid orderId, Guid itemId)
        {
            var orderItemToDelete = await _orderItemsDb.FindAsync(itemId);
            if (orderItemToDelete == null || orderItemToDelete.OrderId != orderId)
                throw new KeyNotFoundException($"Order item with id {itemId} not found");

            _orderItemsDb.Remove(orderItemToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
