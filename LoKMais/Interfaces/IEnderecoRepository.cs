using LoKMais.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Interfaces
{
    public interface IEnderecoRepository : IResositoryBase<Endereco>
    {
        Task<IList<Endereco>> BuscarTodosAsync();
        Task<Endereco> BuscarEnderecoPorIdAsync(Guid enderecoId);
    }
}
