
using MediatR;
using Rira_TohidBadri_Crud.Contract.Response.Person;

namespace Rira_TohidBadri_Crud.Application.Queries.Person.GetPersons;

public record GetPersonsQuery:IRequest<GetPersonsResponse>;