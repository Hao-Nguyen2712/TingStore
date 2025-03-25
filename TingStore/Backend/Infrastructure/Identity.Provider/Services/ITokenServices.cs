// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using Identity.Provider.Models;

namespace Identity.Provider.Services
{
    public interface ITokenServices
    {
        Task<TokenModel> GenerateJwtToken(ApplicationUser user);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    }
}
