using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data.Context;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Repositories.Contracts;
using Server.Responses;

namespace Server.Repositories.Implementations;

public class AuthRepository(ApplicationDbContext context, IConfiguration? configuration) : IAuthRepository
{
    public LoginResponse Login(LoginDto loginDto)
    {
        try
        {
            var user = context.AppUsers.SingleOrDefault(u => u.Email == loginDto.Email);
            if (user is null) return new LoginResponse(false, "User not found!", "");

            var token = GenerateToken(user);
            if (token.Equals("")) return new LoginResponse(false, "Error on generating token", "");

            return PasswordMatched(loginDto, user)
                ? new LoginResponse(true, user, token)
                : new LoginResponse(false, "User not found!", "");
        }
        catch (Exception e)
        {
            return new LoginResponse(false, e.Message, "");
        }
    }

    private bool PasswordMatched(dynamic loginDto, dynamic user) =>
        BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);

    private string GenerateToken(AppUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var confKey = configuration!["Jwt:Key"] ?? null;
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];

        if (confKey is null) return "";

        var key = Encoding.ASCII.GetBytes(confKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}