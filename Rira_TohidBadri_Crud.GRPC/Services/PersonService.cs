namespace Rira_TohidBadri_Crud.GRPC.Services
{
    using Grpc.Core;
    using MediatR;
    using Rira_TohidBadri_Crud.Application.Commands.Person.CreatePerson;
    using Rira_TohidBadri_Crud.Application.Commands.Person.DeletePerson;
    using Rira_TohidBadri_Crud.Application.Commands.Person.UpdatePerson;
    using Rira_TohidBadri_Crud.Application.Queries.Person.GetPersonById;
    using Rira_TohidBadri_Crud.Application.Queries.Person.GetPersons;
    using Rira_TohidBadri_Crud.Contract.Exceptions;
    using System.Threading.Tasks;
    using System.Linq;
    using System;
    using Rira_TohidBadri_Crud.GRPC.Handlers;

    public class PersonService : PersonServices.PersonServicesBase
    {
        private readonly IMediator mediator;

        public PersonService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<PersonResponse> CreatePerson(Person request, ServerCallContext context)
        {
            try
            {
                var command = new CreatePersonCommand(request.FirstName, request.LastName, request.NationalCode, Convert.ToDateTime(request.BirthDate));
                var result = await this.mediator.Send(command);
                return new PersonResponse { Message = "new Person created successfully", Success = true };
            }
            catch (CustomValidationException ex)
            {
                throw ExceptionHandler.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleInternalException(ex);
            }
        }

        public async override Task<Person> GetPerson(PersonRequest request, ServerCallContext context)
        {
            try
            {
                var query = new GetPersonByIdQuery(request.Id);
                var person = await this.mediator.Send(query);

                if (person == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));
                }
                return new Person
                {
                    Id = person.personDto.Id,
                    FirstName = person.personDto.FirstName,
                    LastName = person.personDto.LastName,
                    NationalCode = person.personDto.NationalCode,
                    BirthDate = person.personDto.DateOfBirth.ToString(),
                };
            }
            catch (CustomValidationException ex)
            {
                throw ExceptionHandler.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleInternalException(ex);
            }
        }

        public override async Task<GetAllPersonsResponse> GetAllPersons(GetAllPersonsRequest request, ServerCallContext context)
        {
            try
            {
                var query = new GetPersonsQuery();
                var persons = await this.mediator.Send(query);
                if (persons.personDtos.Count <= 0)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "No Person found"));
                }

                var response = new GetAllPersonsResponse();
                response.Persons.AddRange(persons.personDtos.Select(x => new Person
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    NationalCode = x.NationalCode,
                    BirthDate = x.DateOfBirth.ToString(),
                }));

                return response;
            }
            catch (CustomValidationException ex)
            {
                throw ExceptionHandler.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleInternalException(ex);
            }
        }

        public override async Task<PersonResponse> UpdatePerson(Person request, ServerCallContext context)
        {
            try
            {
                var command = new UpdatePersonCommand(request.Id, request.FirstName, request.LastName, request.NationalCode, Convert.ToDateTime(request.BirthDate));
                var result = await mediator.Send(command);
                return new PersonResponse { Message = "Person updated", Success = true };
            }
            catch (CustomValidationException ex)
            {
                throw ExceptionHandler.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleInternalException(ex);
            }
        }

        public override async Task<PersonResponse> DeletePerson(PersonRequest request, ServerCallContext context)
        {
            try
            {
                var command = new DeletePersonCommand(request.Id);
                var result = await mediator.Send(command);
                return new PersonResponse { Message = "Your Person deleted", Success = true };
            }
            catch (CustomValidationException ex)
            {
                throw ExceptionHandler.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleInternalException(ex);
            }
        }
    }
}
