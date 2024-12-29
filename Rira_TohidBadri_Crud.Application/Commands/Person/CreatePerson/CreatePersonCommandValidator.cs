
using FluentValidation;
using Rira_TohidBadri_Crud.Domain.Entities;

namespace Rira_TohidBadri_Crud.Application.Commands.Person.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
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
            .WithMessage($"{nameof(Domain.Entities.Person.NationalCode)} cannot be shorter than 10 characters");
    }
}

