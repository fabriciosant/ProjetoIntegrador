using LoKMais.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoKMais.Interfaces
{
    public interface IVeiculoRepository : IResositoryBase<Veiculo>
    {
        Task<IList<Veiculo>> BuscarTodosAsync();
        Task<Veiculo> BuscarPorIdAsync(Guid id);
    }
}
