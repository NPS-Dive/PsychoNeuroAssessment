﻿using PNA.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PNA.AuthService.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword ( string password )
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public bool VerifyPassword ( string password, string hash )
    {
        var computedHash = HashPassword(password);
        return computedHash == hash;
    }
}