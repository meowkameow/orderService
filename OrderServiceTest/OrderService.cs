using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderServiceTest.DataContracts;
using OrderServiceTest.Models;

namespace OrderServiceTest;

public class OrderService
{
    public static int counter = 0;
    private readonly OrderContext orderContext;
    
    public OrderService(OrderContext orderContext)
    {
        counter++;
        Console.WriteLine(counter);
        this.orderContext = orderContext;
    }
        public async Task<OrderDc> CreateOrderAsync(OrderDc order)
        {
            if (!ValidateOrderLines(order.Lines, out var error))
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

            this.orderContext.Orders.Add(newOrder);
            await this.orderContext.SaveChangesAsync();


            return OrderDc.FromObject(newOrder);
        }
        
        public async Task<OrderDc> UpdateOrderAsync(Guid orderId, OrderDc updateOrder)
        {
            if (!ValidateOrderLines(updateOrder.Lines, out var error))
            {
                throw new Exception(error);
            }
            
            

            var order = await this.orderContext.Orders
                .Include(x => x.Lines)
                .FirstOrDefaultAsync(x => x.Id == orderId && (x.Status == OrderStatus.New || x.Status == OrderStatus.WaitingForPayment));

            if (order == null)
            {
                throw new Exception($"Order {orderId} not found!");
            }

           

            var existLineIds = order.Lines.Select(x => x.Id).ToArray();
            var updateLineIds = updateOrder.Lines.Select(x => x.Id).ToArray();
            
            var updateLines = updateOrder.Lines.Select(x => new OrderLine
            {
                Id = x.Id,
                Qty = x.Qty,
                OrderId = order.Id,
                Order = order,
            }).ToArray();

            var linesToAdd = updateLines.Where(x => !existLineIds.Contains(x.Id)).ToArray();
            var linesToUpdate = updateLines.Where(x => existLineIds.Contains(x.Id)).ToArray();
            var linesToRemove = order.Lines.Where(x => !updateLineIds.Contains(x.Id)).ToArray();

            foreach (var lineToAdd in linesToAdd)
            {
                order.Lines.Add(lineToAdd);
             
            }
           
            foreach (var lineToUpdate in linesToUpdate)
            {
                var line =  order.Lines.First(x => x.Id == lineToUpdate.Id);
                line.Qty = lineToUpdate.Qty;
            }

            foreach (var lineToRemove in linesToRemove)
            {
                var line =  order.Lines.First(x => x.Id == lineToRemove.Id);
                order.Lines.Remove(line);   
            }

            order.Status = updateOrder.Status;

            await this.orderContext.SaveChangesAsync();


            return OrderDc.FromObject(order);
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            
            var order = await this.orderContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new Exception($"Order {orderId} not found!");
            }
            
            this.orderContext.Remove(order);
            await this.orderContext.SaveChangesAsync();
        }
        
        public async Task<OrderDc> GetOrderAsync(Guid orderId)
        {
            
            var order = await this.orderContext.Orders
                .Include(x => x.Lines)
                .FirstOrDefaultAsync(x => x.Id == orderId
                && x.Status != OrderStatus.InDelivery
                && x.Status != OrderStatus.Delivered
                && x.Status != OrderStatus.Completed);

            if (order == null)
            {
                throw new Exception($"Order {orderId} not found!");
            }
            
            return OrderDc.FromObject(order);
        }

        private bool ValidateOrderLines(OrderLineDc[] lines, out string error)
        {
            if (lines == null || lines.Length == 0)
            {
                
                error =  "Order must have lines.";
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