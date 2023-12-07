using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository;
using Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class DbExtension
    {
        public static IServiceCollection AddDbInfra(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<StockBrokarageDbContext>(options =>
            // options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(StockBrokarageDbContext).Assembly.FullName)));
            services.AddDbContext<StockBrokarageDbContext>(options => options.UseInMemoryDatabase("StockBrokarage"));

            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockHistoryPriceRepository, StockHistoryPriceRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();

            // Register the seeder
            services.AddTransient<StockSeeder>();

            return services;
        }
    }
}
