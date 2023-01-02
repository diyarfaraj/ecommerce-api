using ecommerceApi.Data;
using ecommerceApi.DTOs;
using ecommerceApi.Extensions;
using ecommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ecommerceApi.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly PaymentService _paymentService;
        private readonly StoreContext _storeContext;

        public PaymentController(PaymentService paymentService, StoreContext storeContext)
        {
            _paymentService = paymentService;
            _storeContext = storeContext;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent()
        {
            var basket = await _storeContext.Baskets
                .RetrieveBasketWithItems(User.Identity.Name)
                .FirstOrDefaultAsync();

            if (basket == null) return NotFound();
            
            var intent = await _paymentService.CreateOrUpdatePaymentIntent(basket);
         
            if (intent == null) return BadRequest(new ProblemDetails { Title="Problem creating payment intent"});

            basket.PaymentIntentId = basket.PaymentIntentId ?? intent.Id;
            basket.ClientSecret = basket.ClientSecret ?? intent.ClientSecret;

            _storeContext.Update(basket);
            var result = await _storeContext.SaveChangesAsync() > 0;
            
            if(!result) return BadRequest(new ProblemDetails { Title = "Problem updating basket with intent" });

            return basket.MapBasketToDto();
        }
    }
}
