using Xunit;
using Microsoft.EntityFrameworkCore;
using ecommerceApi.Data;
using ecommerceApi.Entities;
using ecommerceApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Stripe;
using ecommerceApi.RequestHelpers;
using AutoMapper;

namespace ecommerceApi.UnitTests
{
    public class ProductsControllerTests
    {
        private readonly StoreContext _context;
        private readonly ProductsController _controller;
        private readonly Mapper _mapper;
        public ProductsControllerTests()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            _context = new StoreContext(options);
            _controller = new ProductsController(_context, _mapper);

            // Seed database with test data
            SeedTestData();
        }

        //[Fact]
        //public async Task Test_GetProducts_Returns_CorrectResults()
        //{
        //    // Arrange
        //    var productParams = new ProductParams { PageNumber = 1, ProductsPerPage = 2 };
        //    // Act
        //    var result = await _controller.GetProducts(productParams);
        //    var products = result.Value;

        //    // Assert
        //    Assert.Equal(2, products.Count);
        //    Assert.Equal(1, products[0].Id);
        //    Assert.Equal("Product 1", products[0].Name);
        //    Assert.Equal(2, products[1].Id);
        //    Assert.Equal("Product 2", products[1].Name);

        //}

            [Fact]
        public async Task Test_GetSingleProduct_Returns_CorrectProduct()
        {
            // Act
            var result = await _controller.GetSingleProduct(1);
            var product = result.Value;

            // Assert
            Assert.Equal(1, product.Id);
            Assert.Equal("Product 1", product.Name);
            Assert.Equal(100, product.Price);
        }

        [Fact]
        public async Task Test_GetSingleProduct_Returns_OkResult()
        {
            // Act
            var result = await _controller.GetSingleProduct(1);

            // Assert
            Assert.IsType<ActionResult<Entities.Product>>(result);
        }

        [Fact]
        public async Task Test_GetSingleProduct_Returns_NotFoundResult()
        {
            // Act
            var result = await _controller.GetSingleProduct(3);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        private void SeedTestData()
        {
            var res = _context.Products.Find(1);
            if (res != null)
            {
                return;
            }

            _context.Products.Add(new Entities.Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 100,
                Description = "This is a test product",
                ImgUrl = "https://testimage.com",
                Type = "Electronics",
                Brand = "Brand A",
                QuantityInStock = 10
            });
            _context.Products.Add(new Entities.Product
            {
                Id = 2,
                Name = "Product 2",
                Price = 200,
                Description = "This is another test product",
                ImgUrl = "https://testimage2.com",
                Type = "Clothing",
                Brand = "Brand B",
                QuantityInStock = 20
            });
            _context.SaveChanges();
        }

    }
}
