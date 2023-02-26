using System;
using System.Collections.Generic;

namespace ecommerceApi.Entities.OrderAggregate
{
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public ShippingAdress ShippingAdress { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderItem> OrderItems { get; set; }
        public long SubTotal { get; set; }
        public long DeliveryFee { get; set; } 
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public long GetTotal()
        {
            return SubTotal + DeliveryFee;
        }

    }
}
