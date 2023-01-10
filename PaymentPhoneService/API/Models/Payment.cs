using PaymentPhoneService.Helper.Exceptions;

namespace PaymentPhoneService.API.Models;

public class Payment
{
    public PhoneNumber PhoneNumber { get; set; }
    public decimal Amount { get; set; }

    public Payment(string phoneNumber, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new NullReferenceException(nameof(phoneNumber));
        if (amount < 1M)
            throw new PaymentAmountException($"Amount must be more than 1, input amount = {amount}");
        PhoneNumber = new PhoneNumber(phoneNumber);
        Amount = amount;
    }
}