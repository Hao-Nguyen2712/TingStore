// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Identity.Provider.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Identity.Provider.Middleware
{
    
    public class BlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppDbContext _context;
        public BlacklistMiddleware(RequestDelegate next, AppDbContext context)
        {
            _next = next;
            _context = context;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var isBlacklisted = await _context.BlackListToken.AnyAsync(bt => bt.Token == token && bt.ExpiryTime > DateTime.Now);
                if (isBlacklisted)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is blacklisted");
                    return;
                }   
            }
            await _next(context);
        }
    }
}
