using AutoMapper;

namespace VocabularyApp.Persistence.MsSql.Mappings;

public static class PersistenceDbMapperConfigurationExtensions
{
    public static IMapperConfigurationExpression AddPersistenceDbMappingProfiles(this IMapperConfigurationExpression mapConfigExpression)
    {
        mapConfigExpression = mapConfigExpression ?? throw new ArgumentNullException(nameof(mapConfigExpression));

        mapConfigExpression.AddProfile(new EntityMappingProfiles());

        return mapConfigExpression;
    }
}
