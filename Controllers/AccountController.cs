﻿using System.Threading.Tasks;
using ecommerceApi.Data;
using ecommerceApi.DTOs;
using ecommerceApi.Entities;
using ecommerceApi.Extensions;
using ecommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly StoreContext _context;

        public AccountController(UserManager<User> userManager, TokenService tokenService, StoreContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null ||
                !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized();
            }

            var userBasket = await RetrieveBasket(loginDto.Username);
            var anonymousBasket = await RetrieveBasket(Request.Cookies["buyerId"]);

            if(anonymousBasket != null)
            {
                if(userBasket != null)
                {
                    _context.Baskets.Remove(userBasket);
                }
                anonymousBasket.BuyerId = user.UserName;
                Response.Cookies.Delete("buyerId");
                await _context.SaveChangesAsync();
            }
            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                Basket = anonymousBasket != null ? anonymousBasket.MapBasketToDto() : userBasket?.MapBasketToDto()
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Member");
            return StatusCode(201);
        }
        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userBasket = await RetrieveBasket(User.Identity.Name);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                Basket = userBasket?.MapBasketToDto(),
            };
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
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == buyerId);

            return result;
        }
    }
}
