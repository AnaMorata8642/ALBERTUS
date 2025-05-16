using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Albertus.Domain.DTOs;
using Albertus.Domain.Interfaces;
using Albertus.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Albertus.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<bool> RegistrarUsuarioAsync(UsuarioRegisterDTO usuarioRegisterDTO)
        {
            var usuarioExistente = await _usuarioRepository.BuscarPorEmailAsync(usuarioRegisterDTO.email);
            if (usuarioExistente != null)
                return false; // Já existe usuário com esse email

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioRegisterDTO.senha);

            var novoUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                nome = usuarioRegisterDTO.nome,
                email = usuarioRegisterDTO.email,
                senhaHash = senhaHash
            };

            await _usuarioRepository.AdicionarAsync(novoUsuario);
            return true;
        }

        public async Task<string> LoginUsuarioAsync(UsuarioLoginDTO usuarioLoginDTO)
        {
            var usuario = await _usuarioRepository.BuscarPorEmailAsync(usuarioLoginDTO.email);
            if (usuario == null)
                return null;

            var senhaValida = BCrypt.Net.BCrypt.Verify(usuarioLoginDTO.senha, usuario.senhaHash);
            if (!senhaValida)
                return null;

            // Gerarando Token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.email)
            }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BCrypt.Net;
//using Albertus.Domain.DTOs;
//using Albertus.Domain.Interfaces;
//using Albertus.Domain.Models;

//namespace Albertus.Domain.Services
//{
//    public class UsuarioService : IUsuarioService
//    {
//        private readonly IUsuarioRepository _usuarioRepository;

//        public UsuarioService(IUsuarioRepository usuarioRepository)
//        {
//            _usuarioRepository = usuarioRepository;
//        }

//        public async Task<bool> RegistrarUsuarioAsync(UsuarioRegisterDTO usuarioRegisterDTO)
//        {
//            var usuarioExistente = await _usuarioRepository.BuscarPorEmailAsync(usuarioRegisterDTO.email);
//            if (usuarioExistente != null)
//                return false; // Já existe usuário com esse email

//            var novoUsuario = new Usuario(
//                Guid.NewGuid(),
//                usuarioRegisterDTO.nome,
//                usuarioRegisterDTO.email,

//                BCrypt.Net.BCrypt.HashPassword(usuarioRegisterDTO.senha) // Criptografar Senha!
//            );

//            await _usuarioRepository.AdicionarAsync(novoUsuario);
//            return true;
//        }

//        public async Task<string> LoginUsuarioAsync(UsuarioLoginDTO usuarioLoginDTO)
//        {
//            var usuario = await _usuarioRepository.BuscarPorEmailAsync(usuarioLoginDTO.email);
//            if (usuario == null)
//                return null;

//            var senhaValida = BCrypt.Net.BCrypt.Verify(usuarioLoginDTO.senha, usuario.senhaHash);
//            if (!senhaValida)
//                return null;

//            return "Usuário Logado com Sucesso!";
//        }
//    }
//}