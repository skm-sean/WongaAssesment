using Common.Models;
using FluentValidation;

namespace Common.Validators;

public class PayLoadBaseValidator : AbstractValidator<PayloadBase>
{
    public PayLoadBaseValidator()
    {
        RuleFor(x => x.Message).NotEmpty().WithMessage("Please specify a valid message");
        RuleFor(x => x.Message).Length(1, 100).WithMessage("Message must be between 1 and 100 characters");
        RuleFor(x => x.Message).NotNull().WithMessage("Please specify a valid message not null or whitespace");
        RuleFor(x => x.Name).NotNull().WithMessage("Please specify a valid message");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a name not null or whitespace")
            .Matches(@"[A-Z][a-zA-Z]*$").WithMessage("Please specify valid a name (only letters and starts with a capital letter)");
    }
}