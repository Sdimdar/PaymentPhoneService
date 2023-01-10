namespace PaymentPhoneService.Helper.Exceptions;

public class PaymentAmountException : ApplicationException
{
    public PaymentAmountException():base(){}

    public PaymentAmountException( string message):base(message){}
}