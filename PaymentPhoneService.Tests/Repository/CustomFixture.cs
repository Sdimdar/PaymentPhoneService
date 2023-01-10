using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PaymentPhoneService.Domain.Services;
using PaymentPhoneService.Infrastructure.Repositories.Interfaces;

namespace PaymentPhoneService.Tests.Repository;

[CollectionDefinition("WebApplicationFactory")]
public class CustomFixture<TProgram> :  WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var repositoryDesc = services.First(s => s.ServiceType == typeof(ITransactionRepository));
            services.Remove(repositoryDesc);
            services.AddTransient<ITransactionRepository, MoqRepository>();
        });
    }
    
}