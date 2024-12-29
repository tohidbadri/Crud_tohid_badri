
using FluentValidation;

namespace Rira_TohidBadri_Crud.Application.Commands.Person.DeletePerson;

public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator()
    {
        RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage($"{nameof(Domain.Entities.Person.Id)} cannot be empty");
    }
}

