## Miny ToDo

This repository is for practising programming skills about n-layer, Web API, authentication/authorization and many more.

## Installation & Run

```sh
git clone https://github.com/halilkocaoz/miny-todo-nlayer
cd miny-todo-nlayer/MinyToDo.WebAPI/ && dotnet run
```

## Database migration

```sh
 cd ./MinyToDo.Data && dotnet ef --startup-project ../MinyToDo.WebAPI/ migrations add Migration_Name
```

or if you have make command you can use

```sh
make mig name=Migration_Name
```

## Techs

* [ASP.NET 5](https://dotnet.microsoft.com/apps/aspnet "ASP.NET 5")
* [Postgres](https://www.postgresql.org/ "Postgres")
* [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore "Entity Framework Core")
* Authentication / Authorization with [Jwt](https://jwt.io/ "Jwt") Bearer and [IdentityDbContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identitydbcontext?view=aspnetcore-5.0 "IdentityDbContext")

## ToDo for this project

* Implementations for Team  
    TeamMember, TeamCategory, TeamTask  
* Application user role specifaction  
    Normal user (can use just for her/himself)  
    Team member (can use with a team and be normal user)  
    Team manager (authorization to add a user into team, change team/its categories)  

## Swagger

![Swagger](https://github.com/halilkocaoz/miny-todo/blob/main/assets/swagger.png "Swagger")
