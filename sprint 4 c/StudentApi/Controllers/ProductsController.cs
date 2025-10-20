using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    /// <summary>
    /// Exposes CRUD operations for products over HTTP.  The controller
    /// delegates to the ProductService for business logic and uses
    /// attribute routing to map endpoints.  Each method returns appropriate
    /// HTTP status codes.  The search parameter demonstrates simple LINQ
    /// querying.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns all products or filters them by a search term if provided.
        /// Example: GET /api/products?search=lapis
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? search)
        {
            var products = await _service.GetAllAsync(search);
            return Ok(products);
        }

        /// <summary>
        /// Returns a single product by id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Creates a new product.  Returns a 201 Created response with a
        /// Location header pointing to the newly created resource.  Uses
        /// CreatedAtAction to comply with RESTful conventions.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var created = await _service.CreateAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing product.  If the product does not exist a 404 Not
        /// Found is returned.  Otherwise a 204 No Content response is returned.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            var updated = await _service.UpdateAsync(id, product);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes a product by id.  Returns 204 No Content when successful or
        /// 404 Not Found if the product doesn't exist.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}