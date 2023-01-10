using PaymentPhoneService.API.Models;

namespace PaymentPhoneService.Domain.Services;

public interface IPhonePaymentService
{
    Task<bool> AddPayment(PaymentRequest request, CancellationToken cancellationToken);
}