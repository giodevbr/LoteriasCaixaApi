namespace Loterias.Infra.Data.Repository.Interfaces
{
    public interface IBaseRepository <TEntity> where TEntity : class
    {
        TEntity? ObterPorId(int id);

        Task<TEntity?> ObterPorIdAsync(int id);

        IList<TEntity> ObterTodos();

        Task<IList<TEntity>> ObterTodosAsync();

        bool Existe();

        Task<bool> ExisteAsync();

        TEntity Cadastrar(TEntity entidade);

        Task<TEntity> CadastrarAsync(TEntity entidade);

        void Atualizar(TEntity entidade);

        Task AtualizarAsync(TEntity entidade);

        void Deletar(TEntity entidade);

        Task DeletarAsync(TEntity entidade);

        void Dispose();

        ValueTask DisposeAsync();
    }
}
