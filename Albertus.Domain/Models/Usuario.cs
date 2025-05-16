using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albertus.Domain.Models
{
    public class Usuario
    {
        public Usuario() { }

        public Guid Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senhaHash { get; set; }

        public Usuario(Guid id, string nome, string email, string senhaHash)
        {
            Id = id;
            this.nome = nome;
            this.email = email;
            this.senhaHash = senhaHash;
        }
    }
}
