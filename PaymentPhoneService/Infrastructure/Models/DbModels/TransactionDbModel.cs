using PaymentPhoneService.Helper.Enums;

namespace PaymentPhoneService.Infrastructure.Models.DbModels;

public class TransactionDbModel
{
    public int Id { get; set; }
    public string Number { get; set; }
    public Decimal Amount { get; set; }
    public DateTime CreateDate { get; set; }
    public MobileOperator Operator { get; set; }
    
}