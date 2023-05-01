using System;
using System.Threading.Tasks;
using OrdersBase.Models;

namespace OrdersBase;

public interface IOrderRepository
{
    public Task CreateOrderAsync(Order order);

    public Task UpdateOrderAsync(Guid orderId, OrderStatus status, OrderLine[] lines);

    public Task DeleteOrderAsync(Guid orderId);

    public Task<Order> GetOrderAsync(Guid orderId);
}