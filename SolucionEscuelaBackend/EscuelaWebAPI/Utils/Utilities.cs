using System.Text;
using System.Security.Cryptography;
using EscuelaWebAPI.DTO.Student;
using Escuela.Domain.Entities;
namespace EscuelaWebAPI.Utils
{
    public static class Utilities
    {
        public static StudentDTO ConvertToDto(Student item)
        {
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
    }
}
