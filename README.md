## Comandos do projeto
```
docker compose up
dotnet restore
dotnet user-secrets set "ConnectionStrings:PersistenceConnectionString" "" --project src/EmployeeSample.WebAPI
dotnet user-secrets set "ConnectionStrings:PersistenceTestsConnectionString" "" --project src/EmployeeSample.WebAPI
dotnet user-secrets list --project src/EmployeeSample.WebAPI
dotnet build src/EmployeeSample.Database /p:NetCoreBuild=true
dotnet test tests/EmployeeSample.UnitTests
dotnet test tests/EmployeeSample.IntegrationTests
dotnet run --project src/EmployeeSample.WebAPI
```

```
Server=127.0.0.1,1434;Initial Catalog=employeesample;Persist Security Info=False;User ID=sa;Password=Test@231;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;
```