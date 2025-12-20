using System;
using System.Collections.Generic;

namespace Escuela.Domain.Entities;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<StudentXsubject> StudentXsubjects { get; set; } = new List<StudentXsubject>();
}
