using EscuelaWebAPI.DTO.General;

namespace EscuelaWebAPI.Services.Interfaces
{
    public interface IGeneralService
    {
        Task<ResponseDTO> GetAll();
        Task<ResponseDTO> GetById(RequestDTO dto);
        Task<ResponseDTO> CreateNew(RequestDTO dto);
        Task<ResponseDTO> Update(RequestDTO dto);
        Task<ResponseDTO> Delete(RequestDTO dto);
    }
}
