
using Rira_TohidBadri_Crud.Contract.Dtos;

namespace Rira_TohidBadri_Crud.Contract.Response.Person;

public record GetPersonsResponse(List<PersonDto> personDtos);