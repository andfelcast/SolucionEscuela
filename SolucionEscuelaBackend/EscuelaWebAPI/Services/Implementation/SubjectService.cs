using Escuela.Domain.Repositories;
using Escuela.Infrastructure.Repositories;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.DTO.Subject;
using EscuelaWebAPI.DTO.Teacher;
using EscuelaWebAPI.Services.Interfaces;
using EscuelaWebAPI.Utils;

namespace EscuelaWebAPI.Services.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<ResponseDTO> GetAll()
        {
            List<SubjectDTO> lstSubjects = new List<SubjectDTO>();
            foreach (var item in await _subjectRepository.GetAll())
            {
                lstSubjects.Add(Utilities.ConvertToDto(item)!);
            }
            return new ResponseDTO
            {
                IsValid = lstSubjects.Count > 0,
                Message = lstSubjects.Count > 0 ? "Exitoso" : "No hay registros",
                ResultData = lstSubjects.Count > 0 ? lstSubjects : null
            };
        }

        public async Task<ResponseDTO> GetById(RequestDTO dto)
        {
            ResponseDTO response = new ResponseDTO();
            SubjectDTO subject = Utilities.ConvertToDto(await _subjectRepository.GetById(Convert.ToInt32(dto.Id)))!;
            return new ResponseDTO
            {
                IsValid = subject != null,
                Message = subject != null ? "Exitoso" : "No hay registros",
                ResultData = subject
            };
        }

        public async Task<ResponseDTO> CreateNew(RequestDTO dto)
        {
            SubjectDTO subject = dto.Body as SubjectDTO;
            string newId = await _subjectRepository.Insert(Utilities.ConvertToEntity(subject!));
            return new ResponseDTO
            {
                IsValid = newId != null,
                Message = newId != null ? "Exitoso" : "No hay registros",
                ResultData = newId
            };
        }

        public async Task<ResponseDTO> Update(RequestDTO dto)
        {
            SubjectDTO subject = dto.Body as SubjectDTO;
            bool success = await _subjectRepository.Update(Utilities.ConvertToEntity(subject));
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
            bool success = await _subjectRepository.Delete(id);
            return new ResponseDTO
            {
                IsValid = success,
                Message = success ? "Exitoso" : "No hay registros por eliminar",
                ResultData = null
            };
        }
    }
}
