using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YukaBLL.Contracts;
using YukaWeb.TokenEmissor;

namespace YukaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        private readonly IConfiguration _configuration;
        public SizeController(ISizeService sizeService, IConfiguration configuration)
        {
            _sizeService = sizeService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Obtener los tamaños desde el servicio
            var sizes = await _sizeService.GetAllAsync();

            // Retornar ambos en un objeto anónimo
            return Ok(sizes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Message = "Data for admin only",
                UserId = userId,
                UserRole = userRole
            });
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            //var tokenHandler = new TokenHandler(_configuration);
            if (email == "juanklk@gmail.com" && password == "klk123")
                return Ok(GenerateToken("1", "Juan", email, "Admin"));

            return Unauthorized();
        }

        private string GenerateToken(string userId, string userName, string email, string role)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.GivenName, userName),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8), // Duración del token
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
