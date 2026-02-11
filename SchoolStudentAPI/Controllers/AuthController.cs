using Microsoft.AspNetCore.Mvc;
using SchoolStudentAPI.Dto;
using SchoolStudentAPI.Service;

namespace SchoolStudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var result = _service.Login(dto);

            if (result == null)
                return Unauthorized("Invalid Username or Password");

            return Ok(result);
        }
    }
}
