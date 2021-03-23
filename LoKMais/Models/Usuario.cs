using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models.ViewModels
{
    public class Usuario : IdentityUser<Guid>
    {
        public Usuario(string userName) : base(userName)
        {
        }

        public string Telefone { get; set; }

        [ForeignKey("EnderecoId")]
        public Endereco Enderecos { get; set; }
    }
}
