using System.Security.Cryptography;

public static class PasswordHasher 
{
    private static readonly int SaltSize = 128 / 8;
    private static readonly int KeySize = 256 / 8;
    private static readonly int Iterations = 10000;
    private static readonly char Delimiter = ';';
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);

        return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public static bool VerifyPassword(string hashedPassword, string inputPassword)
    {
        var parts = hashedPassword.Split(Delimiter);
        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        // hash the given password, to compare it with the stored hash
        var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, HashAlgorithm, KeySize);

        // compare the two hashes in a fixed time operation
        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}
