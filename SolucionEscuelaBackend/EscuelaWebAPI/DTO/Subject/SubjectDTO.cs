using EscuelaWebAPI.DTO.Student;
using EscuelaWebAPI.DTO.Teacher;

namespace EscuelaWebAPI.DTO.Subject
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public int TeacherId { get; set; }
        public TeacherDTO Teacher { get; set; }
        public List<StudentDTO> Students { get; set; }

    }
}
