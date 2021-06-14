using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Interfaces
{
    public interface IResositoryBase<TEntity> : IDisposable where TEntity : class
    {
        Task SaveAsync(params TEntity[] entities);
        Task UpdateAsync(params TEntity[] entities);
        Task RemoveAsync(params TEntity[] entities);
        IQueryable<TEntity> All();
        TEntity Find(Guid Id);
    }
}
