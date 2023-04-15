namespace Odering.Core.Repositories.Command.Base
{
    public interface ICommandRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellation = default);
        Task UpdateAsync(T entity, CancellationToken cancellation = default);
        Task DeleteAsync(T entity, CancellationToken cancellation = default);
    }
}
