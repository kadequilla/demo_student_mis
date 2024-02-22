using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Server.Repositories.Contracts;
using Server.Responses;

namespace Server.Repositories.Implementations;

public class AppUserRepository(ApplicationDbContext context) : IAppUserRepository
{
    public GeneralResponse GetAll()
    {
        return new GeneralResponse(true, context.AppUsers.ToList());
    }

    public async Task<GeneralResponse?> GetById(int id)
    {
        var appUser = await context.AppUsers.FindAsync(id);
        return appUser is null ? new GeneralResponse(false, "Not found!") : new GeneralResponse(true, appUser);
    }

    public async Task<GeneralResponse> Post(AppUser appUser)
    {
        try
        {
            appUser.Password = BCrypt.Net.BCrypt.HashPassword(appUser.Password);
            appUser.DateCreated = DateTime.Now;
            appUser.IsActive = true;
            context.AppUsers.Add(appUser);

            await context.SaveChangesAsync();
            return new GeneralResponse(true, appUser);
        }
        catch (Exception e)
        {
            return new GeneralResponse(false, e);
        }
    }

    public async Task<GeneralResponse> Put(AppUser? obj)
    {
        if (obj is null) return new GeneralResponse(false, "Student not found!");
        if (!AppUserExists(obj.Id)) return new GeneralResponse(false, "User not found!");


        context.Entry(obj).State = EntityState.Modified;
        try
        {
            await context.SaveChangesAsync();
            return new GeneralResponse(true, obj);
        }
        catch (Exception e)
        {
            return new GeneralResponse(false, e.Message);
        }
    }

    public async Task<GeneralResponse> Delete(int id)
    {
        var appUser = await context.AppUsers.FindAsync(id);
        if (appUser is null) return new GeneralResponse(false, "User not found!");

        context.AppUsers.Remove(appUser);
        try
        {
            await context.SaveChangesAsync();
            return new GeneralResponse(true, "Successfully Deleted!");
        }
        catch (Exception e)
        {
            return new GeneralResponse(false, e.Message);
        }
    }

    private bool AppUserExists(int id)
    {
        return context.AppUsers.Any(e => e.Id == id);
    }
}