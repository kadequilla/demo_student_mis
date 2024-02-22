using Server.Responses;

namespace Server.Repositories.Contracts;

public interface IBaseRepository<in T>
{
    GeneralResponse GetAll();
    Task<GeneralResponse?> GetById(int id);
    Task<GeneralResponse> Post(T obj);
    Task<GeneralResponse> Put(T obj);
    Task<GeneralResponse> Delete(int id);
}