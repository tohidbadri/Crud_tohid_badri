
using MediatR;
using Rira_TohidBadri_Crud.Infrastructure;

namespace Rira_TohidBadri_Crud.Application.Commands.Person.CreatePerson;

public class CreatePersonCommandHandler:IRequestHandler<CreatePersonCommand,int>
{
    private readonly CrudDbContext context;

    public CreatePersonCommandHandler(CrudDbContext context)
    {
        this.context = context;
    }

    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Domain.Entities.Person
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            NationalCode = request.NationalCode,
        };
        await context.People.AddAsync(person);  
        await context.SaveChangesAsync(cancellationToken);
        return person.Id;
    }
}

