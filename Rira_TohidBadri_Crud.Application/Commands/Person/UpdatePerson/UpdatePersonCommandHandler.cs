
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rira_TohidBadri_Crud.Contract.Exceptions;
using Rira_TohidBadri_Crud.Infrastructure;

namespace Rira_TohidBadri_Crud.Application.Commands.Person.UpdatePerson;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Unit>
{
    private readonly CrudDbContext context;

    public UpdatePersonCommandHandler(CrudDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var updatingPerson = await context.People.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (updatingPerson is null)
        {
            throw new ArgumentException($"{nameof(Domain.Entities.Person)} with {nameof(Domain.Entities.Person.Id)}: {request.Id} was not found in database!");
        }

        updatingPerson.FirstName = request.FirstName;
        updatingPerson.LastName = request.LastName;
        updatingPerson.NationalCode = request.NationalCode;
        updatingPerson.DateOfBirth = request.DateOfBirth;

        context.People.Update(updatingPerson);
        await context.SaveChangesAsync();

        return Unit.Value;
    }
}

