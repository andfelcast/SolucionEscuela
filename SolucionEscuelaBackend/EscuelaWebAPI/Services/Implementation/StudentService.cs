using Escuela.Domain.Repositories;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.Services.Interfaces;

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
