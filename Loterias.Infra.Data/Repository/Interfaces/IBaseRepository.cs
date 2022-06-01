namespace Loterias.Infra.Data.Repository.Interfaces
{
    public interface IBaseRepository <TEntity> where TEntity : class
    {
        TEntity? GetById(int id);

        Task<TEntity?> GetByIdAsync(int id);

        IList<TEntity> GetAll();

        Task<IList<TEntity>> GetAllAsync();

        bool Any();

        Task<bool> AnyAsync();

        TEntity Add(TEntity entidade);

        Task<TEntity> AddAsync(TEntity entidade);

        void Update(TEntity entidade);

        Task UpdateAsync(TEntity entidade);

        void Delete(TEntity entidade);

        Task DeleteAsync(TEntity entidade);

        void Dispose();

        ValueTask DisposeAsync();
    }
}
