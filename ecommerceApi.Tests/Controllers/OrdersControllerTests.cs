using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerceApi.Data;
using ecommerceApi.DTOs;
using ecommerceApi.Entities.OrderAggregate;
using ecommerceApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FakeItEasy;
using ecommerceApi.Controllers;
using ecommerceApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ecommerceApi.Tests
{
    public class OrdersControllerTests
    {
        private readonly IdentityDbContext<User, Role, int> _context;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _context = A.Fake<IdentityDbContext<User, Role, int>>();
            A.CallTo(() => _context.Set<Order>()).Returns(new List<Order>
        {
            new Order { Id = 1 },
            new Order { Id = 2 }
        }.AsQueryable());
            // Configure other DbSets here

            _controller = new OrdersController(_context);
        }
        [Fact]
        public async Task GetOrders_ReturnsListOfOrderDtos()
        {
            // Arrange
            var orders = new List<OrderDto>
            {
                new OrderDto { Id = 1 },
                new OrderDto { Id = 2 }
            };

            A.CallTo(() => _context.Orders.ProjectOrderToOrderDto())
                .Returns(orders.AsQueryable());

            // Act
            var result = await _controller.GetOrders();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<OrderDto>>>(result);
            var model = Assert.IsAssignableFrom<List<OrderDto>>(actionResult.Value);
            Assert.Equal(2, model.Count);
        }
    }
}