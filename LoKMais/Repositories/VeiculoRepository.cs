using LoKMais.Data;
using LoKMais.Interfaces;
using LoKMais.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Repositories
{
    public class VeiculoRepository : RepositoryBase<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(LkContextDB contexto) : base(contexto) { }

        public async Task<IList<Veiculo>> BuscarTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Veiculo> BuscarPorIdAsync(Guid id)
        {
            var veiculoResult = await _dbSet.FirstOrDefaultAsync(veiculo => veiculo.Id == id);

            return veiculoResult;
        }
    }
}
