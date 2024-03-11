# GameShop
GameShop using repository pattern with EF

**Setup**:
- Setup Database. I use SqlLocalDb, check the connection string and start server if needed or update connection string
- Run the following command to generate database: dotnet ef database update --project Core\GamesGlobal.Dal.EntityFramework\GamesGlobal.Dal.EntityFramework.csproj --startup-project GamesGlobal.Api\GamesGlobal.Api.csproj --context GamesGlobal.Dal.EntityFramework.GamesGlobalContext --configuration Debug 20240311205231_Initial
- Run the Api project: dotnet run --project GamesGlobal.Api/GamesGlobal.Api.csproj --launch-profile https

**Requirements**:
- .net 8 runtime

**Features**:
- Code First Entity Framework implemented in the repository pattern with dependency injection
- User PI encrypted at rest with search hashing
- JWT Token authentication
- Automapper used to map between entity models and dto's
- Specific admin key used for admin features like adding and removing games
- Basic cart CRUD
- Simple project structure with seperation vs implementation to change EF to something else if you should choose to do so.

**TODO and other Improvement**
- Change JWT token to OAuth2
- Add orders for converting cart on checkout
- Game list has image byte[] stored in db and returned as base64, should upload to S3 bucket or similar storage and persist url instead
