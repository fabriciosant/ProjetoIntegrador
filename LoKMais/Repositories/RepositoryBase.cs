using LoKMais.Data;
using LoKMais.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Repositories
{
    public class RepositoryBase<TEntity>: IResositoryBase<TEntity> where TEntity : class
    {
        protected readonly LkContextDB _contexto;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(LkContextDB contexto)
        {
            _contexto = contexto;
            _dbSet = _contexto.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> All() => _dbSet.AsQueryable();
        public virtual TEntity Find(Guid Id) => _dbSet.Find(Id);

        public void Dispose()
        {
            _contexto.Dispose();
            GC.SuppressFinalize(this);
        }

        public async virtual Task SaveAsync(params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _contexto.SaveChangesAsync();
        }

        public async virtual Task RemoveAsync(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
            await _contexto.SaveChangesAsync();
        }


        public async virtual Task UpdateAsync(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
            await _contexto.SaveChangesAsync();
        }
    }
}
