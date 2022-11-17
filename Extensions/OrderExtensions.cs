using System.Linq;
using ecommerceApi.DTOs;
using ecommerceApi.Entities.OrderAggregate;

namespace ecommerceApi.Extensions
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDto> ProjectOrderToOrderDto( this IQueryable<Order> query)
        {
            //map them props
            return query
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    BuyerId = order.BuyerId,
                    OrderDate = order.OrderDate,
                    ShippingAdress = order.ShippingAdress,
                    DeliveryFee = order.DeliveryFee,
                    OrderItems = order.OrderItems,

                });
        }
    }
}
