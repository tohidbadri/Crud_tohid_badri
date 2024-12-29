
using FluentValidation;

namespace Rira_TohidBadri_Crud.Application.Commands.Person.UpdatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(x => x.FirstName)
                  .NotEmpty()
                  .WithMessage($"{nameof(Domain.Entities.Person.FirstName)} cannot be empty")
                  .MaximumLength(50)
                  .WithMessage($"{nameof(Domain.Entities.Person.FirstName)} cannot be longer than 50 characters")
                  .MinimumLength(5)
                  .WithMessage($"{nameof(Domain.Entities.Person.FirstName)} cannot be shorter than 5 characters");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage($"{nameof(Domain.Entities.Person.LastName)} cannot be empty")
            .MaximumLength(50)
            .WithMessage($"{nameof(Domain.Entities.Person.LastName)} cannot be longer than 50 characters");

        RuleFor(x => x.NationalCode)
            .NotEmpty()
            .WithMessage($"{nameof(Domain.Entities.Person.NationalCode)} cannot be empty")
            .MaximumLength(10)
            .WithMessage($"{nameof(Domain.Entities.Person.NationalCode)} cannot be longer than 10 characters")
            .MinimumLength(10)
            .WithMessage($"{nameof(Domain.Entities.Person.FirstName)} cannot be shorter than 10 characters");
    }
}

