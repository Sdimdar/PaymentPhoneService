using PaymentPhoneService.API.Models;
using PaymentPhoneService.Infrastructure.Repositories.Interfaces;

namespace PaymentPhoneService.Domain.Services;

public class PhonePaymentService : IPhonePaymentService
{
    private readonly ITransactionRepository _repository;

    public PhonePaymentService(ITransactionRepository repository)
    {
        _repository = repository;
    }


    public async Task<bool> AddPayment(PaymentRequest request, CancellationToken cancellationToken)
    {
        Payment payment = new Payment(request.PhoneNumber, request.Amount);
        await _repository.AddPayment(payment, cancellationToken);
        return true;
    }
}