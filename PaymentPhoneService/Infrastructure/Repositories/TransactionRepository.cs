using PaymentPhoneService.API.Models;
using PaymentPhoneService.Helper.Enums;
using PaymentPhoneService.Infrastructure.Models;
using PaymentPhoneService.Infrastructure.Models.DbModels;
using PaymentPhoneService.Infrastructure.Repositories.Interfaces;

namespace PaymentPhoneService.Infrastructure.Repositories;

public class TransactionRepository:ITransactionRepository
{
    private readonly TransactionDbContext _db;
    private readonly ILogger<TransactionRepository> _logger;

    public TransactionRepository(TransactionDbContext db, ILogger<TransactionRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task AddPayment(Payment payment, CancellationToken cancellationToken)
    {
        TransactionDbModel transaction = new TransactionDbModel()
        {
            Number = payment.PhoneNumber.Number,
            Operator = payment.PhoneNumber.OperatorCode,
            Amount = payment.Amount,
            CreateDate = DateTime.Now
        };
        await _db.Transactions.AddAsync(transaction,cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Add new entity in database,Id = {transaction.Id}, entity = {transaction}");
    }
    
    
}