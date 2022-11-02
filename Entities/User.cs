﻿using Microsoft.AspNetCore.Identity;

namespace ecommerceApi.Entities
{
    public class User : IdentityUser<int>
    {
        public UserAddress Address { get; set; }
    }
}
