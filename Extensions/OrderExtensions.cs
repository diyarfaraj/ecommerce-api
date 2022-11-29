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
                    OrderStatus = order.OrderStatus.ToString(),
                    Total = order.GetTotal(),
                    SubTotal = order.SubTotal,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDto
                    {
                        ProductId = item.ItemOrdered.ProductId,
                        Name = item.ItemOrdered.Name,
                        PictureUrl = item.ItemOrdered.PictureUrl,
                        Price = item.Price,
                        Quantity = item.Quantity
                    }).ToList(),

                });
        }
    }
}
