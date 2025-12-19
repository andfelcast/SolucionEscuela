using Escuela.Domain.Repositories;
using EscuelaWebAPI.DTO.Auth;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.Services.Interfaces;

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
            return null;        
        }
    }
}
