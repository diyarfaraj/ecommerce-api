using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommerceApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ecommerceApiTest.Controllers
{
    public class BuggyControllerTests
    {
        private readonly BuggyController _controller;

        public BuggyControllerTests()
        {
            _controller = new BuggyController();
        }

        [Fact]
        public void GetNotFound_ReturnsNotFoundResult()
        {
            // Arrange

            // Act
            var result = _controller.GetNotFound();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetBadRequest_ReturnsBadRequestObjectResult()
        {
            // Arrange

            // Act
            var result = _controller.GetBadRequest();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetUnAuthorised_ReturnsUnauthorizedResult()
        {
            // Arrange

            // Act
            var result = _controller.GetUnAuthorised();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        //[Fact]
        //public void GetValidationError_ReturnsValidationProblemResult()
        //{
        //    // Arrange

        //    // Act
        //    var result = _controller.GetValidationError();

        //    // Assert
        //    Assert.IsType<ValidationProblemResult>(result);
        //}

        [Fact]
        public void GetServerError_ThrowsException()
        {
            // Arrange

            // Act
            Action act = () => _controller.GetServerError();

            // Assert
            Assert.Throws<Exception>(act);
        }

    }
}
