using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            var tokenHandler = new TokenHandler(_configuration);
            var token = tokenHandler.GenerateToken("1", "Juan", "juanklk@gmail.com", "Admin");

            // Retornar ambos en un objeto anónimo
            return Ok(new
            {
                Sizes = sizes,
                Token = token
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            
            return Ok("Data for admin only");
        }

    }
}
