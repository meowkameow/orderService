using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersBase;
using OrderServiceApp.DataContracts;
using OrderServiceApp.ViewModels;

namespace OrderServiceApp
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        
        [HttpPost()]
        public async Task<ActionResult<OrderViewModel>> CreateOrder([FromBody] CreateOrderDto order)
        {
            try
            {
                var newOrder =  await _orderService.CreateOrderAsync(order.ToObject());
                return OrderViewModel.FromObject(newOrder);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ExceptionDc(ex.Message))
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
        }
        
        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderViewModel>> UpdateOrder([FromRoute] Guid orderId, [FromBody] UpdateOrderDto updateOrder)
        {
            try
            {
                var updatedOrder =   await _orderService.UpdateOrderAsync(orderId, updateOrder.ToObject());
                return OrderViewModel.FromObject(updatedOrder);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ExceptionDc(ex.Message))
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder([FromRoute] Guid orderId)
        {
            try
            {
                await _orderService.DeleteOrderAsync(orderId);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ExceptionDc(ex.Message))
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
        }
        
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderViewModel>> GetOrder([FromRoute] Guid orderId)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(orderId);
                return OrderViewModel.FromObject(order);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ExceptionDc(ex.Message))
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
        }
    }
}