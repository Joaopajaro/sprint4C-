using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    /// <summary>
    /// Represents a simple product stored in the system.  Each product has an
    /// identifier, a name, an optional description, a price and a quantity.
    /// The DataAnnotations attributes are used both for validation on input and
    /// to drive the OpenAPI schema generation in Swagger.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Database‑generated identifier.  When using the in‑memory provider
        /// Entity Framework Core will assign this automatically.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the product.  This field is required.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional description for the product.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Price of the product.  Must be a non‑negative value.
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity in stock.
        /// </summary>
        public int Quantity { get; set; }
    }
}