using System.Threading.Tasks;

namespace Pokedex.Services
{
    public interface IWebAPIClient
    {
        Task<string> GetPokemonDetails(string name);
        Task<string> TranslateText(string textToTranslate, string language);
    }
}