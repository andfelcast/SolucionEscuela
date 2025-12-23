using Escuela.Domain.Repositories;
using EscuelaWebAPI.DTO.Auth;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.Services.Interfaces;
using EscuelaWebAPI.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EscuelaWebAPI.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        public async Task<ResponseDTO> Authenticate(LoginDTO dto) {
            bool isValid = await _authRepository.Login(dto.UserName, Utilities.Encrypt(dto.Password));
            return new ResponseDTO
            {
                IsValid = isValid,
                Message = isValid ? "Exitoso" : "Login fallido",
            };
        }
        public async Task<ResponseDTO> ValidateToken(string token) {
            bool success = false;
            var claimsPrincipal = new ClaimsPrincipal();
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!))
            };
            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                success = true;
                return new ResponseDTO
                {
                    IsValid = success
                };
            }
            catch (Exception)
            {
                success = false;
                return new ResponseDTO
                {
                    IsValid = success
                };
            }            
        }
    }
}
