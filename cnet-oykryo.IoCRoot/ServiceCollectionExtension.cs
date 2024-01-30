using cnet_oykryo.application.Services;
using cnet_oykryo.domain.Services;
using cnet_oykryo.Infrastructure.Data;
using cnet_oykryo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnet_oykryo.IoCRoot
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection serviceDescriptors, ConfigurationManager configurationManager )
        {
            return
                serviceDescriptors
                .AddScoped<IBankAccountRepository, BankAccountRepository>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IBankAccountRepository, BankAccountRepository>()
            // Register your DbContext with the connection string
            .AddDbContext<EPSDBContext>(options =>
                    options.UseSqlServer(
                configurationManager.GetConnectionString("DbContextConnection"),
                sqlServerOptions => sqlServerOptions.MigrationsAssembly("cnet-oykryo.web.api")
            ));
            
        }
    }
}
