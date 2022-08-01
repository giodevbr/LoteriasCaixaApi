using Loterias.Infra.Data.Model;
using Loterias.Infra.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Loterias.Infra.Data.Repository.Repositories
{
    public class BaseRepository<TEntity> : IDisposable, IAsyncDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        protected AppDbContext _dbContext;

        private bool _disposed = false;

        public BaseRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public IList<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public bool Any()
        {
            return _dbContext.Set<TEntity>().Any();
        }

        public async Task<bool> AnyAsync()
        {
            return await _dbContext.Set<TEntity>().AnyAsync();
        }

        public TEntity Add(TEntity entidade)
        {
            var novaEntidade = _dbContext.Set<TEntity>().Add(entidade);

            _dbContext.SaveChanges();

            return novaEntidade.Entity;
        }

        public async Task<TEntity> AddAsync(TEntity entidade)
        {
            var novaEntidade = await _dbContext.Set<TEntity>().AddAsync(entidade);

            await _dbContext.SaveChangesAsync();

            return novaEntidade.Entity;
        }

        public void Update(TEntity entidade)
        {
            try
            {
                _dbContext.Set<TEntity>().Update(entidade);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        public async Task UpdateAsync(TEntity entidade)
        {
            try
            {
                _dbContext.Set<TEntity>().Update(entidade);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                await DisposeAsync();
                throw;
            }
        }

        public void Delete(TEntity entidade)
        {
            try
            {
                _dbContext.Set<TEntity>().Remove(entidade);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        public async Task DeleteAsync(TEntity entidade)
        {
            try
            {
                _dbContext.Set<TEntity>().Remove(entidade);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                await DisposeAsync();
                throw;
            }
        }

        public void Dispose()
        {
            if (_disposed || _dbContext == null)
                return;

            _dbContext.Dispose();
            _disposed = true;

            GC.SuppressFinalize(this);
            GC.Collect();
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed || _dbContext == null)
                return;

            await _dbContext.DisposeAsync();

            _disposed = true;

            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
