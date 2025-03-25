// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Identity;

namespace Identity.Provider.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public int? UserId { get; set; } // User Id in User Service 
    };
}
