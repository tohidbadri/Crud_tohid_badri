
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rira_TohidBadri_Crud.Contract.Response.Person;
using Rira_TohidBadri_Crud.Infrastructure;

namespace Rira_TohidBadri_Crud.Application.Queries.Person.GetPersons;

public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, GetPersonsResponse>
{
    private readonly CrudDbContext context;

    public GetPersonsQueryHandler(CrudDbContext context)
    {
        this.context = context;
    }

    public async Task<GetPersonsResponse> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await context.People.ToListAsync(cancellationToken);
        return persons.Adapt<GetPersonsResponse>();
    }
}

