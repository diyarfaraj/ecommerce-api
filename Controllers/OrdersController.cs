using System.Linq;
using System.Threading.Tasks;
using ecommerceApi.Data;
using ecommerceApi.Entities.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly StoreContext _context;

        public OrdersController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Order>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Where(x => x.BuyerId == User.Identity.Name)
                .ToListAsync();
        }
    }
}
