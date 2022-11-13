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
        }
    }
}
