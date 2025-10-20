using Microsoft.EntityFrameworkCore;
using StudentApi.Models;

namespace StudentApi.Data
{
    /// <summary>
    /// ApplicationDbContext coordinates Entity Framework Core functionality for
    /// the data model.  It derives from DbContext and exposes a DbSet for
    /// Products.  Additional DbSets can be added here as the domain grows.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Products table.  In an in‑memory database this will be created at
        /// runtime; when using a real provider EF Core will generate the table
        /// schema based off this DbSet.
        /// </summary>
        public DbSet<Product> Products => Set<Product>();
    }
}