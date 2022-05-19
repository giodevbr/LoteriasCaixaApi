using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterias.Infra.Data.Repository.Interfaces
{
    public interface IBaseRepository <TEntity> where TEntity : class
    {
        public TEntity? ObterPorId(int id);

        public Task<TEntity?> ObterPorIdAsync(int id);

        public IList<TEntity> ObterTodos();

        public Task<IList<TEntity>> ObterTodosAsync();

        public TEntity Cadastrar(TEntity entidade);

        public Task<TEntity> CadastrarAsync(TEntity entidade);

        public void Atualizar(TEntity entidade);

        public Task AtualizarAsync(TEntity entidade);

        public void Deletar(TEntity entidade);

        public Task DeletarAsync(TEntity entidade);

        public void Dispose();

        public ValueTask DisposeAsync();
    }
}
