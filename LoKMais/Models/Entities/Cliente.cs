using LoKMais.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LoKMais.Models
{
    public class Cliente : IdentityUser<Guid>
    {
        public string NomeCompleto { get; set; }
        public virtual Endereco Endereco { get; set; }
        public IList<Veiculo> Veiculo { get; set; }
    }
}
