using EscuelaWebAPI.DTO.General;

namespace EscuelaWebAPI.Services.Interfaces
{
    public interface IStudentService : IGeneralService
    {
        Task<ResponseDTO> AddSubjects(RequestDTO dto);
    }
}
