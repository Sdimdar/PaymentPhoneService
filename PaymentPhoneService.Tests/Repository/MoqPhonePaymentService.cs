using PaymentPhoneService.API.Models;
using PaymentPhoneService.Domain.Services;

namespace PaymentPhoneService.Tests.Repository;

public class MoqPhonePaymentService : IPhonePaymentService
{
    public Task<bool> AddPayment(PaymentRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}