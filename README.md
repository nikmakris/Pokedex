# Pokedex - An API that Returns Basic Pokémon Info with a Fun Twist

This is a Pokémon API that returns basic Pokémon information. 
There is an additional endpoint which provides a funny Yoda :grin: or Shakespeare :scroll: version of the description.

## Description

This fun API project features two endpoints.
The first endpoint provides basic Pokémon information.

* **HTTP/GET /pokemon/&lt;pokemon name&gt;**

The second endpoint provides a funny translated description depending on the characteristics of the Pokémon requested. 

* **HTTP/GET /pokemon/translated/&lt;pokemon name&gt;**

If the Pokémon's habitat is **'cave'** or it **is_legendary** Pokemon you'll get a Yoda translated description, but by default this endpoint will return a Shakespeare inspired description.

### Future Improvements for Production

1. Add the ability to request a chosen language translation.
2. Add additional Pokémon information to response.
3. Inject IHttpClient into WebAPIClient class, so that HttpClient could be mocked and covered with unit tests.
4. Return more detailed HTTP response codes based on error scenarios rather than null.
5. Add authentication to funtranslations.com endpoint so that ratelimit could be removed.
6. Add authorisation and authentication.
7. Implement ratelimiting.

## Getting Started

### Dependencies

* Visual Studio Code or Visual Studio 2022
* .NET 6.0 SDK

### Installing

* [Install](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) the latest .NET 6.0 SDK
* Install Git
* Clone this repo
* Open with Visual Studio Code or Visual Studio 2022

### Executing program

* Open either Visual Studio Code or Visual Studio 2022
* Open the solution
* Start debugging to run the app from within your IDE

## Authors

Nik Makris

## Version History

* 0.1
    * Initial Release

## License

This project is licensed under the GNU GPLv3 License
