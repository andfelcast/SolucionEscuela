using EscuelaWebAPI.DTO.Subject;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EscuelaWebAPI.DTO.Student
{
    public class StudentDTO : RegisterDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Credits { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public List<SubjectDTO> Subjects { get; set; }
    }
}
