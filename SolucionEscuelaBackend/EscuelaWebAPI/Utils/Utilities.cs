using System.Text;
using System.Security.Cryptography;
using EscuelaWebAPI.DTO.Student;
using Escuela.Domain.Entities;
using EscuelaWebAPI.DTO.Subject;
using EscuelaWebAPI.DTO.Teacher;
using EscuelaWebAPI.DTO.General;

namespace EscuelaWebAPI.Utils
{
    public static class Utilities
    {
        public static StudentDTO? ConvertToDto(Student item)
        {
            if (item == null) {
                return null;
            }
            List<SubjectDTO> lstSubjects = new List<SubjectDTO>();
            foreach (var element in item.StudentXsubjects) {
                lstSubjects.Add(ConvertToDto(element.Subject)!);
            }
            return new StudentDTO
            {
                Id = item.Id,
                Active = item.Active,
                Address = item.Address,
                BirthDate = item.BirthDate,
                City = item.City,
                FirstName = item.FirstName,
                CreationDate =  item.CreationDate,
                Credits = item.StudentXsubjects.Count * 3,
                Email = item.Email,
                LastName = item.LastName,
                UserName = item.UserName,      
                Subjects = lstSubjects
            };
        }

        public static SubjectDTO? ConvertToDto(Subject item)
        {
            if (item == null) {
                return null;
            }
            return new SubjectDTO
            {
                Credits = item.Credits,
                Description = item.Description,
                Id = item.Id,
                Name = item.Name,
                TeacherId = item.TeacherId,
                Teacher = ConvertToDto(item.Teacher)!
            };
        }

        public static TeacherDTO? ConvertToDto(Teacher item)
        {
            if (item == null)
            {
                return null;
            }
            return new TeacherDTO
            {                
                Id = item.Id,
                Name = item.Name,                                
            };
        }

        public static string Encrypt(string data) {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                // Convert byte array to a string (hex format)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static Student ConvertToEntity(StudentDTO dto)
        {
            return new Student
            {
                Active = dto.Active,
                BirthDate = dto.BirthDate,
                Address = dto.Address,
                City = dto.City,
                CreationDate = dto.CreationDate,
                Email = dto.Email,
                FirstName = dto.FirstName,
                Id = dto.Id,
                LastName = dto.LastName,
                Password = dto.Password,
                Phone = dto.Phone,
                UserName = dto.UserName
            };
        }
    }
}
