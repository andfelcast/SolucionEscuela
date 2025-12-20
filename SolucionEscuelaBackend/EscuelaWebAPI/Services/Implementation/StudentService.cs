using Escuela.Domain.Repositories;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.DTO.Student;
using EscuelaWebAPI.Services.Interfaces;
using EscuelaWebAPI.Utils;

namespace EscuelaWebAPI.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<ResponseDTO> GetAll()
        {
            List<StudentDTO> lstStudents = new List<StudentDTO>();
            foreach (var item in await _studentRepository.GetAll()) {
                lstStudents.Add(Utilities.ConvertToDto(item));
            }
            return null;
        }

        public async Task<ResponseDTO> GetById(RequestDTO dto)
        {
            return null;
        }

        public async Task<ResponseDTO> CreateNew(RequestDTO dto)
        {
            return null;
        }

        public async Task<ResponseDTO> Update(RequestDTO dto)
        {
            return null;
        }
        public async Task<ResponseDTO> Delete(RequestDTO dto)
        {
            return null;
        }
    }
}
