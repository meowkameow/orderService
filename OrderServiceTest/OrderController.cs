using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderServiceTest.DataContracts;
using OrderServiceTest.Models;

namespace OrderServiceTest
{

    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }
        
        [HttpPost()]
        public async Task<ActionResult<OrderDc>> CreateOrder([FromBody] OrderDc order)
        {
            try
            {
                return await orderService.CreateOrderAsync(order);
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
        public async Task<ActionResult<OrderDc>> UpdateOrder([FromRoute] Guid orderId, [FromBody] OrderDc updateOrder)
        {
            try
            {
                return await orderService.UpdateOrderAsync(orderId, updateOrder);
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
                await orderService.DeleteOrderAsync(orderId);
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
        public async Task<ActionResult<OrderDc>> GetOrder([FromRoute] Guid orderId)
        {
            try
            {
                return await orderService.GetOrderAsync(orderId);
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