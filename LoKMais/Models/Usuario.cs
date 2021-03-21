using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models
{
    public class Usuario : IdentityUser<Guid>
    {
        public string NomeCompleto { get; set; }
        public string Endereco { get; set; }
    }
}
