using System;
using System.Runtime.Serialization;

namespace OrderServiceApp.DataContracts
{
    [DataContract]
    public class OrderLineDto
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "qty")]
        public int Qty { get; set; }
    }
}