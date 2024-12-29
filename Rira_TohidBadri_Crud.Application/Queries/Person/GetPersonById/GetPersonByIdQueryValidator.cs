
using FluentValidation;

namespace Rira_TohidBadri_Crud.Application.Queries.Person.GetPersonById;

public class GetPersonByIdQueryValidator : AbstractValidator<GetPersonByIdQuery>
{
    public GetPersonByIdQueryValidator()
    {
        RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage($"{nameof(Domain.Entities.Person.Id)} cannot be empty");
    }
}

