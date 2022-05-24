using Microsoft.AspNetCore.Mvc;
using Pokedex.Interfaces;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("pokemon/")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService pokemonService;

        public PokemonController(IPokemonService service)
        {
            pokemonService = service;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var response = await pokemonService.LoadPokemon(name);

            return Ok(response);
        }

        [HttpGet]
        [Route("translated/{name}")]
        public async Task<IActionResult> Translated(string name)
        {
            var response = await pokemonService.LoadTranslatedPokemon(name);

            return Ok(response);
        }
    }
}