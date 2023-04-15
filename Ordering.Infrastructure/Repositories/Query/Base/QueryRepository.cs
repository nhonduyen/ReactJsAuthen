using Microsoft.EntityFrameworkCore;
using Odering.Core.Repositories.Query.Base;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories.Query.Base
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        protected readonly OrderingContext _context;
        private readonly DbSet<T> _table;

        public QueryRepository(OrderingContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        {
            var results = await _context.Set<T>().AsNoTracking().ToListAsync(ct);
            return results;
        }

        public async Task<IReadOnlyList<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken ct = default)
        {
            var results = await _context.Set<T>().AsNoTracking().Where(expression).ToListAsync(ct);
            return results;
        }
    }
}

