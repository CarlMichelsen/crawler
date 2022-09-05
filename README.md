# crawler
A general purpose webcrawler initially intended for Esportal.

# required dotnet tools
dotnet tool install --global dotnet-ef --version 6.0.8


# Use docker compose
## docker api
build:
docker build -t crawlerapi -f Dockerfile .

run:
docker run --restart=always --name crawlercontainer -e "SA_PASSWORD=verysecret" -e "DATABASE_URL="Server=db\\SQLEXPRESS;Database=esportal;Trusted_Connection=True;" -p 80:80 -p 443:443 -d crawlerapi

## docker mssql
docker pull mcr.microsoft.com/mssql/server:2022-latest

docker run --restart=always --name db -v db-vol:/var/opt/mssql -e "MSSQL_PID=Express" -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=verysecret" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
