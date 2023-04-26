using System;
using System.Collections.Generic;

namespace OrderServiceTest.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<OrderLine> Lines { get; set; }
    }
}