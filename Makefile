mig:
	cd ./MinyToDo.Data && dotnet ef --startup-project ../MinyToDo.WebAPI/ migrations add $(name) && cd ..
migrm:
	cd ./MinyToDo.Data && dotnet ef migrations remove --startup-project ../MinyToDo.WebAPI/ && cd ..
dbup:
	cd ./MinyToDo.Data && dotnet ef --startup-project ../MinyToDo.WebAPI/ database update && cd ..
r:
	cd ./MinyToDo.WebAPI/ && dotnet run && cd ..
wr:
	cd ./MinyToDo.WebAPI/ && dotnet watch run && cd ..
