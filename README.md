# GameShop
GameShop using repository pattern with EF

Setup:
- Setup Database. I use SqlLocalDb, check the connection string and start server if needed or update connection string
- Run the following command to generate database: dotnet ef database update --project Core\GamesGlobal.Dal.EntityFramework\GamesGlobal.Dal.EntityFramework.csproj --startup-project GamesGlobal.Api\GamesGlobal.Api.csproj --context GamesGlobal.Dal.EntityFramework.GamesGlobalContext --configuration Debug 20240311205231_Initial
- Run the Api project: dotnet run --project GamesGlobal.Api/GamesGlobal.Api.csproj --launch-profile https
