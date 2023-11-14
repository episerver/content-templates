#!/bin/bash

echo "Creating databases..."

let cmsresult=1
let commerceresult=1

for i in {1..100}; do
    if test -f /var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.mdf; then
        echo "Restoring CMS DB from .mdf/.ldf"

        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '${DB_NAME}') CREATE DATABASE ${DB_NAME} ON (NAME=${DB_NAME}_data, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.mdf') LOG ON (NAME=${DB_NAME}_log, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.ldf') FOR ATTACH;"
        let cmsresult=$?
    else
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '${DB_NAME}') CREATE DATABASE ${DB_NAME} ON (NAME=${DB_NAME}_data, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.mdf') LOG ON (NAME=${DB_NAME}_log, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME}.ldf')"
        let cmsresult=$?
    fi

    if test -f /var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME_COMMERCE}.mdf; then
        echo "Restoring Commerce DB from .mdf/.ldf"
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '${DB_NAME_COMMERCE}') CREATE DATABASE ${DB_NAME_COMMERCE} ON (NAME=${DB_NAME_COMMERCE}_data, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME_COMMERCE}.mdf') LOG ON (NAME=${DB_NAME_COMMERCE}_log, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME_COMMERCE}.ldf') FOR ATTACH;"
        let commerceresult=$?
    else
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '${DB_NAME_COMMERCE}') CREATE DATABASE ${DB_NAME_COMMERCE} ON (NAME=${DB_NAME_COMMERCE}_data, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME_COMMERCE}.mdf') LOG ON (NAME=${DB_NAME_COMMERCE}_log, FILENAME='/var/opt/mssql/host_data/${DB_DIRECTORY}/${DB_NAME_COMMERCE}.ldf')"
        let commerceresult=$?
    fi

    if [ $cmsresult -eq 0 ] && [ $commerceresult -eq 0 ]; then
        echo "Creating databases completed"
        break
    else
        echo "Creating databases. Not ready yet..."
        sleep 1
    fi
done