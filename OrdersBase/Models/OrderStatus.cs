using System.Runtime.Serialization;

namespace OrdersBase.Models
{
    public enum OrderStatus
    {
        [EnumMember(Value = "New")]
        New = 0,
        [EnumMember(Value = "WaitingForPayment")]
        WaitingForPayment = 1,
        [EnumMember(Value = "Paid")]
        Paid = 2,
        [EnumMember(Value = "InDelivery")]
        InDelivery = 3,
        [EnumMember(Value = "Delivered")]
        Delivered = 4,
        [EnumMember(Value = "Completed")]
        Completed = 5
    }
}