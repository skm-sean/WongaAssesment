using Common.Models;
using FluentValidation;

namespace Common.Validators;

public class PayLoadBaseValidator : AbstractValidator<PayloadBase>
{
    public PayLoadBaseValidator()
    {
        RuleFor(x => x.Message).NotEmpty().WithMessage("Please specify a valid message");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a name")
            .Matches(@"[A-Z][a-zA-Z]*$").WithMessage("Please specify a name");
    }
}