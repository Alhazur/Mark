using Mark.Models.Class;
using Microsoft.EntityFrameworkCore;

namespace Mark.DataBase
{
    public class MarkDbContext : DbContext
    {
        public MarkDbContext(DbContextOptions<MarkDbContext> options) : base(options) { }
        public DbSet<Companies> Companiesdb { get; set; }
        public DbSet<Stores> Storesdb { get; set; }
    }
}
