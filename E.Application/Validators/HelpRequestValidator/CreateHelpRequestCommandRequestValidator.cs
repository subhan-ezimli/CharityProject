using E.Application.CQRS.HelpRequest.Command.Request;
using FluentValidation;

namespace E.Application.Validators.HelpRequestValidator;

internal sealed class CreateHelpRequestCommandRequestValidator : AbstractValidator<CreateHelpRequestCommandRequest>
{
    public CreateHelpRequestCommandRequestValidator()
    {
        RuleFor(request => request.Address)
           .NotEmpty()
           .NotNull();

        RuleFor(request => request.ShortInfo)
          .NotEmpty()
          .NotNull();

        RuleFor(request => request.PhoneNumber)
          .NotEmpty()
          .NotNull();

        RuleFor(request => request.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(request => request.Surname)
       .NotEmpty()
       .NotNull();

    }

}
