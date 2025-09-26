# Demo Solution

This repository contains a .NET 8 solution with the following projects:

- **WebApplicationDemo**: The API project.
- **FunctionAppDemo**: Azure Functions project.
- **WebApplicationDemo.Tests**: Tests for the API.
- **FunctionAppDemo.Tests**: Tests for the Azure Functions.

The Postman collection and environments are here too.
When testing the production environment in Azure, the first request may be used to wake up the server. If it fails, please try again.

## Running Locally

### 1. Start the SQL Server Database (Required for both projects)

Navigate to the `WebApplicationDemo/SQLServer` directory and run:

```sh
docker build -t my-sqlserver .
docker run -p 1433:1433 --name sqlserver -d my-sqlserver
```

### 2. Apply Database Migrations (API Project)

Open the `WebApplicationDemo` project in Visual Studio. Then, open the Package Manager Console and run:

```ps
# Run these commands to apply migrations
Add-Migration InitialCreate
Add-Migration AddFunctionsAndProcedures
Update-Database
```

### 3. Run the projects

Run both `WebApplicationDemo` and `FunctionAppDemo` projects from Visual Studio. It could be run with docker too, but I suggest running it from Visual Studio for simplicity.

## Testing

- Tests are provided in `WebApplicationDemo.Tests` and `FunctionAppDemo.Tests`.

## API Testing with Postman

- Use the provided Postman collection and environments for testing the API and Functions.
- For local testing, use the `Demo - Local` environment.
- For testing deployed API and Azure Functions in Azure, use the `Demo - Production` environment.

I created pre-req and post-res scripts in Postman to test the endpoints. I suggest using the Postman Runner to execute the requests in order.
The following order would test all endpoints, except the DELETE endpoint, which you can test separately.

POST Company -> GET All Companies -> GET Company -> PUT Company -> POST Store -> GET All Stores -> GET Store -> PUT Store -> POST Product -> GET All Products -> GET Product -> PUT Product

I didn't create a staging environment because App Services needs standard or premium instances to support deployment slots, and I used free tiers for this demo.
