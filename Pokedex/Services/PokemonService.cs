using AutoMapper;
using Microsoft.Extensions.Logging;
using Pokedex.Entities;
using Pokedex.Entities.FunTranslation;
using Pokedex.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IWebAPIClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<PokemonService> _logger;

        public PokemonService(IWebAPIClient client, IMapper mapper, ILogger<PokemonService> logger)
        {
            _logger = logger;
            _client = client;
            _mapper = mapper;
        }

        public async Task<PokedexModel> LoadPokemon(string name)
        {
            var response = await _client.GetPokemonDetails(name);

            if (response == null)
            {
                _logger.LogInformation($"No Pokemon found with name {name}");
                return null;
            }

            return _mapper.Map<PokedexModel>(JsonSerializer.Deserialize<PokemonResponseDto>(response));
        }

        public async Task<PokedexModel> LoadTranslatedPokemon(string name)
        {
            var pokemonResponse = await _client.GetPokemonDetails(name);

            if (pokemonResponse == null)
            {
                _logger.LogInformation($"No Pokemon found with name {name}");
                return null;
            }

            var pokemonResponseDto = JsonSerializer.Deserialize<PokemonResponseDto>(pokemonResponse);

            var pokemon = _mapper.Map<PokedexModel>(pokemonResponseDto);

            if (pokemon != null)
            {
                string language = "shakespeare";

                if (pokemon.Habitat == "cave" || pokemon.Is_Legendary)
                {
                    _logger.LogInformation($"Switching to {language} translation");
                    language = "yoda";
                }
                else
                {
                    _logger.LogInformation($"Translating into {language}");
                }

                var response = await _client.TranslateText(pokemon.Description, language);

                if (response == null)
                {
                    _logger.LogInformation($"No {language} translation available for Pokemon with description {pokemon.Description}");

                    // Can't translate return untranslated Pokemon
                    return pokemon;
                }

                var translatedResponse = JsonSerializer.Deserialize<TranslatedResponseDto>(response);

                if (translatedResponse != null && translatedResponse.Success?.Total > 0)
                {
                    _logger.LogInformation($"Adding {language} translated Pokemon description {pokemon.Description}");
                    pokemon.Description = translatedResponse?.Contents?.Translated;
                }

                return pokemon;
            }
            else
            {
                return null;
            }
        }
    }
}
