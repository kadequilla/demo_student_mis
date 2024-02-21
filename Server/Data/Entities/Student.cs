using Server.Data.Entities;

namespace Data.Entities;

public class Student : BaseEntity
{
    public int Id { get; set; }
    public string? FullName { get; set; }
}