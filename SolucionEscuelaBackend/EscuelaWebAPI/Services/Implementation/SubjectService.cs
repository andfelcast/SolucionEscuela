using Escuela.Domain.Repositories;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.Services.Interfaces;

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
