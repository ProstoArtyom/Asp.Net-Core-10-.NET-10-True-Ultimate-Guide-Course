using WebApplication1.Entities;

namespace WebApplication1.Contracts.DTOs
{
    public class OrderAddRequest
    {
        public string OrderNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemAddRequest> OrderItems { get; set; } = new();

        public Order ToOrder()
        {
            var orderId = Guid.NewGuid();
            return new Order
            {
                Id = orderId,
                OrderNumber = OrderNumber,
                CustomerName = CustomerName,
                OrderDate = OrderDate,
                TotalAmount = TotalAmount,
                OrderItems = OrderItems
                    .Select(temp => temp.ToOrderItem(orderId))
                    .ToList()
            };
        }
    }
}
