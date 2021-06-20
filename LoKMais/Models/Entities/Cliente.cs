using Microsoft.AspNetCore.Identity;
using System;

namespace LoKMais.Models
{
    public class Cliente : IdentityUser<Guid>
    {
        public string NomeCompleto { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
