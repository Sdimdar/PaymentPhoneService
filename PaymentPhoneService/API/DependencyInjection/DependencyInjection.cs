using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;
using PaymentPhoneService.Domain.Services;
using PaymentPhoneService.Infrastructure.Models;
using PaymentPhoneService.Infrastructure.Repositories;
using PaymentPhoneService.Infrastructure.Repositories.Interfaces;

namespace PaymentPhoneService.API.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IPhonePaymentService, PhonePaymentService>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();

        return services;
    }

    public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TransactionDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnectionString")));
        return services;
    }
   
    public static WebApplicationBuilder AddLoggingConfiguration(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();

        return builder;
    }
}