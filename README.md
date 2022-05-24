# Pokedex - An API that Returns Basic Pokemon Info with a Fun Twist

This a Pokemon API that returns basic Pokemon information. 
There is an additional endpoint which provides a funny Yoda or Shakespeare version of the description.

## Description

This fun API project features two endpoints.
The first endpoint provides basic Pokemon information.

* HTTP/GET /pokemon/<pokemon name>

The second endpoint provides a funny translated description depending on the characteristics of the Pokemon requested. 

* HTTP/GET /pokemon/translated/<pokemon name>

If the Pokemon's habitat is 'cave' or it is a legendary Pokemon you'll get a Yoda translated description, but by default this endpoint will return a Shakespeare inspired description.

### Future Improvements for Production

* Add the ability to request a chosen language translation.
* Add additional Pokemon information to response.
* Inject IHttpClient into WebAPIClient class, so that HttpClient could be mocked and covered with unit tests.
* Return more detailed HTTP response codes based on error scenarios rather than null.
* Add authentication to funtranslations.com endpoint so that ratelimit could be removed.
* Add authorisation and authentication.
* Implement ratelimiting.

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
