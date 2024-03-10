namespace GamesGlobal.Shared.Models.RequestModels;

public class RegisterUserRequest
{
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}