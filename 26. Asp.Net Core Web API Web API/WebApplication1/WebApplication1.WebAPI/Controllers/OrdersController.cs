using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.DTOs;
using WebApplication1.Contracts.Interfaces.IRepositories;

namespace WebApplication1.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders()
        {
            var orders = await _orderRepository.GetAllWithItemsAsync();
            var orderResponseList = orders.Select(temp => temp.ToOrderResponse()).ToList();
            return orderResponseList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrderById(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetWithItemsAsync(id);
                return order.ToOrderResponse();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] OrderAddRequest orderAddRequest)
        {
            var order = orderAddRequest.ToOrder();
            var orderFromAdd = await _orderRepository.AddAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderFromAdd.Id }, orderFromAdd.ToOrderResponse());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderResponse>> PutOrder(Guid id, [FromBody] OrderUpdateRequest orderUpdateRequest)
        {
            try
            {
                var order = orderUpdateRequest.ToOrder(id);
                await _orderRepository.UpdateAsync(order);
                return order.ToOrderResponse();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                await _orderRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{orderId}/items")]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetOrderItems(Guid orderId)
        {
            try
            {
                var orderItemList = await _orderRepository.GetAllOrderItemsAsync(orderId);
                return orderItemList
                    .Select(temp => temp.ToOrderItemResponse())
                    .ToList();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{orderId}/items/{itemId}")]
        public async Task<ActionResult<OrderItemResponse>> GetOrderItemById(Guid orderId, Guid itemId)
        {
            try
            {
                var orderItem = await _orderRepository.GetOrderItemByIdAsync(orderId, itemId);
                return orderItem.ToOrderItemResponse();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{orderId}/items")]
        public async Task<ActionResult<OrderItemResponse>> PostOrderItem(Guid orderId,
            [FromBody] OrderItemAddRequest orderItemAddRequest)
        {
            try
            {
                var orderItem = orderItemAddRequest.ToOrderItem(orderId);
                var orderItemFromAdd = await _orderRepository.AddOrderItemAsync(orderItem);

                return CreatedAtAction(
                    nameof(GetOrderItemById), 
                    new { orderId = orderItemFromAdd.OrderId, itemId = orderItemFromAdd.Id },
                    orderItemFromAdd.ToOrderItemResponse()
                );
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{orderId}/items/{itemId}")]
        public async Task<ActionResult<OrderItemResponse>> PutOrderItem(Guid orderId, Guid itemId, 
            [FromBody] OrderItemUpdateRequest orderItemUpdateRequest)
        {
            try
            {
                var orderItem = orderItemUpdateRequest.ToOrderItem(orderId, itemId);
                await _orderRepository.UpdateOrderItemAsync(orderItem);
                return orderItem.ToOrderItemResponse();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{orderId}/items/{itemId}")]
        public async Task<IActionResult> DeleteOrderItem(Guid orderId, Guid itemId)
        {
            try
            {
                await _orderRepository.DeleteOrderItemAsync(orderId, itemId);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
