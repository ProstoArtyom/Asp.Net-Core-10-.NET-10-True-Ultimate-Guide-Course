using WebApplication1.Entities;

namespace WebApplication1.Contracts.Interfaces.IRepositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetWithItemsAsync(Guid id);
        Task<IEnumerable<Order>> GetAllWithItemsAsync();
        Task<OrderItem> GetOrderItemByIdAsync(Guid orderId, Guid itemId);
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync(Guid orderId);
        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(Guid orderId, Guid itemId);
    }
}
