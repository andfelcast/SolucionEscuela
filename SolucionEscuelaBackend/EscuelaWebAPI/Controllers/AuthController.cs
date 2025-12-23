using EscuelaWebAPI.DTO.Auth;
using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EscuelaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IStudentService _studentService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService service, IStudentService studentService, IConfiguration configuration, ILogger<AuthController> logger) {
            _service = service;
            _studentService = studentService;
            _configuration = configuration;
            _logger = logger;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO dto)
        {
            _logger.LogTrace("Start Login service");
            ResponseDTO response = await _service.Authenticate(dto);
            if (!response.IsValid)
            {
                _logger.LogWarning("Login: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Login Service executed");
            return Ok(response);
        }

        [HttpGet("ValidateToken/{token}")]
        public async Task<ActionResult> ValidateToken(string token) {
            _logger.LogTrace("Start Validate Token service");
            ResponseDTO response = await _service.ValidateToken(token);
            if (!response.IsValid)
            {
                _logger.LogWarning("Validate Token: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Validate Token Service executed");
            return Ok(response);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RequestDTO dto)
        {
            ResponseDTO response = await _studentService.CreateNew(dto);
            if (!response.IsValid)
            {
                _logger.LogError("Register: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Student Creation Service executed");
            return Ok(response);
        }
    }
}
