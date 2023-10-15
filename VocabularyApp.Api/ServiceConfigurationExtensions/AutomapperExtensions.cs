using VocabularyApp.Persistence.MsSql.Mappings;
using AutoMapper;

namespace VocabularyApp.Api.ServiceConfigurationExtensions;

public static class AutomapperExtensions
{
    public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddPersistenceDbMappingProfiles();
        });

        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}