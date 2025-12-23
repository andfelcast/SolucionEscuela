using Escuela.Domain.Entities;
using Escuela.Domain.Repositories;
using EscuelaWebAPI.DTO.Auth;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.DTO.Student;
using EscuelaWebAPI.Services.Interfaces;
using EscuelaWebAPI.Utils;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace EscuelaWebAPI.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions options;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<ResponseDTO> Authenticate(LoginDTO dto) {            
            StudentDTO student = Utilities.ConvertToDto(await _authRepository.Login(dto.UserName, Utilities.Encrypt(dto.Password)))!;
            return new ResponseDTO
            {
                IsValid = student != null,
                Message = student != null ? "Exitoso" : "Login fallido",
                ResultData = student != null ? new
                {
                    UserId = student!.Id,
                    UserName = student.UserName,
                    Token = Utilities.GenerateJWT(student)
                } : null
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
