using System;
using System.Collections.Generic;

namespace Escuela.Domain.Entities;

public partial class Teacher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly EntryDate { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
