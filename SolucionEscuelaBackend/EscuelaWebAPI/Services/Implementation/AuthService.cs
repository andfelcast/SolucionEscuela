using Escuela.Domain.Repositories;
using EscuelaWebAPI.DTO.Auth;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.Services.Interfaces;
using EscuelaWebAPI.Utils;

namespace EscuelaWebAPI.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<ResponseDTO> Authenticate(LoginDTO dto) {
            bool isValid = await _authRepository.Login(dto.UserName, Utilities.Encrypt(dto.Password));
            return new ResponseDTO
            {
                IsValid = isValid,
                Message = isValid ? "Exitoso" : "Login fallido",
            };
        }
    }
}
