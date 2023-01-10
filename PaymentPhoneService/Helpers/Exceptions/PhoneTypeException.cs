namespace PaymentPhoneService.Helper.Exceptions;

public class PhoneTypeException : ApplicationException
{
    public PhoneTypeException():base(){}

    public PhoneTypeException(string message): base(message){}
}