using Escuela.Domain.Entities;
using Escuela.Domain.Repositories;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.DTO.Student;
using EscuelaWebAPI.Services.Interfaces;
using EscuelaWebAPI.Utils;
using System.Text.Json;

namespace EscuelaWebAPI.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly JsonSerializerOptions options;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
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
            try
            {
                StudentDTO student = JsonSerializer.Deserialize<StudentDTO>(dto.Body.ToString(), options);
                string userName = await _studentRepository.Register(Utilities.ConvertToEntity(student!));
                return new ResponseDTO
                {
                    IsValid = userName != null,
                    Message = userName != null ? "Exitoso" : "No hay registros",
                    ResultData = userName
                };
            }
            catch (Exception)
            {
                return new ResponseDTO
                {
                    IsValid = false,
                    Message = "No exitoso",
                    ResultData = null
                };
            }
        }

        public async Task<ResponseDTO> AddSubjects(RequestDTO dto) {
            int studentId = Convert.ToInt32(dto.Id);
            int[] subjectIds = (int[])dto.Body;
            bool success = await _studentRepository.AddSubjects(studentId, subjectIds);
            return new ResponseDTO
            {
                IsValid = success,
                Message = success ? "Exitoso" : "Error en actualización",
                ResultData = null
            };
        }

        public async Task<ResponseDTO> Update(RequestDTO dto)
        {
            StudentDTO student = (StudentDTO)dto.Body;
            bool success = await _studentRepository.Update(Utilities.ConvertToEntity(student));
            return new ResponseDTO
            {
                IsValid = success,
                Message = success ? "Exitoso" : "Error en actualización",
                ResultData = null
            };

        }
        public async Task<ResponseDTO> Delete(RequestDTO dto)
        {
            int id = Convert.ToInt32(dto.Id);
            bool success = await _studentRepository.Delete(id);
            return new ResponseDTO
            {
                IsValid = success,
                Message = success ? "Exitoso" : "No hay registros por eliminar",
                ResultData = null
            };
        }
    }
}
