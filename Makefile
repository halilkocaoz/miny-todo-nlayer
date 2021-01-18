mig:
	cd ./MinyToDo.Data && dotnet ef --startup-project ../MinyToDo.Api/ migrations add $(name) && cd ..
migrm:
	cd ./MinyToDo.Data && dotnet ef migrations remove --startup-project ../MinyToDo.Api/ && cd ..
dbup:
	cd ./MinyToDo.Data && dotnet ef --startup-project ../MinyToDo.Api/ database update && cd ..
r:
	cd ./MinyToDo.Api/ && dotnet run && cd ..
wr:
	cd ./MinyToDo.Api/ && dotnet watch run && cd ..
