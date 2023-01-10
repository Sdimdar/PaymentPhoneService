using System.Globalization;
using Microsoft.AspNetCore.Localization;
using NLog;
using NLog.Web;
using PaymentPhoneService.API.DependencyInjection;
using PaymentPhoneService.Helpers;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Init");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddLoggingConfiguration();
// Add services to the container.
    var services = builder.Services;

    services.AddLocalization(opt => opt.ResourcesPath = "Resources");
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddApplicationServices();
    services.AddDbConfiguration(builder.Configuration);


    var app = builder.Build();

    var supportedCultures = new[]
    {
        new CultureInfo("ru"),
        new CultureInfo("kk")
    };


    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("ru"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
    });

    app.UseMiddleware<GlobalExceptionExtension>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();

}
catch(Exception ex)
{
    logger.Error(ex, "The program stopped due to an error");
    throw;
}
finally
{
    LogManager.Shutdown();
}
public partial class Program { }
