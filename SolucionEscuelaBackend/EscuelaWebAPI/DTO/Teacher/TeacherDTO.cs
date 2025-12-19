using EscuelaWebAPI.DTO.Subject;

namespace EscuelaWebAPI.DTO.Teacher
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubjectDTO> Subjects { get; set; }
    }
}
