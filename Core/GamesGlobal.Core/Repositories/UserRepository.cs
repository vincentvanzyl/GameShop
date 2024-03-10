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

    public Task<UserEntity?> GetByEmail(string emailAddress)
    {
        var encryptedEmailAddress = emailAddress.Encrypt();
        return Repository.GetOne(x => x.EmailAddress == encryptedEmailAddress);
    }
    
}