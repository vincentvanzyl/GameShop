using System.Security.Cryptography;
using System.Text;
using GamesGlobal.Utilities.Config;

namespace GamesGlobal.Utilities.Security;

public static class Encryption
{
	public static string? HashString(string stringToHash, string salt)
	{
		if (string.IsNullOrWhiteSpace(stringToHash))
			return null;

		if (salt == null)
			throw new ArgumentNullException("salt",
				@"Salt cannot be null - if you intended to hash without salt, call HashStringWithoutSalt");

		var saltyString = string.Concat(stringToHash, salt);

		return HashString(saltyString);
	}

	public static string? HashSearchableString(string stringToHash)
	{
		if (string.IsNullOrEmpty(stringToHash))
			return null;

		var searchSalt = GamesGlobalSettings.Instance.Security.SearchSalt;
		if (string.IsNullOrEmpty(searchSalt))
			throw new Exception(
				"SearchSalt is needed for searching through encrypted data.  Please make sure it has been set in the config.");

		var saltyString = string.Concat(stringToHash.ToLower(), searchSalt);

		return HashString(saltyString);
	}

	private static string? HashString(string stringToHash)
	{
		var key = GamesGlobalSettings.Instance.Security.StrongKey;
		if (string.IsNullOrEmpty(key))
			throw new Exception(
				"StrongKey is needed for encryption.  Please make sure it has been set in the config.");

		var strongKeyBytes = Convert.FromBase64String(key);
		var stringToHashBytes = Encoding.UTF8.GetBytes(stringToHash);
		string hashedResult;

		using var myHasher = new HMACSHA512(strongKeyBytes);
		var hashValue = myHasher.ComputeHash(stringToHashBytes);
		hashedResult = Convert.ToBase64String(hashValue);

		return hashedResult;
	}

	public static string? Encrypt(string stringToEncrypt)
	{
		if (string.IsNullOrEmpty(stringToEncrypt))
			return null;

		var key = KeyStore.GetKey();
		var textToEncryptBytes = Encoding.UTF8.GetBytes(stringToEncrypt);
		using var aes = Aes.Create();
		//explicitly set mode and padding rather than relying on defaults
		aes.Mode = CipherMode.CBC;
		aes.Padding = PaddingMode.PKCS7;
		aes.KeySize = 256;
		aes.BlockSize = 128;

		var iv = aes.IV;
		using var cryptoTransform = aes.CreateEncryptor(key, iv);
		//do encryption
		var encrypted = cryptoTransform.TransformFinalBlock(textToEncryptBytes, 0, textToEncryptBytes.Length);
		//prefix IV to result
		var encryptedWithIv = new byte[encrypted.Length + iv.Length];
		iv.CopyTo(encryptedWithIv, 0);
		encrypted.CopyTo(encryptedWithIv, iv.Length);
		var resultString = Convert.ToBase64String(encryptedWithIv);

		return resultString;
	}

	public static string? Decrypt(string stringToDecrypt)
	{
		if (string.IsNullOrWhiteSpace(stringToDecrypt))
			return null;

		var key = KeyStore.GetKey();
		using var aes = Aes.Create();
		//explicitly set mode and padding rather than relying on defaults
		aes.Mode = CipherMode.CBC;
		aes.Padding = PaddingMode.PKCS7;
		aes.KeySize = 256;
		aes.BlockSize = 128;

		//get IV 
		var ivAndEncryptedBytes = Convert.FromBase64String(stringToDecrypt);
		var iv = new byte[aes.IV.Length];
		var encryptedBytes = new byte[ivAndEncryptedBytes.Length - iv.Length];
		Array.Copy(ivAndEncryptedBytes, 0, iv, 0, iv.Length);
		Array.Copy(ivAndEncryptedBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

		using var cryptoTransform = aes.CreateDecryptor(key, iv);
		//do decryption
		var decrypted = cryptoTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
		var resultString = Encoding.UTF8.GetString(decrypted);
		
		return resultString;
	}

	//only being used for 227 business forgotpassword endpoint for shared encryption from their api 
	private const string BasicSalt = "227Bus!ne55";
	private const string BasicKey = "8723775a-972f-4c27-ac45-eba136d0e589";

	public static string? BasicEncrypt(string stringToEncrypt)
	{
		if (!string.IsNullOrWhiteSpace(stringToEncrypt))
			return null;

		var bytes = Encoding.ASCII.GetBytes(BasicSalt);
		var key = new Rfc2898DeriveBytes(BasicKey, bytes);

		var aesAlg = Aes.Create();
		aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
		aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

		var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
		var msEncrypt = new MemoryStream();
		using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
		using (var swEncrypt = new StreamWriter(csEncrypt))
		{
			swEncrypt.Write(stringToEncrypt);
		}

		return Convert.ToBase64String(msEncrypt.ToArray());
	}

	public static string? BasicDecrypt(string stringToDecrypt)
	{
		if (string.IsNullOrWhiteSpace(stringToDecrypt))
			return null;

		var bytes = Encoding.ASCII.GetBytes(BasicSalt);
		var key = new Rfc2898DeriveBytes(BasicKey, bytes);

		var aesAlg = Aes.Create();
		aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
		aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

		var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
		var cipher = Convert.FromBase64String(stringToDecrypt);

		using var msDecrypt = new MemoryStream(cipher);
		using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
		using var srDecrypt = new StreamReader(csDecrypt);
		
		var decryptedString = srDecrypt.ReadToEnd();
		srDecrypt.Close();

		return decryptedString;
	}
}