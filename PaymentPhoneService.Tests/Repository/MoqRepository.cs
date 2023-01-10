using PaymentPhoneService.API.Models;
using PaymentPhoneService.Infrastructure.Repositories.Interfaces;

namespace PaymentPhoneService.Tests.Repository;

public class MoqRepository : ITransactionRepository
{
    public Task AddPayment(Payment payment, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}