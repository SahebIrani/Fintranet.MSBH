using Domain.Validators;

using Xunit;

namespace Test.Unit;

public class MobileNumberValidatorTest
{
    public static IEnumerable<object[]> Data()
    {
        yield return new object[] { "+989123456789", true };
        yield return new object[] { "+31612345678", true };
        yield return new object[] { "+982188776655", false };
        yield return new object[] { "+16156548877", true };
        yield return new object[] { "0912134", false };
    }

    [Theory(DisplayName = "MobileNumberIsValid"), MemberData(nameof(Data))]
    public void MobileValidatorTest_WithExpectedResult(string phoneNumber, bool expectedResult)
    {
        var testResult = MobileValidator.IsValid(phoneNumber);

        Assert.Equal(expectedResult, testResult);
    }
}
