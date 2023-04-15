using Microsoft.EntityFrameworkCore;
using Odering.Core.Repositories.Command.Base;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories.Command.Base
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        protected readonly OrderingContext _context;

        public CommandRepository(OrderingContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellation = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellation = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellation);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellation = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellation);
        }
    }
}
