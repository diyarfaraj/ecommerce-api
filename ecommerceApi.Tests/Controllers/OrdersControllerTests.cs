using ecommerceApi.Controllers;
using ecommerceApi.Data;
using ecommerceApi.DTOs;
using ecommerceApi.Entities;
using ecommerceApi.Entities.OrderAggregate;
using ecommerceApi.Extensions;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ecommerceApi.Tests.Controllers
{
    public class OrdersControllerTests
    {
        [Fact]
        public async Task CreateOrder_ReturnsBadRequest_WhenCalledWithEmptyDto()
        {
            // Arrange
            var fakeContext = A.Fake<StoreContext>();
            var controller = new OrdersController(fakeContext);

            //A.CallTo(() => fakeContext.Baskets.RetrieveBasketWithItems(A<string>._)).Return(Task.FromResult<Basket>(null));

            // Act
            var result = await controller.CreateOrder(new CreateOrderDto());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Could not locate basket", badRequestResult.Value);
        }
    }
}
