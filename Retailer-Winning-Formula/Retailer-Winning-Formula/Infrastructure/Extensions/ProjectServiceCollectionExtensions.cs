using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retailer_Winning_Formula.DataLayer.DataContext;

namespace Retailer_Winning_Formula.Infrastructure.Extensions
{
    public static class ProjectServiceCollectionExtensions
    {
        /// <summary>
        /// Configure Database Context
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<ZucDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Dev"), providerOptions => providerOptions.EnableRetryOnFailure()));
    }
}
