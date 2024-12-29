
using MediatR;
using Rira_TohidBadri_Crud.Contract.Response.Person;

namespace Rira_TohidBadri_Crud.Application.Queries.Person.GetPersonById;

public record GetPersonByIdQuery(int Id) : IRequest<GetPersonByIdResponse>;