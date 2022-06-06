using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retailer_Winning_Formula.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retailer_Winning_Formula.Infrastructure.Extensions
{
    public static class CustomServiceCollectionExtension
    {
        public static IServiceCollection AddCustomOptions(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            return services
              .Configure<EmailConfigurationOption>(
                    configuration.GetSection(nameof(ApplicationOptions.EmailConfiguration)))
              .Configure<CompanyEmailsOption>(
                    configuration.GetSection(nameof(ApplicationOptions.CompanyEmails)));
        }
    }
}
