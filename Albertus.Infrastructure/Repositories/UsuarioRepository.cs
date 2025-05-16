using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albertus.Domain.Models;
using Albertus.Infrastructure.Data;
using Albertus.Infrastructure.Repositories;
using Albertus.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Albertus.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> BuscarPorEmailAsync(string email)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<Usuario> BuscarPorIdAsync(Guid id)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UsuarioExisteAsync(string email)
        {
            return await _context.Usuario.AnyAsync(u => u.email == email);
        }
    }
}