using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Payment.Application
{
    public static class Dependency
    {
        public static ServiceCollection AddApplication(this ServiceCollection services)
        {
            return services;
        }
    }
}
