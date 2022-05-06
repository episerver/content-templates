# Empty Commerce template

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

Create two empty databases on the external database server and update the connection strings accordingly.

```bash
$ dotnet run
````
