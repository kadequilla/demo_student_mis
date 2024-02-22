using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contracts;
using Server.Responses;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController(IAppUserRepository appUserRepository) : AbstractControllerBase<AppUser>
    {
        public override ActionResult<GeneralResponse> GetAll()
        {
            return Ok(appUserRepository.GetAll());
        }

        public override Task<GeneralResponse> GetById(int id) => appUserRepository.GetById(id)!;
        public override Task<GeneralResponse> Post(AppUser obj) => appUserRepository.Post(obj);
        public override Task<GeneralResponse> Put(AppUser obj) => appUserRepository.Put(obj);
        public override Task<GeneralResponse> Delete(int id) => appUserRepository.Delete(id);
    }
}