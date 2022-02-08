# Welcome to Martial Robots test

This is my implementation for Martial robots DCSL Code Challenge

# Backend (Asp .Net Core)

Requirements:
- The project requires [.NET Core 2.0](https://dotnet.microsoft.com/download/dotnet-core/2.0). 
Just open the source code in Visual Studio 2017+ and press F5.

## Docker
Backend project has a Dockerfile to run and deploy compiled files into separated docker container.
Usage:
```
docker build -f Dockerfile -t backend .
docker run -it -p 7147:443 -p 5147:80 backend
```

Unresolved https issues:
I couldn't have time to configure the environment with a self-signed certificate to allow https. I tried this commands:
```
docker run --rm -it -p 5147:80 -p 7147:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=7147 -e ASPNETCORE_ENVIRONMENT=Development -v $Env:APPDATA\microsoft\UserSecrets:/root/.microsoft/usersecrets -v $Env:USERPROFILE\.dotnet:/root/.aspnet/https/ backend

docker run -it -p 7147:443 -e ASPNETCORE_Kestrel__Certificates__Default__Path=/app/MartianRobotWebApi/cert/dev-cert.pfx -e ASPNETCORE_Kestrel__Certificates__Default__Password=development -e "ASPNETCORE_URLS=https://+;http://+" backend
```

## Code coverage
Backend project includes also Coverlet and ReportGenerator libaries which can generate code coverage report for all tests. You can run and generate the report executing the script file 'execute-code-coverage-report.ps1' in a PowerShel (v.7) console

# FrontEnd (Angular latest)
Requirements:
- Node

Run in console:
```
npm install
ng serve
```
## Docker
Frontend project has a Dockerfile to run and deploy transpiled files into separated docker container.
```
docker build -t frontend .
docker run -d -it -p 4200:80 frontend
```

# Compatible IDEs

Tested on:
- Visual Studio 2022 Community edition

