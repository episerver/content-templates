# Alloy MVC template

This template should not be seen as best practices, but as a great way to learn and test Optimizely CMS. 

## How to run

Chose one of the following options to get started. 

### Windows

Prerequisities
- .NET SDK 6+
- SQL Server 2016 Express LocalDB (or later)

```bash
$ dotnet run
````

### Any OS with Docker

Prerequisities
- Docker
- Enable Docker support when applying the template

```bash
$ docker-compose up
````

> Note that this Docker setup is just configured for local development. Follow this [guide to enable HTTPS](https://github.com/dotnet/dotnet-docker/blob/main/samples/run-aspnetcore-https-development.md).

### Any OS with external database server

Prerequisities
- .NET SDK 6+
- SQL Server 2016 (or later) on a external server, e.g. Azure SQL

Create an empty database on the external database server and update the connection string accordingly.

```bash
$ dotnet run
````
