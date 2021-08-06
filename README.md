# PayerPayeeSys

<br/>

This is a api using ASP.NET Core 5.0, CQRS with MediatR, Polly with circuit breaker pattern for resilience(In-progress), Microsoft Sql Server or In Memory and a lot of fun things like swagger, JWT or Azure Active Diretory and Docker with Docker-compose, following the principles of SOLID, DRY and Clean Architecture. 


## Technologies
* .NET Core 5.0
* Entity Framework Core 5.0
* Microsoft Azure (Active Directory)
* Docker (Docker-compose)
* MediatR
* AutoMapper
* FluentValidation
* Swagger
* xUnit, Moq

## Database Configuration
This solution is configured to use a Microsoft sql database by default. 

If you would like to use In Memory, there is a key to be updated in **WebApi/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": true,
```
(This ensures that all users will be able to run the solution without needing to set up additional infrastructure)

Verify that the **DbConnection** connection details within **WebApi/appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.


## Authorization Configuration
This solution by default the application will use JWT as Athorization model.

```json
  "AuthenticationType": "Jwt",
```

If you would like to use Azure Active Directory, there is a key to be updated in **WebApi/appsettings.json** as follows:

```json
  "AuthenticationType": "AzureAD",
```

Verify that the **AzureAd** connection details within **WebApi/appsettings.AzureAD.json** points to a valid Azure Active Directory values. 

## Getting Started
The easiest way to get started is to install Docker in your computer and:

1. Navigate to root folder, same where Solution.sln is located.
   - Run the command `docker-compose up` << this will setup the environment using Microsoft Sql Server   
2. Docker compose will download all necessary components and, after all...
3. Just access http://localhost:8080 to check if it's working fine

## Testing
You can check all API endpoints at http://localhost:8080/swagger/index.html. Thanks [Swagger](https://github.com/swagger-api)