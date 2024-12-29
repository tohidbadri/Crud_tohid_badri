
using MediatR;

namespace Rira_TohidBadri_Crud.Application.Commands.Person.DeletePerson;

public record DeletePersonCommand(int Id):IRequest<Unit>;