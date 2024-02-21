using Data.Entities;
using Server.Data;
using Server.Repositories.Contracts;
using Server.Responses;

namespace Server.Repositories.Implementations;

public class StudentRepository(ApplicationDbContext context) : IStudentRepository
{
    public GeneralResponse GetAll()
    {
        return new GeneralResponse(true, context.Students.ToList());
    }


    public async Task<GeneralResponse?> GetById(int id)
    {
        var student = await context.Students.FindAsync(id);
        return student is null ? new GeneralResponse(false, "Not found!") : new GeneralResponse(true, student);
    }

    public async Task<GeneralResponse> Post(Student student)
    {
        context.Students.Add(student);
        await context.SaveChangesAsync();
        return new GeneralResponse(true, student);
    }
}