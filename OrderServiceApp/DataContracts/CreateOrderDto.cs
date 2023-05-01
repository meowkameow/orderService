using System;
using System.Linq;
using System.Runtime.Serialization;
using OrdersBase.Models;

namespace OrderServiceApp.DataContracts
{
    [DataContract]
    public class CreateOrderDto
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "lines")]
        public OrderLineDto[] Lines { get; set; }

        public Order ToObject()
        {
            return new Order
            {
                Id = this.Id,
                Lines = this.Lines.Select(x => new OrderLine()
                {
                    Id = x.Id,
                    Qty = x.Qty,
                }).ToArray(),
            };
        }
    }
}