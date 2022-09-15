# crawler
A general purpose webcrawler initially intended for Esportal.

# required dotnet tools
dotnet tool install --global dotnet-ef --version 6.0.8

# local database setup
create docker volume with name "database-blob"
sudo docker run -d --mount source=database-blob,target=/sql_data --restart always --name mssql -e "SA_PASSWORD=ThisIsATestDevelopmentPassword!123" -e "MSSQL_PID=Express" -e "ACCEPT_EULA=Y" -e "LC_ALL=en_US.UTF-8" -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest