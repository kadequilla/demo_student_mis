using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Responses;

namespace Server.Controllers;

public abstract class AbstractControllerBase<T> : ControllerBase
{
    [HttpGet]
    [Authorize]
    public abstract ActionResult<GeneralResponse> GetAll();

    [HttpGet("{id}")]
    public abstract Task<GeneralResponse> GetById(int id);

    [HttpPost]
    public abstract Task<GeneralResponse> Post(T obj);

    [HttpPut("update")]
    public abstract Task<GeneralResponse> Put(T obj);

    [HttpDelete("delete/{id:int}")]
    public abstract Task<GeneralResponse> Delete(int id);
}