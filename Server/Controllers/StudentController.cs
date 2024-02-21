using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repositories.Contracts;
using Server.Responses;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IStudentRepository studentRepository) : ControllerBase
    {
        // GET: api/Student
        [HttpGet]
        public GeneralResponse GetStudents() => studentRepository.GetAll();

        // GET: api/Student/5
        [HttpGet("{id}")]
        public Task<GeneralResponse?> GetStudent(int id) =>
            studentRepository.GetById(id);

        [HttpPost]
        public Task<GeneralResponse> PostStudent(Student student) =>
            studentRepository.Post(student);
    }
}