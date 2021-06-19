using LoKMais.Data;
using LoKMais.Interfaces;
using LoKMais.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Repositories
{
    public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(LkContextDB contexto): base(contexto) { }

        public async Task<IList<Endereco>> BuscarTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Endereco> BuscarEnderecoPorIdAsync(Guid enderecoId)
        {
            var result = await _dbSet.FirstOrDefaultAsync(endereco => endereco.EnderecoId == enderecoId);
           
            return result;
        }
    }
}
