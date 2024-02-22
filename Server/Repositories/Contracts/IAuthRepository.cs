using Data.DTOs;
using Server.Responses;

namespace Server.Repositories.Contracts;

public interface IAuthRepository
{
    LoginResponse Login(LoginDto loginDto);
}