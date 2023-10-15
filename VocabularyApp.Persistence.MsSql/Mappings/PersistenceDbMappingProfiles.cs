using AutoMapper;
using VocabularyApp.Application.Entities;

namespace VocabularyApp.Persistence.MsSql.Mappings;

public class EntityMappingProfiles : Profile
{
    public EntityMappingProfiles()
    {
        CreateMap<Word, Entities.Word>().ReverseMap();
    }
}
