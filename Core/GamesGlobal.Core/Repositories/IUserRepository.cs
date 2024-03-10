using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;

namespace GamesGlobal.Core.Repositories;

public interface IUserRepository : IReadWriteRepository<UserEntity>
{
    Task<UserEntity?> GetByEmail(string emailAddress);
}