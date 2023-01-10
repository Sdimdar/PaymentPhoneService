using PaymentPhoneService.Helper;
using PaymentPhoneService.Helper.Enums;
using PaymentPhoneService.Helper.Exceptions;

namespace PaymentPhoneService.API.Models;

public class PhoneNumber
{
    private int _minimumNumber = 11;
    private int _maximumNumber = 12;
    public MobileOperator OperatorCode { get; set; }
    public string Number { get; set; }

    public PhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
            throw new NullReferenceException(nameof(phoneNumber));
        phoneNumber = phoneNumber.Replace("(", "").Replace(")", "").Replace(" ", "");
        if (phoneNumber.Contains('+') && phoneNumber.Length == _maximumNumber)
        {
            Number = phoneNumber[2..];
            if (!Number.All(char.IsDigit))
                throw new PhoneTypeException($"Input number is not correct. Input value = {phoneNumber}," +
                                             $"cant convert to digit number = {Number}");
            GetOperatorCode(phoneNumber[2..5]);
        }
        else if (phoneNumber.Length == _minimumNumber && !phoneNumber.Contains('+'))
        {
            Number = phoneNumber[1..];
            if (!Number.All(char.IsDigit))
                throw new PhoneTypeException($"Input number is not correct. Input value = {phoneNumber}," +
                                             $"cant convert to digit number = {Number}");
            GetOperatorCode(phoneNumber[1..4]);
        }
        else
        {
            throw new PhoneTypeException($"Phone number is not correct. Incorrect length. Input value {phoneNumber}");
        }

    }

    private void GetOperatorCode(string code)
    {
        if (int.TryParse(code, out int operatorCode))
        {
            if (operatorCode == MobileOperatorInfo.Altel1 || operatorCode == MobileOperatorInfo.Altel2)
                OperatorCode = MobileOperator.Altel;
            else if (operatorCode == MobileOperatorInfo.Beeline1 || operatorCode == MobileOperatorInfo.Beeline2)
                OperatorCode = MobileOperator.Beeline;
            else if (operatorCode == MobileOperatorInfo.TeleTwo1 || operatorCode == MobileOperatorInfo.TeleTwo2)
                OperatorCode = MobileOperator.TeleTwo;
            else if (operatorCode == MobileOperatorInfo.Active)
                OperatorCode = MobileOperator.Active;
            else
            {
                throw new PhoneTypeException($"Input operator code not found, input code = {code}");
            }
        }
        else
        {
            throw new PhoneTypeException($"Input operator code is not correct, input code = {code}. " +
                                         $"Can not convert to digit number");
        }
    }
}