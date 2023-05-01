using System.Linq;
using System.Runtime.Serialization;
using OrdersBase.Models;

namespace OrderServiceApp.DataContracts
{
    [DataContract]
    public class UpdateOrderDto
    {
        [DataMember(Name = "status")]
        public OrderStatus Status { get; set; }


        [DataMember(Name = "lines")]
        public OrderLineDto[] Lines { get; set; }

        public Order ToObject()
        {
            return new Order
            {
                Status = this.Status,
                Lines = this.Lines.Select(x => new OrderLine()
                {
                    Id = x.Id,
                    Qty = x.Qty,
                }).ToArray(),
            };
        }
    }
}