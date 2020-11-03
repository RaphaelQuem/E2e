Infra:
    Docker Desktop for Windows (Usando WSL2)
Frontend:
    NodeJS
    ReactJS
Backend:
    ASP.NET Core 3.1
Database:
    Sql Server Express

Como rodar:

Em um terminal, mova para a pasta para onde clonou o projeto e execute os seguintes comandos:

docker build -t "e2e-front:react" Client
docker build -t "e2e-back:netcore" Server

docker run -v ${PWD}:/app -v /app/node_modules -p 3001:3000 --rm e2e-front:react