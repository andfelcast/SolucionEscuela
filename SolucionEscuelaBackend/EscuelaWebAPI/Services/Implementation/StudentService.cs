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
                lstStudents.Add(Utilities.ConvertToDto(item)!);
            }
            return new ResponseDTO { 
                IsValid = lstStudents.Count > 0,
                Message = lstStudents.Count > 0 ? "Exitoso" : "No hay registros",
                ResultData = lstStudents.Count > 0 ? lstStudents : null
            };
        }

        public async Task<ResponseDTO> GetById(RequestDTO dto)
        {
            ResponseDTO response = new ResponseDTO();
            StudentDTO student = Utilities.ConvertToDto(await _studentRepository.GetById(Convert.ToInt32(dto.Id)))!;
            return new ResponseDTO
            {
                IsValid = student != null,
                Message = student != null ? "Exitoso" : "No hay registros",
                ResultData = student
            };
        }

        public async Task<ResponseDTO> CreateNew(RequestDTO dto)
        {
            StudentDTO student = dto.Body as StudentDTO;
            string userName = await _studentRepository.Register(Utilities.ConvertToEntity(student!));
            return new ResponseDTO
            {
                IsValid = userName != null,
                Message = userName != null ? "Exitoso" : "No hay registros",
                ResultData = userName
            };
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
