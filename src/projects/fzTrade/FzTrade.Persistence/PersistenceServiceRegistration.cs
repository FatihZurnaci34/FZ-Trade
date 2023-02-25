using FzTrade.Application.Services.Repositories;
using FzTrade.Persistence.Contexts;
using FzTrade.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FzTrade.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options =>
                                                     options.UseSqlServer(
                                                         configuration.GetConnectionString("FZTradeConnectionString")));
            //services.AddScoped<IBrandRepository, BrandRepository>();
            //services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            return services;
        }
    }
}