using Pokedex.Entities;
using System.Threading.Tasks;

namespace Pokedex.Interfaces
{
    public interface IPokemonService
    {
        Task<PokedexModel> LoadPokemon(string name);

        Task<PokedexModel> LoadTranslatedPokemon(string name);
    }
}
