using Data.Entities;
using Server.Responses;

namespace Server.Repositories.Contracts;

public interface IStudentRepository
{
    GeneralResponse GetAll();
    Task<GeneralResponse?> GetById(int id);
    Task<GeneralResponse> Post(Student student);
}