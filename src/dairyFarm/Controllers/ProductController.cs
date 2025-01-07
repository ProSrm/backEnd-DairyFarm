using dairyFarm.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dairyFarm.Entity;


namespace dairyFarm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DFdbContext _context;

        public ProductController(DFdbContext context)
        {
            _context = context;
        }
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.99m },
            new Product { Id = 2, Name = "Product 2", Price = 15.99m },
            new Product { Id = 3, Name = "Product 3", Price = 22.99m }
        };
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }


        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}