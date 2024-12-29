
using MediatR;

namespace Rira_TohidBadri_Crud.Application.Commands.Person.CreatePerson;

public record CreatePersonCommand(string FirstName, string LastName, string NationalCode, DateTime DateOfBirth):IRequest<int>;