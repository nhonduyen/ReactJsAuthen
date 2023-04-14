using System.Linq.Expressions;

namespace Odering.Core.Repositories.Query.Base
{
    public interface IQueryRepository<T> where T : class
    {
        // Generic repository for all if any
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
    }
}
