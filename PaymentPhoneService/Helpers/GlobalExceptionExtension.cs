using System.Net;
using Microsoft.Extensions.Localization;
using PaymentPhoneService.API.Models;
using PaymentPhoneService.Helper.Exceptions;

namespace PaymentPhoneService.Helpers;

public class GlobalExceptionExtension
{
    private readonly RequestDelegate _next;
    private readonly IStringLocalizer<GlobalExceptionExtension> _localizer;
    private readonly ILogger<GlobalExceptionExtension> _logger;

    public GlobalExceptionExtension(RequestDelegate next, IStringLocalizer<GlobalExceptionExtension> localizer, ILogger<GlobalExceptionExtension> logger)
    {
        _next = next;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ResponseVM response;
        try
        {
            await _next.Invoke(context);
        }
        catch (PaymentAmountException ex)
        {
            
            _logger.LogWarning(ex.Message);
            response = new ResponseVM()
            {
                IsSuccess = false,
                Message = null,
                Error = _localizer["InvalidAmount"]
            };
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (PhoneTypeException ex)
        {
            
            _logger.LogWarning(ex.Message);
            response = new ResponseVM()
            {
                IsSuccess = false,
                Message = null,
                Error = _localizer["InvalidPhoneNumber"]
            };
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (NullReferenceException ex)
        {
            
            _logger.LogWarning("The data is null or empty: " + ex.Message);
            response = new ResponseVM()
            {
                IsSuccess = false,
                Message = null,
                Error = _localizer["InvalidInputData"]
            };
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(_localizer["ServerError"]);
        }
    }
}