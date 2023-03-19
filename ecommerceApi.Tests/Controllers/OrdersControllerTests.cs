using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ecommerceApi.Controllers;
using ecommerceApi.Data;
using ecommerceApi.DTOs;
using ecommerceApi.Entities.OrderAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ecommerceApi.Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly OrdersController _controller;
        private readonly StoreContext _context;

        public OrdersControllerTests()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new StoreContext(options);
            _controller = new OrdersController(_context);
        }

        [Fact]
        public async Task GetOrders_ReturnsOrdersForAuthenticatedUser()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
            }));

            _context.Orders.Add(new Order
            {
                BuyerId = "TestUser",
                OrderItems = new List<OrderItem> { new OrderItem() },
                SubTotal = 100,
                DeliveryFee = 10,
                OrderDate = DateTime.UtcNow, // Add this line
                ShippingAdress = new ShippingAdress // Add this line
                {
                    FullName = "John Doe",
                    Address1 = "123 Main St",
                    City = "New York",
                    Country = "USA",
                    Zip = "10001",
                    State = "NY"
                },
                OrderStatus = OrderStatus.Pending // Add this line
            });
            _context.Orders.Add(new Order
            {
                BuyerId = "AnotherUser",
                OrderItems = new List<OrderItem> { new OrderItem() },
                SubTotal = 200,
                DeliveryFee = 20,
                OrderDate = DateTime.UtcNow, // Add this line
                ShippingAdress = new ShippingAdress // Add this line
                {
                    FullName = "Jane Doe",
                    Address1 = "456 Main St",
                    City = "Los Angeles",
                    Country = "USA",
                    Zip = "90001",
                    State = "CA"
                },
                OrderStatus = OrderStatus.Pending // Add this line
            });
            await _context.SaveChangesAsync();

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            ActionResult<List<OrderDto>> result;
            try
            {
                result = await _controller.GetOrders();
            }
            catch (Exception ex)
            {
                // Print the exception details
                Console.WriteLine("Exception: " + ex);
                throw;
            }


            // Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<List<OrderDto>>>(result);
            var orders = Assert.IsType<List<OrderDto>>(actionResult.Value);
            Assert.Single(orders);
            Assert.Equal("TestUser", orders.First().BuyerId);
        }
    }
}
