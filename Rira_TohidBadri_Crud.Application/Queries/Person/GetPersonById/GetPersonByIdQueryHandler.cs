
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rira_TohidBadri_Crud.Contract.Exceptions;
using Rira_TohidBadri_Crud.Contract.Response.Person;
using Rira_TohidBadri_Crud.Infrastructure;

namespace Rira_TohidBadri_Crud.Application.Queries.Person.GetPersonById;

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, GetPersonByIdResponse>
{
    private readonly CrudDbContext context;

    public GetPersonByIdQueryHandler(CrudDbContext context)
    {
        this.context = context;
    }

    public async Task<GetPersonByIdResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await context.People.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (person == null)
        {
            throw new NotFoundException($"{nameof(Domain.Entities.Person)} with {nameof(Domain.Entities.Person.Id)}: {request.Id} was not found in database!");
        }
        return person.Adapt<GetPersonByIdResponse>();
    }
}

