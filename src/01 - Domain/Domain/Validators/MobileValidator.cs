using PhoneNumbers;

namespace Domain.Validators;

public class MobileValidator
{
    private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();

    public static bool IsValid(string phoneNumber)
    {
        if (IsValidPhoneNumber(
            phoneNumber,
            out PhoneNumber? phoneNumberObj))
            return IsTypeOfMobile(phoneNumberObj);

        return default;
    }

    private static bool IsTypeOfMobile(PhoneNumber? phoneNumber)
    {
        var numberType = PhoneNumberUtil.GetNumberType(phoneNumber);

        var isTypeOfMobileResult = numberType.ToString()
            .EndsWith(
                nameof(PhoneNumberType.MOBILE),
                StringComparison.InvariantCulture
            )
        ;

        return isTypeOfMobileResult;
    }

    private static bool IsVlidNumber(PhoneNumber? phoneNumber) =>
        PhoneNumberUtil.IsValidNumber(phoneNumber);

    private static bool IsValidPhoneNumber(
        string phoneNumber,
        out PhoneNumber? phoneNumberObj)
    {
        try
        {
            phoneNumberObj = PhoneNumberUtil.Parse(phoneNumber, default);

            var isValidNumberResult = IsVlidNumber(phoneNumberObj);

            return isValidNumberResult;
        }
        catch (NumberParseException)
        {
            phoneNumberObj = default;

            return default;
        }
    }
}
