FROM mcr.microsoft.com/mssql/server:2019-latest AS base

ENV ACCEPT_EULA=Y

USER root

WORKDIR /src
COPY ./Docker/docker-create-db.sh .
RUN chmod +x /src/docker-create-db.sh

USER mssql

EXPOSE 1433

ENTRYPOINT /src/docker-create-db.sh & /opt/mssql/bin/sqlservr
