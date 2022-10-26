﻿using System.Linq;
using ecommerceApi.DTOs;
using ecommerceApi.Entities;

namespace ecommerceApi.Extensions
{
    public static class BasketExtensions
    {
        public static BasketDto MapBasketToDto( this Basket basket)
        {
            if(basket == null)
            {
                return null;
            }

            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.ImgUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity,
                }).ToList(),
            };
        }
    }
}