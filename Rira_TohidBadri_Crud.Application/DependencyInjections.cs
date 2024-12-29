
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Rira_TohidBadri_Crud.Application.Behaviors;
using Rira_TohidBadri_Crud.Application.Mappings;
using System.Reflection;

namespace Rira_TohidBadri_Crud.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cf =>
        {
            cf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            cf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        MappingConfig.Configure();
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);

        //injection of Fluent Validator
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}

