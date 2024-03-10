using GamesGlobal.Utilities.Security;

namespace GamesGlobal.Shared.Extensions;

public static class StringExtensions
{
    public static string? Decrypt(this string input)
    {
        try
        {
            return string.IsNullOrEmpty(input) ? input : Encryption.Decrypt(input);
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Failed to decrypt '{input}' [{e.Message}]..", e);
        }
    }
    
    public static string? Encrypt(this string input) => Encryption.Encrypt(input);

    public static string? Hash(this string input, string salt) => Encryption.HashString(input, salt);
	
    public static string? Hash(this string input, Guid salt) => Encryption.HashString(input, salt.ToString());
	
    public static string? HashSearchable(this string input) => Encryption.HashSearchableString(input);
}