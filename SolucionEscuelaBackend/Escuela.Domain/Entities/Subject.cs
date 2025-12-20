using System;
using System.Collections.Generic;

namespace Escuela.Domain.Entities;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Credits { get; set; }

    public int TeacherId { get; set; }

    public DateTime CreationDate { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<StudentXsubject> StudentXsubjects { get; set; } = new List<StudentXsubject>();

    public virtual Teacher Teacher { get; set; } = null!;
}
