using Escuela.Domain.Repositories;
using Escuela.Infrastructure.Repositories;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.DTO.Student;
using EscuelaWebAPI.DTO.Teacher;
using EscuelaWebAPI.Services.Interfaces;
using EscuelaWebAPI.Utils;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EscuelaWebAPI.Services.Implementation
{        
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly JsonSerializerOptions options;

        public TeacherService(ITeacherRepository teacherRepository) {
            _teacherRepository = teacherRepository;
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ResponseDTO> GetAll() {
            List<TeacherDTO> lstTeachers = new List<TeacherDTO>();
            foreach (var item in await _teacherRepository.GetAll())
            {
                lstTeachers.Add(Utilities.ConvertToDto(item)!);
            }
            return new ResponseDTO
            {
                IsValid = lstTeachers.Count > 0,
                Message = lstTeachers.Count > 0 ? "Exitoso" : "No hay registros",
                ResultData = lstTeachers.Count > 0 ? lstTeachers : null
            };
        }

        public async Task<ResponseDTO> GetById(RequestDTO dto)
        {
            ResponseDTO response = new ResponseDTO();
            TeacherDTO teacher = Utilities.ConvertToDto(await _teacherRepository.GetById(Convert.ToInt32(dto.Id)))!;
            return new ResponseDTO
            {
                IsValid = teacher != null,
                Message = teacher != null ? "Exitoso" : "No hay registros",
                ResultData = teacher
            };
        }

        public async Task<ResponseDTO> CreateNew(RequestDTO dto)
        {
            TeacherDTO teacher = dto.Body as TeacherDTO;
            string newId = await _teacherRepository.Insert(Utilities.ConvertToEntity(teacher!));
            return new ResponseDTO
            {
                IsValid = newId != null,
                Message = newId != null ? "Exitoso" : "No hay registros",
                ResultData = newId
            };
        }

        public async Task<ResponseDTO> Update(RequestDTO dto)
        {
            TeacherDTO teacher = dto.Body as TeacherDTO;
            bool success = await _teacherRepository.Update(Utilities.ConvertToEntity(teacher));
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
            bool success = await _teacherRepository.Delete(id);
            return new ResponseDTO
            {
                IsValid = success,
                Message = success ? "Exitoso" : "No hay registros por eliminar",
                ResultData = null
            };
        }


    }
}
