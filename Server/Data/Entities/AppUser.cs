using System.ComponentModel.DataAnnotations;
using Server.Data.Entities;

namespace Data.Entities;

public class AppUser : BaseEntity
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
}