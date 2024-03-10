using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Dal.Persistence;
using GamesGlobal.Shared.Extensions;

namespace GamesGlobal.Core.Repositories;

public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(IGeneralUnitOfWork generalUnitOfWork) : base(generalUnitOfWork)
    {
    }

    protected override IRepository<UserEntity> Repository => _generalUnitOfWork.Users;

    public async Task<UserEntity?> GetByEmail(string emailAddress)
    {
        var searchHash = emailAddress.HashSearchable();
        return await Repository.GetOne(x => x.EmailSearchHash == searchHash);
    }
    
}