using System;
using System.Threading.Tasks;
using OrdersBase.Models;

namespace OrdersBase
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(Order order);

        public Task<Order> UpdateOrderAsync(Guid orderId, Order updateOrder);

        public Task DeleteOrderAsync(Guid orderId);

        public Task<Order> GetOrderAsync(Guid orderId);
    }
}