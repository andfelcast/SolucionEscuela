using EscuelaWebAPI.DTO.General;
using EscuelaWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EscuelaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;
        private readonly ILogger<SubjectController> _logger;

        public SubjectController(ISubjectService service, ILogger<SubjectController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogTrace("Start Subject List service");
            ResponseDTO response = await _service.GetAll();
            if (!response.IsValid)
            {
                _logger.LogWarning("GetAll: " + response.Message);
                return NotFound(response);
            }
            _logger.LogInformation("Subject List Service executed");
            return Ok(response);
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetById(RequestDTO dto)
        {
            _logger.LogTrace("Start Subject Detail service");
            ResponseDTO response = await _service.GetById(dto);
            if (!response.IsValid)
            {
                _logger.LogWarning("GetById: " + response.Message);
                return NotFound(response);
            }
            _logger.LogInformation("Subject Detail Service executed");
            return Ok(response);
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(RequestDTO dto)
        {
            ResponseDTO response = await _service.CreateNew(dto);
            if (!response.IsValid)
            {
                _logger.LogError("Register: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Subject Creation Service executed");
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(RequestDTO dto)
        {
            ResponseDTO response = await _service.Update(dto);
            if (!response.IsValid)
            {
                _logger.LogError("Update: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Subject Update Service executed");
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(RequestDTO dto)
        {
            ResponseDTO response = await _service.Delete(dto);
            if (!response.IsValid)
            {
                _logger.LogError("Delete: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Subject Delete Service executed");
            return Ok(response);
        }
    }
}
