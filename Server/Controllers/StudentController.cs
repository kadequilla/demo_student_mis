using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contracts;
using Server.Responses;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IStudentRepository studentRepository) : AbstractControllerBase<Student>
    {
        public override ActionResult<GeneralResponse> GetAll()
        {
            try
            {
                var res = studentRepository.GetAll();
                return Ok(studentRepository.GetAll());
            }
            catch (Exception e)
            {
                return new GeneralResponse(false, e.Message);
            }
        }

        public override Task<GeneralResponse> GetById(int id) => studentRepository.GetById(id)!;
        public override Task<GeneralResponse> Post(Student obj) => studentRepository.Post(obj);
        public override Task<GeneralResponse> Put(Student obj) => studentRepository.Put(obj);
        public override Task<GeneralResponse> Delete(int id) => studentRepository.Delete(id);
    }
}