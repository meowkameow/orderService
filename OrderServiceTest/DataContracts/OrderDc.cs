using System;
using System.Linq;
using System.Runtime.Serialization;
using OrderServiceTest.Models;

namespace OrderServiceTest.DataContracts;

[DataContract]
public class OrderDc
{
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [DataMember(Name = "status")]
    public OrderStatus Status { get; set; }
    
    [DataMember(Name = "created")]
    public DateTime Created { get; set; }
    
    
    [DataMember(Name = "lines")]
    public OrderLineDc[] Lines { get; set; }

    public static OrderDc FromObject(Order order)
    {
        return new OrderDc
        {
        Id = order.Id,
        Status = order.Status,
        Created = order.Created,
        Lines = order.Lines.Select(x => new OrderLineDc()
        {
         Id   = x.Id,
         Qty = x.Qty,
        }).ToArray(),
        };
    }
}