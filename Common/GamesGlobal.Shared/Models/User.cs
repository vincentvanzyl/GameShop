using GamesGlobal.Shared.Enums;

namespace GamesGlobal.Shared.Models;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Roles Role { get; set; }
}