using System;
using System.Runtime.Serialization;

namespace OrderServiceApp.ViewModels
{
    [DataContract]
    public class OrderLineViewModel
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "qty")]
        public int Qty { get; set; }
    }
}