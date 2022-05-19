using Loterias.Infra.Data.Model;
using Loterias.Infra.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Loterias.Infra.Data.Repository.Services
{
    public class BaseRepository<TEntity> : IDisposable, IAsyncDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        protected AppDbContext _dbContext;

        private bool _disposed = false;

        public BaseRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public TEntity? ObterPorId(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity?> ObterPorIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public IList<TEntity> ObterTodos()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public async Task<IList<TEntity>> ObterTodosAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public TEntity Cadastrar(TEntity entidade)
        {
            var novaEntidade = _dbContext.Set<TEntity>().Add(entidade);

            Salvar();

            return novaEntidade.Entity;
        }

        public async Task<TEntity> CadastrarAsync(TEntity entidade)
        {
            var novaEntidade = await _dbContext.Set<TEntity>().AddAsync(entidade);

            await SalvarAsync();

            return novaEntidade.Entity;
        }

        public void Atualizar(TEntity entidade)
        {
            try
            {
                _dbContext.Set<TEntity>().Update(entidade);

                Salvar();
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        public async Task AtualizarAsync(TEntity entidade)
        {
            try
            {
                _dbContext.Set<TEntity>().Update(entidade);

                await SalvarAsync();
            }
            catch (Exception)
            {
                await DisposeAsync();
                throw;
            }
        }

        public void Deletar(TEntity entidade)
        {
            try
            {
                _dbContext.Set<TEntity>().Remove(entidade);

                Salvar();
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        public async Task DeletarAsync(TEntity entidade)
        {
            try
            {
                _dbContext.Set<TEntity>().Remove(entidade);

                await SalvarAsync();
            }
            catch (Exception)
            {
                await DisposeAsync();
                throw;
            }
        }

        private void Salvar()
        {
            _dbContext.SaveChanges();
        }

        private async Task SalvarAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_disposed || _dbContext == null)
                return;

            _dbContext.Dispose();
            _disposed = true;
            
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed || _dbContext == null)
                return;

            await _dbContext.DisposeAsync();
            _disposed = true;

            GC.SuppressFinalize(this);
        }
    }
}
