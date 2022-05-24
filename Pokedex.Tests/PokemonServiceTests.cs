using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Pokedex.Entities;
using Pokedex.Services;
using System.Text.Json;
using Xunit;

namespace Pokedex.Tests
{
    public class PokemonServiceTests
    {
        [Fact]
        public async void PokemonService_LoadPokemon_ReturnsExpected()
        {
            var mockLogger = new Mock<ILogger<PokemonService>>();

            var fixture = new Fixture();
            var mockedPokemonResponse = fixture.Create<PokemonResponseDto>();

            var expectedPokemonModel = new PokedexModel()
            {
                Name = mockedPokemonResponse.Name,
                Habitat = mockedPokemonResponse.Habitat.Name,
                Is_Legendary = mockedPokemonResponse.Is_Legendary,
                Description = mockedPokemonResponse.FlavorTestEntries[0].FlavorText
            };

            string mockedPokemonResponseJson = JsonSerializer.Serialize(mockedPokemonResponse);

            // Arrange
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<PokedexModel>(It.IsAny<PokemonResponseDto>()))
                .Returns(expectedPokemonModel);

            var mockWebAPIClient = new Mock<IWebAPIClient>();
            mockWebAPIClient.Setup(repo => repo.GetPokemonDetails(It.IsAny<string>()))
                .ReturnsAsync(mockedPokemonResponseJson);

            // Act
            var pokemonService = new PokemonService(mockWebAPIClient.Object, mockMapper.Object, mockLogger.Object);
            var actualResponseModel = await pokemonService.LoadPokemon(expectedPokemonModel.Name);

            // Assert
            Assert.Equal(expectedPokemonModel.Name, actualResponseModel.Name);
            Assert.Equal(expectedPokemonModel.Habitat, actualResponseModel.Habitat);
            Assert.Equal(expectedPokemonModel.Is_Legendary, actualResponseModel.Is_Legendary);
            Assert.Equal(expectedPokemonModel.Description, actualResponseModel.Description);
        }

        [Theory]
        [InlineData(true, "suburbia")]
        [InlineData(false, "cave")]
        [InlineData(false, "city")]
        [InlineData(false, "countryside")]
        public async void PokemonService_LoadTranslatedPokemon_ReturnsExpected(bool isLegendary, string habitat)
        {
            var mockLogger = new Mock<ILogger<PokemonService>>();

            var fixture = new Fixture();
            var mockedPokemonResponse = fixture.Create<PokemonResponseDto>();
            mockedPokemonResponse.Is_Legendary = isLegendary;
            mockedPokemonResponse.Habitat.Name = habitat;

            var expectedPokemonModel = new PokedexModel()
            {
                Name = mockedPokemonResponse.Name,
                Habitat = mockedPokemonResponse.Habitat.Name,
                Is_Legendary = mockedPokemonResponse.Is_Legendary,
                Description = mockedPokemonResponse.FlavorTestEntries[0].FlavorText
            };

            string language = isLegendary || habitat == "cave" ? "yoda" : "shakespeare";

            var expectedTranslatedPokemonModel = new PokedexModel()
            {
                Name = mockedPokemonResponse.Name,
                Habitat = mockedPokemonResponse.Habitat.Name,
                Is_Legendary = mockedPokemonResponse.Is_Legendary,
                Description = language == "yoda" ? "Force be with you nik" : "Valorous morrow to thee,  sir nik"
            };

            string mockedPokemonResponseJson = JsonSerializer.Serialize(mockedPokemonResponse);
            string expectedTranslatedPokemonResponseJson = JsonSerializer.Serialize(expectedTranslatedPokemonModel);

            // Arrange
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<PokedexModel>(It.IsAny<PokemonResponseDto>()))
                .Returns(expectedPokemonModel);

            var mockWebAPIClient = new Mock<IWebAPIClient>();
            mockWebAPIClient.Setup(repo => repo.GetPokemonDetails(It.IsAny<string>()))
                .ReturnsAsync(mockedPokemonResponseJson);
            mockWebAPIClient.Setup(repo => repo.TranslateText(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(expectedTranslatedPokemonResponseJson);

            // Act
            var pokemonService = new PokemonService(mockWebAPIClient.Object, mockMapper.Object, mockLogger.Object);
            var actualResponseModel = await pokemonService.LoadTranslatedPokemon(expectedPokemonModel.Name);

            // Assert
            Assert.Equal(expectedPokemonModel.Name, actualResponseModel.Name);
            Assert.Equal(expectedPokemonModel.Habitat, actualResponseModel.Habitat);
            Assert.Equal(expectedPokemonModel.Is_Legendary, actualResponseModel.Is_Legendary);
            Assert.Equal(expectedPokemonModel.Description, actualResponseModel.Description);
        }

        [Fact]
        public async void PokemonService_LoadTranslatedPokemon_ReturnsUntranslatedDescriptionWhenTranslationUnavailable()
        {
            var mockLogger = new Mock<ILogger<PokemonService>>();

            var fixture = new Fixture();
            var mockedPokemonResponse = fixture.Create<PokemonResponseDto>();

            var expectedPokemonModel = new PokedexModel()
            {
                Name = mockedPokemonResponse.Name,
                Habitat = mockedPokemonResponse.Habitat.Name,
                Is_Legendary = mockedPokemonResponse.Is_Legendary,
                Description = mockedPokemonResponse.FlavorTestEntries[0].FlavorText
            };

            string mockedPokemonResponseJson = JsonSerializer.Serialize(mockedPokemonResponse);

            // Arrange
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<PokedexModel>(It.IsAny<PokemonResponseDto>()))
                .Returns(expectedPokemonModel);

            var mockWebAPIClient = new Mock<IWebAPIClient>();
            mockWebAPIClient.Setup(repo => repo.GetPokemonDetails(It.IsAny<string>()))
                .ReturnsAsync(mockedPokemonResponseJson);
            mockWebAPIClient.Setup(repo => repo.TranslateText(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(value: null);

            // Act
            var pokemonService = new PokemonService(mockWebAPIClient.Object, mockMapper.Object, mockLogger.Object);
            var actualResponseModel = await pokemonService.LoadTranslatedPokemon(expectedPokemonModel.Name);

            // Assert
            Assert.Equal(expectedPokemonModel.Name, actualResponseModel.Name);
            Assert.Equal(expectedPokemonModel.Habitat, actualResponseModel.Habitat);
            Assert.Equal(expectedPokemonModel.Is_Legendary, actualResponseModel.Is_Legendary);
            Assert.Equal(expectedPokemonModel.Description, actualResponseModel.Description);
        }
    }
}
