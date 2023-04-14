using Microsoft.EntityFrameworkCore;
using Odering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
