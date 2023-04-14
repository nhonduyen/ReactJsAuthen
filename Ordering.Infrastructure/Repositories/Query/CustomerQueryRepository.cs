using Odering.Core.Entities;
using Odering.Core.Repositories.Query;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories.Query.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories.Query
{
    public class CustomerQueryRepository : QueryRepository<Customer>, ICustomerQueryRepository
    {
        public CustomerQueryRepository(OrderingContext context) : base(context)
        {
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            var customer = await base.FindByConditionAsync(x => x.Id == id);
            return customer.FirstOrDefault();
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var customer = await base.FindByConditionAsync(x => x.Email == email);
            return customer.FirstOrDefault();
        }
    }
}
