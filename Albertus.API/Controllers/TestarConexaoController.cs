using Albertus.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Albertus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestarConexaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestarConexaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Testar()
        {
            var conectado = _context.Database.CanConnect();
            return Ok(new { conectado });
        }
    }
}