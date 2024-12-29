
using MediatR;

namespace Rira_TohidBadri_Crud.Application.Commands.Person.UpdatePerson;

public record UpdatePersonCommand(int Id, string FirstName, string LastName, string NationalCode, DateTime DateOfBirth):IRequest<Unit>;
