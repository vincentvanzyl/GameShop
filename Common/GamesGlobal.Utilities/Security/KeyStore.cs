using System.Security.Cryptography;
using GamesGlobal.Utilities.Config;

namespace GamesGlobal.Utilities.Security;

internal sealed class KeyStore
{
    private static readonly object _lockObject = new();
    private static byte[]? _key;

    private static readonly byte[] SaltBytes =
    [
        32, 32, 79, 117, 114, 32, 75, 101, 121, 32, 80, 114, 105, 110, 99, 105, 112, 108, 101, 115, 58, 13, 10, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 105, 116, 63, 115, 32, 102, 111, 114, 32, 117, 115, 44, 32, 13, 10, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 101, 112, 105, 99,
        32, 99, 114, 101, 97, 116, 105, 111, 110, 44, 32, 13, 10, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
        32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 111, 110, 32, 112, 117, 114, 112, 111,
        115, 101
    ];
     
    public static byte[] GetKey()
    {
        if (_key != null) return _key;
        lock (_lockObject)
        {
            _key = GenerateKey(GamesGlobalSettings.Instance.Security.StrongKey);
        }

        return _key;
    }
     
    private static byte[] GenerateKey(string? baseKey)
    {
        if (baseKey == null) throw new Exception($"Could not load settings StrongKey.");
        using var deriver = new Rfc2898DeriveBytes(baseKey, SaltBytes);
        return deriver.GetBytes(32);
    }
	
	
}