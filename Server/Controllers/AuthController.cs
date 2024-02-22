using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contracts;
using Server.Responses;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository authRepository) : ControllerBase
    {
        [HttpPost]
        public LoginResponse Auth(LoginDto loginDto) => authRepository.Login(loginDto);
    }
}