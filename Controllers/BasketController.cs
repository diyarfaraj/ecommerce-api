using System;
using System.Linq;
using System.Threading.Tasks;
using ecommerceApi.Data;
using ecommerceApi.DTOs;
using ecommerceApi.Entities;
using ecommerceApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;

        public BasketController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet(Name ="GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) return NotFound();
            return basket.MapBasketToDto();
        }

      

        [HttpPost] //api/basket?productId=3&quantity=2
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) basket =  CreateBasket();

            var product = await _context.Products.FindAsync(productId);
            if (product == null) return BadRequest(new ProblemDetails { Title = "Product not found"});

            basket.AddItem(product, quantity);
            
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());
            return BadRequest(new ProblemDetails { Title="Problem saving item to basket"});
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteItemFromBasket(int productId, int quantity)
        {
            //get basket
            var basket = await RetrieveBasket(GetBuyerId());
            if(basket == null) return NotFound();
            //remove item or reduce
            basket.RemoveItem(productId, quantity);
            //save changes
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();
            return BadRequest(new ProblemDetails { Title="Error removing from basket"});
        }

        private async Task<Basket> RetrieveBasket(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }
            var result = await _context.Baskets
                .Include(basket => basket.Items)
                .ThenInclude(p => p.Product)//.FirstOrDefaultAsync();
                .FirstOrDefaultAsync(x => x.BuyerId == buyerId);

            return result;
        }

        private string GetBuyerId()
        {
            return User.Identity?.Name ?? Request.Cookies["buyerId"];
        }


        private Basket CreateBasket()
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();

                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }
          
            
            //the only value thats needed for creating new basket is buyerId.
             //Id is created incremently, and new empty list of items is created automatically
            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;
        }

    }
}
