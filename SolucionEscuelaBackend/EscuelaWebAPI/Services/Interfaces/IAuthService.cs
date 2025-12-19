using EscuelaWebAPI.DTO.Auth;
using EscuelaWebAPI.DTO.General;

namespace EscuelaWebAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO> Authenticate(LoginDTO dto);
    }
}
