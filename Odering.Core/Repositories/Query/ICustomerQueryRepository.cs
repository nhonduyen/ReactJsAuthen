using Odering.Core.Entities;
using Odering.Core.Repositories.Query.Base;

namespace Odering.Core.Repositories.Query
{
    public interface ICustomerQueryRepository : IQueryRepository<Customer>
    {
        //Custom operation which is not generic
        Task<Customer> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Customer> GetCustomerByEmail(string email, CancellationToken ct = default);
    }
}
