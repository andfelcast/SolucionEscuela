using Escuela.Domain.Entities;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.DTO.Student;
using EscuelaWebAPI.DTO.Subject;
using EscuelaWebAPI.DTO.Teacher;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
                DocumentNumber = item.DocumentNumber,
                City = item.City,
                FirstName = item.FirstName,
                CreationDate =  item.CreationDate,
                Credits = item.StudentXsubjects.Count * 3,
                Email = item.Email,
                Phone = item.Phone,
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
            List<SubjectDTO> lstSubjects = new List<SubjectDTO>();
            foreach (var element in item.Subjects)
            {
                lstSubjects.Add(ConvertToDto(element)!);
            }
            return new TeacherDTO
            {                
                Id = item.Id,
                Name = item.Name,
                Active = item.Active,
                EntryDate = item.EntryDate,
                Subjects = lstSubjects
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

        public static string GenerateJWT(StudentDTO student)
        {
            //crear la informacion del usuario para token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
                new Claim(ClaimTypes.Name, student.UserName!),
                new Claim(ClaimTypes.Email, student.Email)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F8096D78-03DA-4911-B291-5E6A35ECF058"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }

        public static Student ConvertToEntity(StudentDTO dto)
        {
            return new Student
            {
                Active = dto.Active,
                DocumentNumber = dto.DocumentNumber,
                Address = dto.Address,
                City = dto.City,
                CreationDate = dto.CreationDate,
                Email = dto.Email,
                FirstName = dto.FirstName,
                Id = dto.Id,
                LastName = dto.LastName,
                Password = Encrypt(dto.Password),
                Phone = dto.Phone,
                UserName = dto.UserName
            };
        }

        public static Subject ConvertToEntity(SubjectDTO dto) {
            return new Subject
            {
                Active = dto.Active,
                Credits = dto.Credits,
                Description = dto.Description,
                Name = dto.Name,
                Id = dto.Id,
                TeacherId = dto.TeacherId
            };
        }

        public static Teacher ConvertToEntity(TeacherDTO dto) {
            return new Teacher
            {
                Active = dto.Active,
                EntryDate = dto.EntryDate,
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
