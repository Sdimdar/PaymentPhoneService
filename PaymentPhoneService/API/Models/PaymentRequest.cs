namespace PaymentPhoneService.API.Models;

public class PaymentRequest
{
    public string PhoneNumber { get; set; }
    public decimal Amount { get; set; }
}