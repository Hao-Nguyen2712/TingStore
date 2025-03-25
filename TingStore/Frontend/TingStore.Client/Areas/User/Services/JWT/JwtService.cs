// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TingStore.Client.Areas.User.Services.JWT
{
    public class JwtService
    {
        public ClaimsPrincipal DecodeJwt(string token)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtHandler.ReadJwtToken(token);

                // Chuyển đổi claims thành ClaimsPrincipal
                var claims = jwtToken.Claims;
                var identity = new ClaimsIdentity(claims, "jwt");
                return new ClaimsPrincipal(identity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error decoding JWT: {ex.Message}");
                return null;
            }
        }
    }
}
