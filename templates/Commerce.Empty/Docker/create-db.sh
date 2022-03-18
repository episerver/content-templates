#!/bin/bash

for i in {1..100}; do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '${DB_NAME}') CREATE DATABASE ${DB_NAME} ON (NAME=${DB_NAME}_data, FILENAME='/var/opt/mssql/host_data/${DB_NAME}.mdf') LOG ON (NAME=${DB_NAME}_log, FILENAME='/var/opt/mssql/host_data/${DB_NAME}.ldf')"
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '${DB_NAME_COMMERCE}') CREATE DATABASE ${DB_NAME_COMMERCE} ON (NAME=${DB_NAME_COMMERCE}_data, FILENAME='/var/opt/mssql/host_data/${DB_NAME_COMMERCE}.mdf') LOG ON (NAME=${DB_NAME_COMMERCE}_log, FILENAME='/var/opt/mssql/host_data/${DB_NAME_COMMERCE}.ldf')"
    if [ $? -eq 0 ]; then
        echo "Creating database completed"
        break
    else
        echo "Creating database. Not ready yet..."
        sleep 1
    fi
done