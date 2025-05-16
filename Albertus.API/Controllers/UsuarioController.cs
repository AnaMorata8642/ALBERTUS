using Microsoft.AspNetCore.Mvc;
using Albertus.Domain.DTOs;
using Albertus.Domain.Interfaces;

namespace Albertus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegisterDTO dto)
        {
            var sucesso = await _usuarioService.RegistrarUsuarioAsync(dto);
            if (!sucesso)
                return BadRequest("Usuário Já Cadastrado!");

            return Ok("Usuário Cadastrado com Sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
        {
            var token = await _usuarioService.LoginUsuarioAsync(dto);
            if (token == null) return Unauthorized("E-mail ou Senha Inválidos!");

            return Ok(new { Token = token });
        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using Albertus.Domain.DTOs;
//using Albertus.Domain.Interfaces;

//namespace Albertus.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UsuarioController : ControllerBase
//    {
//        private readonly IUsuarioService _usuarioService;

//        public UsuarioController(IUsuarioService usuarioService)
//        {
//            _usuarioService = usuarioService;
//        }

//        [HttpPost("registrar")]
//        public async Task<IActionResult> Registrar([FromBody] UsuarioRegisterDTO usuarioRegisterDTO)
//        {
//            var sucesso = await _usuarioService.RegistrarUsuarioAsync(usuarioRegisterDTO);
//            if (!sucesso)
//                return BadRequest("E-mail Já Cadastrado!");

//            return Ok("Usuário Cadastrado com Sucesso!");
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO usuarioLoginDTO)
//        {
//            var resultado = await _usuarioService.LoginUsuarioAsync(usuarioLoginDTO);
//            if (resultado == null)
//                return Unauthorized("E-mail ou Senha Inválidos!");
//            return Ok(resultado);z
//        }
//    }
//}