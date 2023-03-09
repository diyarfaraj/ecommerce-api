using ecommerceApi.Controllers;
using ecommerceApi.Data;
using ecommerceApi.DTOs;
using ecommerceApi.Entities;
using ecommerceApi.Entities.OrderAggregate;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ecommerceApi.Tests.Controllers
{
    public class OrdersControllerTests
    {
        [Fact]
        public async Task GetOrders_ReturnsExpectedListOfOrdersForUser()
        {
            // Arrange
            var context = A.Fake<StoreContext>();
            var controller = new OrdersController(context);
            var orders = new List<Order>()
            {
                new Order() { Id = 1, BuyerId = "user1" },
                new Order() { Id = 2, BuyerId = "user1" },
                new Order() { Id = 3, BuyerId = "user2" }
            };
            A.CallTo(() => context.Orders).ReturnsDbSet(orders);

            // Act
            var result = await controller.GetOrders();
            var okResult = result.Result as OkObjectResult;
            var orderDtos = okResult.Value as List<OrderDto>;

            // Assert
            Assert.NotNull(orderDtos);
            Assert.Equal(2, orderDtos.Count);
            Assert.True(orderDtos.All(o => o.BuyerId == "user1"));
        }

        [Fact]
        public async Task CreateOrder_CreatesOrderAndReturnsCreatedOrderId()
        {
            // Arrange
            var context = A.Fake<StoreContext>();
            var controller = new OrdersController(context);
            var basket = new Basket() { BuyerId = "user1", PaymentIntentId = "pi_123" };
            var productItem = new ProductItem() { Id = 1, Name = "Product 1", ImgUrl = "https://example.com/product1.jpg", Price = 100, QuantityInStock = 5 };
            var basketItem = new BasketItem() { ProductId = productItem.Id, Quantity = 2 };
            basket.Items.Add(basketItem);
            A.CallTo(() => context.Baskets).ReturnsDbSet(new List<Basket>() { basket });
            A.CallTo(() => context.Products.FindAsync(productItem.Id)).Returns(productItem);
            A.CallTo(() => context.SaveChangesAsync()).Returns(1);

            // Act
            var orderDto = new CreateOrderDto()
            {
                ShippingAdress = new AddressDto() { FullName = "John Doe", Address1 = "123 Main St", City = "New York", Country = "USA", Zip = "10001", State = "NY" },
                SaveAddress = true
            };
            var result = await controller.CreateOrder(orderDto);
            var createdAtRouteResult = result.Result as CreatedAtRouteResult;

            // Assert
            Assert.NotNull(createdAtRouteResult);
            Assert.Equal("GetOrder", createdAtRouteResult.RouteName);
            Assert.Equal(basket.PaymentIntentId, (createdAtRouteResult.RouteValues["id"] as int?).Value);
        }
    }
}
