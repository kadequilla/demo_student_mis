﻿using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
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
        try
        {
            student.DateCreated = DateTime.Now;
            student.IsActive = true;
            context.Students.Add(student);

            await context.SaveChangesAsync();
            return new GeneralResponse(true, student);
        }
        catch (Exception e)
        {
            return new GeneralResponse(false, e);
        }
    }

    public async Task<GeneralResponse> Put(Student? obj)
    {
        if (obj is null) return new GeneralResponse(false, "Student not found!");
        if (!StudentExists(obj.Id)) return new GeneralResponse(false, "Student not found!");

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
        var student = await context.Students.FindAsync(id);
        if (student is null) return new GeneralResponse(false, "Student not found!");

        context.Students.Remove(student);
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

    private bool StudentExists(int id)
    {
        return context.Students.Any(e => e.Id == id);
    }
}