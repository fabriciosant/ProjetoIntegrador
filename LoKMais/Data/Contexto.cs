using LoKMais.Models;
using LoKMais.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Data
{
    public class Contexto : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public Contexto(DbContextOptions<Contexto> options): base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }
    }
}
