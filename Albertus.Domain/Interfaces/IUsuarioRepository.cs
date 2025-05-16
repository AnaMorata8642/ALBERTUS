using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albertus.Domain.Models;

namespace Albertus.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AdicionarAsync(Usuario usuario);            // Para cadastro
        Task<Usuario> BuscarPorEmailAsync(string email); // Para login
        Task<Usuario> BuscarPorIdAsync(Guid id);         // Para perfil/autorização
        Task<bool> UsuarioExisteAsync(string email);     // Para evitar duplicação
        //Task AtualizarAsync(Usuario usuario);          // Para atualizar dados
    }
}