using System;
using System.Linq;
using System.Threading.Tasks;
using OrdersBase;
using OrdersBase.Models;

namespace OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            if (!ValidateOrderLines(order.Lines.ToArray(), out var error))
            {
                throw new Exception(error);
            }

            var newOrder = new Order
            {
                Id = order.Id,
                Created = DateTime.UtcNow,
                Status = OrderStatus.New,
                Lines = order.Lines.Select(x => new OrderLine
                {
                    Id = x.Id,
                    Qty = x.Qty,
                }).ToArray(),
            };

            await this._orderRepository.CreateOrderAsync(newOrder);

            return newOrder;
        }

        public async Task<Order> UpdateOrderAsync(Guid orderId, Order updateOrder)
        {
            if (!ValidateOrderLines(updateOrder.Lines.ToArray(), out var error))
            {
                throw new Exception(error);
            }

            var order = await this._orderRepository.GetOrderAsync(orderId);

            if (order == null)
            {
                throw new Exception($"Order {orderId} not found!");
            }

            if (order.Status != OrderStatus.New && order.Status != OrderStatus.WaitingForPayment)
            {
                throw new Exception($"Order with status {order.Status} cant be updated!");
            }
            
            await this._orderRepository.UpdateOrderAsync(orderId, updateOrder.Status, updateOrder.Lines.ToArray());
            
            return order;
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await this._orderRepository.GetOrderAsync(orderId);

            if (order == null)
            {
                throw new Exception($"Order {orderId} not found!");
            }

            await this._orderRepository.DeleteOrderAsync(orderId);
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            var order = await this._orderRepository.GetOrderAsync(orderId);

            if (order == null)
            {
                throw new Exception($"Order {orderId} not found!");
            }

            if (order.Status == OrderStatus.InDelivery || order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Completed)
            {
                throw new Exception($"Order with status {order.Status} cant be deleted!");
            }

            return order;
        }

        private bool ValidateOrderLines(OrderLine[] lines, out string error)
        {
            if (lines == null || lines.Length == 0)
            {
                error = "Order must have lines.";
                return false;
            }

            if (lines.Any(x => x.Qty <= 0))
            {
                error = "Order line must be greater then 0.";
                return false;
            }

            error = String.Empty;
            return true;
        }
    }
}