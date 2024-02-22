using System.ComponentModel.DataAnnotations;
using Data.Entities;

namespace Data.Entities;

public class AppUser : BaseEntity
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    [Required] [EmailAddress] public string Email { get; set; }
    [Required] public string Password { get; set; }
}