using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerceApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace ecommerceApi.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(StoreContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    UserName = "Bob",
                    Email = "bob@test.com"
                };
                await userManager.CreateAsync(user, "Hej123!");
                await userManager.AddToRoleAsync(user, "Member");

                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                await userManager.CreateAsync(admin, "Hej123!");
                await userManager.AddToRolesAsync(admin, new[] {"Member","Admin"});
            }
            if (context.Products.Any()) return;

            var products = new List<Product>
            {
                        new Product
                {
                    Name = "Pomade",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 12000,
                    ImgUrl = "/images/products/sb-ang1.png",
                    Brand = "Kochi Stockholm",
                    Type = "Hair Product",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Putty Structure",
                    Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                    Price = 15000,
                    ImgUrl = "/images/products/sb-ang2.png",
                    Brand = "Kochi Stockholm",
                    Type = "Hair Product",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Define Cream",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    ImgUrl = "/images/products/sb-core1.png",
                    Brand = "Kochi Stockholm",
                    Type = "Hair Product",
                    QuantityInStock = 100
                },

        new Product
                {
                    Name = "Quiff Roller",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 12000,
                    ImgUrl = "/images/products/sb-ang1.png",
                    Brand = "Kochi Stockholm",
                    Type = "Hair Styling",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Detailed Comb",
                    Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                    Price = 15000,
                    ImgUrl = "/images/products/sb-ang2.png",
                    Brand = "Kochi Stockholm",
                    Type = "Hair Styling",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Volume Spray",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    ImgUrl = "/images/products/sb-core1.png",
                    Brand = "Kochi Stockholm",
                    Type = "Hair Styling",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Beard Oil",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 12000,
                    ImgUrl = "/images/products/sb-ang1.png",
                    Brand = "Kochi Stockholm",
                    Type = "Beard Products",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Extreme Cream",
                    Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                    Price = 15000,
                    ImgUrl = "/images/products/sb-ang2.png",
                    Brand = "Kochi Stockholm",
                    Type = "Beard Products",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Beard Comb",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    ImgUrl = "/images/products/sb-core1.png",
                    Brand = "Kochi Stockholm",
                    Type = "Beard Products",
                    QuantityInStock = 100
                },

            };

            foreach (var item in products)
            {
                context.Products.Add(item);
            }
            context.SaveChanges();
        }
    }
}
