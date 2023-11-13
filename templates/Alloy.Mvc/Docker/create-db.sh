#!/bin/bash

echo "Creating database..."

let result=1

for i in {1..100}; do
    if test -f /var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.mdf; then
        echo "Restoring from .mdf/.ldf"
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '${DB_NAME}') CREATE DATABASE ${DB_NAME} ON (NAME=${DB_NAME}_data, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.mdf') LOG ON (NAME=${DB_NAME}_log, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.ldf') FOR ATTACH;"
        let result=$?
    else
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '${DB_NAME}') CREATE DATABASE ${DB_NAME} ON (NAME=${DB_NAME}_data, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.mdf') LOG ON (NAME=${DB_NAME}_log, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.ldf')"
        let result=$?
    fi
    if [ $result -eq 0 ]; then
        echo "Creating database completed"
        break
    else
        echo "Creating database. Not ready yet..."
        sleep 1
    fi
done