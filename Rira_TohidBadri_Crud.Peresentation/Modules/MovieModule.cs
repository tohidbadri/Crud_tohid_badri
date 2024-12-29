using MediatR;
using Rira_TohidBadri_Crud.Application.Commands.Person.CreatePerson;
using Rira_TohidBadri_Crud.Application.Commands.Person.DeletePerson;
using Rira_TohidBadri_Crud.Application.Commands.Person.UpdatePerson;
using Rira_TohidBadri_Crud.Application.Queries.Person.GetPersonById;
using Rira_TohidBadri_Crud.Application.Queries.Person.GetPersons;
using Rira_TohidBadri_Crud.Contract.Request.Person;

namespace Rira_TohidBadri_Crud.Presentation.Modules
{
    public static class PersonModule
    {
        public static void AddPersonEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Person", async (IMediator mediator, CancellationToken ct) =>
            {
                var Person = await mediator.Send(new GetPersonsQuery(), ct);
                return Results.Ok(Person);
            }).WithTags("Person");

            app.MapGet("/api/Person/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
            {
                var Person = await mediator.Send(new GetPersonByIdQuery(id), ct);
                return Results.Ok(Person);
            }).WithTags("Person");

            app.MapPost("/api/Person", async (IMediator mediator, CreatePersonRequest createPersonRequest, CancellationToken ct) =>
            {
                var command = new CreatePersonCommand(createPersonRequest.FirstName, createPersonRequest.LastName, createPersonRequest.NationalCode,createPersonRequest.DateOfBirth);
                var res = await mediator.Send(command, ct);
                return Results.Ok(res);
            }).WithTags("Person");

            app.MapPut("/api/Person/{id}", async (IMediator mediator, int id, UpdatePersonRequest updatePersonRequest, CancellationToken ct) =>
            {
                var command = new UpdatePersonCommand(id, updatePersonRequest.FirstName, updatePersonRequest.LastName, updatePersonRequest.NationalCode,updatePersonRequest.DateOfBirth);
                var res = await mediator.Send(command, ct);
                return Results.Ok(res);
            }).WithTags("Person");

            app.MapDelete("/api/Person/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
            {
                var res = await mediator.Send(new DeletePersonCommand(id), ct);
                return Results.Ok(res);
            }).WithTags("Person");
        }
    }
}
