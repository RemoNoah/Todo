using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text.Json.Serialization;

namespace Todo.Domain.Models;

/// <summary>
/// Represents an User entity
/// </summary>
public class User
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User" /> class.
    /// </summary>
    public User()
    {
        Roles = [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="password">The password.</param>
    public User(string password)
    {
        Roles = [];
        Salt = GenerateSalt();
        Hash = ComputeHash(password, Salt);
    }

    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Username.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>1
    /// Gets or sets the FirstName.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the salt.
    /// </summary>
    [JsonIgnore]
    public string? Salt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hash.
    /// </summary>
    [JsonIgnore]
    public string? Hash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Roles.
    /// </summary>
    public virtual ICollection<Role> Roles { get; set; }

    /// <summary>
    /// Generates the salt.
    /// </summary>
    /// <returns>The salt</returns>
    private static string GenerateSalt()
    {
        byte[] bytes = new byte[128 / 8];
        Random rng = new();
        rng.NextBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Computes the hash.
    /// </summary>
    /// <param name="password">The bytes to hash.</param>
    /// <param name="salt">The salt.</param>
    /// <returns>The Hash</returns>
    private static string ComputeHash(string password, string salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password: password,
           salt: Convert.FromBase64String(salt),
           prf: KeyDerivationPrf.HMACSHA512,
           iterationCount: 100000,
           numBytesRequested: 256 / 8));
    }

    /// <summary>
    /// Verifies the password.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>A Bool</returns>
    public bool VerifyPassword(string password)
    {
        return ComputeHash(password, Salt ?? string.Empty) == Hash;
    }
}
