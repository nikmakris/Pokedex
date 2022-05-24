using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public class WebAPIClient : IWebAPIClient
    {
        private readonly ILogger<WebAPIClient> _logger;

        public WebAPIClient(ILogger<WebAPIClient> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetPokemonDetails(string name)
        {
            var uri = $"https://pokeapi.co/api/v2/pokemon-species/{name}";

            using (var client = new HttpClient())
            {
                try
                {
                    _logger.LogInformation("Calling PokeApi.co for Pokemon details");
                    var response = await client.GetStringAsync(uri);
                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error calling PokeApi.co for Pokemon details");
                    return null;
                }
            }
        }

        public async Task<string> TranslateText(string textToTranslate, string language)
        {
            var uri = $"https://api.funtranslations.com/translate/{language}.json";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    _logger.LogInformation($"Calling api.funtranslations.com for {language} translation");

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("text", textToTranslate),
                    });

                    var response = await client.PostAsync(uri, formContent);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error calling api.funtranslations.com for {language} translation");
                    return null;
                }
            }
        }
    }
}
