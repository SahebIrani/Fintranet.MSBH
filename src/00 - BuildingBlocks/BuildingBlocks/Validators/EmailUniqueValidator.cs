using FluentValidation;
using FluentValidation.Validators;

using Service.Interfaces;

namespace BuildingBlocks.Validators;

public static class MyCustomValidators
{
    public static IRuleBuilderOptions<T, string> EmailUnique<T>(this IRuleBuilder<T, string> ruleBuilder, ICustomerService customerService) =>
        ruleBuilder.SetValidator(new EmailUniqueValidator<T>(customerService));
}

public class EmailUniqueValidator<T> : PropertyValidator<T, string>
{
    public ICustomerService CustomerService { get; }

    public EmailUniqueValidator(ICustomerService customerService) => CustomerService = customerService ?? throw new ArgumentNullException(nameof(customerService));

    public override string Name => "EmailUniqueValidator";

    protected override string GetDefaultMessageTemplate(string errorCode) => "{PropertyName} must be unique ..";

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        var result = CustomerService.Entities.Any(a => a.Email.Equals(value)) is false;

        return result;
    }
}
