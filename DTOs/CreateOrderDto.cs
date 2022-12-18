using ecommerceApi.Entities.OrderAggregate;

namespace ecommerceApi.DTOs
{
    public class CreateOrderDto
    {
        public bool SaveAddress { get; set; }
        public ShippingAdress ShippingAdress { get; set; }


    }
}
