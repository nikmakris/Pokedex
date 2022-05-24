using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pokedex.Controllers;
using Pokedex.Entities;
using Pokedex.Entities.FunTranslation;
using Pokedex.Interfaces;
using Pokedex.Services;
using System.Text.Json;
using Xunit;

namespace Pokedex.Tests
{
    public class PokemonControllerTests
    {
        [Fact]
        public async void PokemonController_Index_ReturnsExpected()
        {
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
            var mockWebAPIClient = new Mock<IWebAPIClient>();
            mockWebAPIClient.Setup(repo => repo.GetPokemonDetails(It.IsAny<string>()))
                .ReturnsAsync(mockedPokemonResponseJson);

            var mockPokemonService = new Mock<IPokemonService>();
            mockPokemonService.Setup(repo => repo.LoadPokemon(It.IsAny<string>()))
                .ReturnsAsync(expectedPokemonModel);

            var controller = new PokemonController(mockPokemonService.Object);

            // Act
            var result = await controller.Index(It.IsAny<string>());
            var okResult = result as OkObjectResult;
            var actualResponseModel = okResult.Value as PokedexModel;

            // Assert
            Assert.IsAssignableFrom<PokedexModel>(okResult.Value);
            Assert.Equal(expectedPokemonModel.Name, actualResponseModel.Name);
            Assert.Equal(expectedPokemonModel.Habitat, actualResponseModel.Habitat);
            Assert.Equal(expectedPokemonModel.Is_Legendary, actualResponseModel.Is_Legendary);
            Assert.Equal(expectedPokemonModel.Description, actualResponseModel.Description);
        }

        [Fact]
        public async void PokemonController_Index_ReturnsNull()
        {
            var fixture = new Fixture();
            var mockedPokemonResponse = fixture.Create<PokemonResponseDto>();

            PokedexModel expectedPokemonModel = null;

            string mockedPokemonResponseJson = JsonSerializer.Serialize(mockedPokemonResponse);

            // Arrange
            var mockWebAPIClient = new Mock<IWebAPIClient>();
            mockWebAPIClient.Setup(repo => repo.GetPokemonDetails(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            var mockPokemonService = new Mock<IPokemonService>();
            mockPokemonService.Setup(repo => repo.LoadPokemon(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            var controller = new PokemonController(mockPokemonService.Object);

            // Act
            var result = await controller.Index(It.IsAny<string>());
            var okResult = result as OkObjectResult;
            var actualResponseModel = okResult.Value as PokedexModel;

            // Assert
            Assert.Equal(expectedPokemonModel, actualResponseModel);
        }

        [Fact]
        public async void PokemonController_Translated_ReturnsExpected()
        {
            var fixture = new Fixture();
            var mockedPokemonResponse = fixture.Create<PokemonResponseDto>();
            var mockedFunTranslationResponse = fixture.Create<TranslatedResponseDto>();

            var expectedPokemonModel = new PokedexModel()
            {
                Name = mockedPokemonResponse.Name,
                Habitat = mockedPokemonResponse.Habitat.Name,
                Is_Legendary = mockedPokemonResponse.Is_Legendary,
                Description = mockedFunTranslationResponse?.Contents?.Translation
            };

            string mockedPokemonResponseJson = JsonSerializer.Serialize(mockedPokemonResponse);
            string mockedFunTranslationResponseJson = JsonSerializer.Serialize(mockedFunTranslationResponse);

            // Arrange
            var mockWebAPIClient = new Mock<IWebAPIClient>();
            mockWebAPIClient.Setup(repo => repo.GetPokemonDetails(It.IsAny<string>()))
                .ReturnsAsync(mockedPokemonResponseJson);
            mockWebAPIClient.Setup(repo => repo.TranslateText(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(mockedFunTranslationResponseJson);

            var mockPokemonService = new Mock<IPokemonService>();
            mockPokemonService.Setup(repo => repo.LoadTranslatedPokemon(It.IsAny<string>()))
                .ReturnsAsync(expectedPokemonModel);

            var controller = new PokemonController(mockPokemonService.Object);

            // Act
            var result = await controller.Translated(It.IsAny<string>());
            var okResult = result as OkObjectResult;
            var actualResponseModel = okResult.Value as PokedexModel;

            // Assert
            Assert.Equal(expectedPokemonModel.Name, actualResponseModel.Name);
            Assert.Equal(expectedPokemonModel.Habitat, actualResponseModel.Habitat);
            Assert.Equal(expectedPokemonModel.Is_Legendary, actualResponseModel.Is_Legendary);
            Assert.Equal(expectedPokemonModel.Description, actualResponseModel.Description);
        }

        [Fact]
        public async void PokemonController_Translated_ReturnsNull()
        {
            var fixture = new Fixture();
            var mockedPokemonResponse = fixture.Create<PokemonResponseDto>();

            PokedexModel expectedPokemonModel = null;

            string mockedPokemonResponseJson = JsonSerializer.Serialize(mockedPokemonResponse);

            // Arrange
            var mockWebAPIClient = new Mock<IWebAPIClient>();
            mockWebAPIClient.Setup(repo => repo.GetPokemonDetails(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<string>());
            mockWebAPIClient.Setup(repo => repo.TranslateText(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(value: null);

            var mockPokemonService = new Mock<IPokemonService>();
            mockPokemonService.Setup(repo => repo.LoadTranslatedPokemon(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            var controller = new PokemonController(mockPokemonService.Object);

            // Act
            var result = await controller.Index(It.IsAny<string>());
            var okResult = result as OkObjectResult;
            var actualResponseModel = okResult.Value as PokedexModel;

            // Assert
            Assert.Equal(expectedPokemonModel, actualResponseModel);
        }
    }
}
