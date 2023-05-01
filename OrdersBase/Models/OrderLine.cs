using System;

namespace OrdersBase.Models
{
    public class OrderLine
    {
        public Guid Id { get; set; }

        public int Qty { get; set; }

        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}