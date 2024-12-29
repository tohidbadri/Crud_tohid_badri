
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rira_TohidBadri_Crud.Contract.Exceptions;
using Rira_TohidBadri_Crud.Infrastructure;

namespace Rira_TohidBadri_Crud.Application.Commands.Person.DeletePerson;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
{
    private readonly CrudDbContext context;

    public DeletePersonCommandHandler(CrudDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var delitingPerson = await context.People.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (delitingPerson is null)
        {
            throw new NotFoundException($"{nameof(Domain.Entities.Person)} with {nameof(Domain.Entities.Person.Id)}: {request.Id} was not found in database!");
        }

        context.People.Remove(delitingPerson);
        await context.SaveChangesAsync();
        return Unit.Value;
    }
}

