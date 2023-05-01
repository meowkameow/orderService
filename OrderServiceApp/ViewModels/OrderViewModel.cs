using System;
using System.Linq;
using System.Runtime.Serialization;
using OrdersBase.Models;

namespace OrderServiceApp.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "status")]
        public OrderStatus Status { get; set; }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }


        [DataMember(Name = "lines")]
        public OrderLineViewModel[] Lines { get; set; }

        public static OrderViewModel FromObject(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                Status = order.Status,
                Created = order.Created,
                Lines = order.Lines.Select(x => new OrderLineViewModel()
                {
                    Id = x.Id,
                    Qty = x.Qty,
                }).ToArray(),
            };
        }
    }
}