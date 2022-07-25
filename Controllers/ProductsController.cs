﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerceApi.Data;
using ecommerceApi.Entities;
using ecommerceApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {   
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(string orderBy, string searchTerm, string brands, string types)
        {
            var query =  _context.Products
                .Sort(orderBy)
                .Search(searchTerm)
                .Filter(brands, types)
                .AsQueryable();
            

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetSingleProduct(int id)
        {
            var product= await _context.Products.FindAsync(id); 
          if (product == null) return NotFound();
          return Ok(product);
        }

    }
}
