using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Core.Repositories;
using User.Infrastructure.Data;
using User.Infrastructure.Repositories;

namespace User.Infrastructure.Extentions
{
    public static class InsfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<UserContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
            // Mỗi request có một instance
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            return serviceCollection;
        }
    }
}
