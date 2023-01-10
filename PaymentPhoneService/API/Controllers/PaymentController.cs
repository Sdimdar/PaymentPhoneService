using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PaymentPhoneService.API.Models;
using PaymentPhoneService.Domain.Services;

namespace PaymentPhoneService.API.Controllers;
[Route("[controller]/[action]")]
public class PaymentController : Controller
{
    private readonly IPhonePaymentService _paymentService;
    private readonly IStringLocalizer<PaymentController> _localizer;

    public PaymentController(IPhonePaymentService paymentService, IStringLocalizer<PaymentController> localizer)
    {
        _paymentService = paymentService;
        _localizer = localizer;
    }

    [HttpPost]
    
    public async Task<ActionResult<ResponseVM>> PhonePayment([FromBody] PaymentRequest request,
        CancellationToken cancellationToken)
    {
        
        ResponseVM response;
        if (await _paymentService.AddPayment(request, cancellationToken))
        {
            response = new ResponseVM()
            {
                IsSuccess = true,
                Error = null,
                Message = _localizer["Successfully"]
            };
            return Ok(response);
        }

        response = new ResponseVM()
        {
            IsSuccess = false,
            Message = null,
            Error = _localizer["InternalPaymentError"]
        };
        return Ok(response);
    }
}