using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models
{
    public class Cliente : IdentityUser<Guid>
    {
        public virtual Endereco Endereco { get; set; }
    }
}
