using PaymentPhoneService.API.Models;

namespace PaymentPhoneService.Infrastructure.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task AddPayment(Payment payment, CancellationToken cancellationToken);
}