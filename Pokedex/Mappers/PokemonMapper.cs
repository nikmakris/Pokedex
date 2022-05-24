using AutoMapper;
using Pokedex.Entities;
using System.Linq;

namespace Pokedex.Mappers
{
    public class PokemonMapper : Profile
    {
        public PokemonMapper()
        {
            CreateMap<PokemonResponseDto, PokedexModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.FlavorTestEntries.FirstOrDefault(l => l.Language.Name == "en").FlavorText));
        }
    }
}
