using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrdersBase;
using OrdersBase.Models;

namespace OrderEfRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _orderContext;

        public OrderRepository(OrderContext orderContext)
        {
            this._orderContext = orderContext;
        }
        public async Task CreateOrderAsync(Order order)
        {
            this._orderContext.Orders.Add(order);
            await this._orderContext.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Guid orderId, OrderStatus status, OrderLine[] newLinesForOrder)
        {
            var order = await this._orderContext.Orders
                .Include(x => x.Lines)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new Exception($"Order {orderId} not found!");
            }
            
            var lines = order.Lines.ToList();
            var existLineIds = lines.Select(x => x.Id).ToArray();
            var updateLineIds = newLinesForOrder.Select(x => x.Id).ToArray();
            var updateLines = newLinesForOrder.Select(x => new OrderLine
            {
                Id = x.Id,
                Qty = x.Qty,
                OrderId = order.Id,
                Order = order,
            }).ToArray();

            var linesToAdd = updateLines.Where(x => !existLineIds.Contains(x.Id)).ToArray();
            var linesToUpdate = updateLines.Where(x => existLineIds.Contains(x.Id)).ToArray();
            var linesToRemove = lines.Where(x => !updateLineIds.Contains(x.Id)).ToArray();

            foreach (var lineToAdd in linesToAdd)
            {
                lines.Add(lineToAdd);
            }

            foreach (var lineToUpdate in linesToUpdate)
            {
                var line = lines.First(x => x.Id == lineToUpdate.Id);
                line.Qty = lineToUpdate.Qty;
            }

            foreach (var lineToRemove in linesToRemove)
            {
                var line = lines.First(x => x.Id == lineToRemove.Id);
                lines.Remove(line);
            }

            order.Status = status;
            order.Lines = lines;

            await this._orderContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await this._orderContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new Exception($"Order {orderId} not found!");
            }

            this._orderContext.Remove(order);
            await this._orderContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            var order = await this._orderContext.Orders
                .Include(x => x.Lines)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new Exception($"Order {orderId} not found!");
            }

            return order;
        }
    }
}