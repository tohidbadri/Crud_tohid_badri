
using Mapster;
using Rira_TohidBadri_Crud.Contract.Response.Person;
using Rira_TohidBadri_Crud.Domain.Entities;

namespace Rira_TohidBadri_Crud.Application.Mappings;

public class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<List<Person>, GetPersonsResponse>.NewConfig()
            .Map(dest => dest.personDtos, src => src);
        TypeAdapterConfig<Person,GetPersonByIdResponse>.NewConfig()
            .Map(dest=>dest.personDto, src => src);
    }
}

