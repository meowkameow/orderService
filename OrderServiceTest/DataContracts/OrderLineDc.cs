using System;
using System.Runtime.Serialization;

namespace OrderServiceTest.DataContracts;

[DataContract]
public class OrderLineDc
{
    [DataMember(Name = "id")]
    public Guid Id { get; set; }
    
    [DataMember(Name = "qty")]
    public int Qty { get; set; }
}