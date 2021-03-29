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
    public class Contexto : IdentityDbContext<Cliente, IdentityRole<Guid>, Guid>
    {
        public Contexto(DbContextOptions<Contexto> options): base(options) { }

        public DbSet<Cliente> Usuarios { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>()
                .HasOne(e => e.Endereco)
                .WithOne(c => c.Cliente).HasForeignKey<Endereco>(e => e.EnderecoId);


            builder.Entity<IdentityRoleClaim<Guid>>().HasKey(x => x.RoleId);
            builder.Entity<IdentityUserRole<Guid>>().HasKey(x => x.UserId);
            builder.Entity<IdentityUserClaim<Guid>>().HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<Guid>>().HasKey(x => x.UserId);
            builder.Entity<IdentityUserClaim<Guid>>().HasKey(x => x.Id);
            builder.Entity<IdentityUserToken<Guid>>().HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<Guid>>().ToTable("UsuarioPapel");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("Logins");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("Claims");
            builder.Entity<IdentityRole<Guid>>().ToTable("Papeis");
            base.OnModelCreating(builder);
        }
    }
}
