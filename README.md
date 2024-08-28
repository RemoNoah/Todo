# Dotnet Api template

## Description

This is a project template for a c# api with enebled Docker support.
It contains a default User and a Role model which are used to implement a simple Role based Authorization.
There is a Endpoint for Registration and login which return a JWT (Jason Web Token)

## Setup

Change all Settings in the appsettings.json in the DotnetApiTemplate.Api project.
The connection string could look like this:
server=localhost,1433;uid=sa;pwd=Init1234;database=DotnetApiTemplate;TrustServerCertificate=true

Add your wanted roles in the DBContext in the DotnetApiTemplate.Dataaccess project

## Docker

If you want to use this in a Docker Container there is a Dockerfile that you can use.

To Use the Api with a mssql docker container there are some commande that you have to execute:

### Create DB Container

```shell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourPassword>" -p 1433:1433  --name <ContainerName> -d mcr.microsoft.com/mssql/server:2022-latest
```

### DB changes

If the Database structur changed you can use the dotnet ef tools the create migrations.

Install ef tool:

```sh
dotnet tool install --global dotnet-ef
```

Chnage to datAccess project:

```sh
cd .\yourpath\DotnetApiTemplate.DataAccess\Migrations\
```

Update DB

```shell
dotnet ef database update
```

### Create new Migration

```shell
dotnet ef migrations add <migrationName>
```

## Project structur

In this Project I used the Repository pattern in combination with Unit of work Pattern
