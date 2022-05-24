using System.Text.Json.Serialization;

namespace Pokedex.Entities
{
    public class PokemonResponseDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("flavor_text_entries")]
        public FlavorTestEntry[] FlavorTestEntries { get; set; }

        [JsonPropertyName("habitat")]
        public Habitat Habitat { get; set; }

        [JsonPropertyName("is_legendary")]
        public bool Is_Legendary { get; set; }
    }

    public class Habitat
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class FlavorTestEntry
    {
        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; }
    }

    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
