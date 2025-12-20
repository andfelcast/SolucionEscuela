using System;
using System.Collections.Generic;

namespace Escuela.Domain.Entities;

public partial class StudentXsubject
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
