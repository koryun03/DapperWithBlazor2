using Domain.RepositoryInterfaces;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructre(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISqlConnectionFactory>(provider =>
             new SqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IProductRepository, ProductRepository>();


        }
    }
}
