using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albertus.Domain.DTOs;

namespace Albertus.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> RegistrarUsuarioAsync(UsuarioRegisterDTO usuarioRegisterDTO);
        Task<string> LoginUsuarioAsync(UsuarioLoginDTO usuarioLoginDTO);
    }
}
