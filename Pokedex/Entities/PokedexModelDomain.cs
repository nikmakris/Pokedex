using System.Text.Json.Serialization;

namespace Pokedex.Entities
{
    public class PokedexModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("habitat")]
        public string Habitat { get; set; }

        [JsonPropertyName("isLegendary")]
        public bool Is_Legendary { get; set; }
    }
}
