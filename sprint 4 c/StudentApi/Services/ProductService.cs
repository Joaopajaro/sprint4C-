using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Services
{
    /// <summary>
    /// Provides business logic for CRUD operations on Products.  The service
    /// interacts with the Entity Framework Core DbContext and exposes async
    /// methods that the controllers can call.  LINQ is used to build queries
    /// against the DbSet, demonstrating simple filtering.
    /// </summary>
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all products.  If a search term is provided the results are
        /// filtered using LINQ on the Name and Description properties.
        /// </summary>
        public async Task<List<Product>> GetAllAsync(string? search = null)
        {
            // Use IQueryable so that the LINQ query is only executed once at
            // enumeration time.  This avoids retrieving all rows when a filter
            // could have been applied in the database provider.
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search!) ||
                                          (p.Description != null &&
                                           p.Description.Contains(search!)));
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Returns a single product by identifier or null if none exists.
        /// </summary>
        public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        /// <summary>
        /// Adds a new product to the database and saves changes.
        /// </summary>
        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Updates an existing product.  Returns false if the product does not
        /// exist.  EF Core tracks the entity and will only send the changed
        /// columns to the provider when SaveChangesAsync is called.
        /// </summary>
        public async Task<bool> UpdateAsync(int id, Product product)
        {
            if (id != product.Id)
            {
                return false;
            }

            var existing = await _context.Products.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.Quantity = product.Quantity;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a product by id.  Returns false if the product is not found.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}