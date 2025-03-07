// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace User.Infrastructure.Data
{
    public  class UserContextSeed
    {
        public static async Task SeedAsync(UserContext userContext, ILogger<UserContextSeed> logger)
        {
            if (!userContext.Users.Any())
            {
                userContext.Users.AddRange(GetUser());
                await userContext.SaveChangesAsync();
                logger.LogInformation($"User Database: {typeof(UserContext).Name} seeded.");
            }
        }

        private static IEnumerable<Core.Entities.User> GetUser()
        {
            return new List<Core.Entities.User>
            {
                new()
                {
                    Email = "datdtce171751@fpt.edu.vn",
                    Password = "tandat123@",
                    FullName = "DatDT",
                    PhoneNumber = "0378196581",
                    Address = "Vinh Long City",
                    isActive = false,
                    CreatedAt = DateTime.Now

                }
            };
        }
    }
}
