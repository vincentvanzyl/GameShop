using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Dal.Entities;

public class UserEntity : BaseEntity
{
    public string Name { get; set; }
    public string EmailAddress { get; set; }
}