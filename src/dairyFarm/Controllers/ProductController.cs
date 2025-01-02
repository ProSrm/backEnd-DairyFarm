using dairyFarm.Model;
using Microsoft.AspNetCore.Mvc;

namespace dairyFarm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.99m },
            new Product { Id = 2, Name = "Product 2", Price = 15.99m },
            new Product { Id = 3, Name = "Product 3", Price = 22.99m }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(products);
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;  
            products.Add(product);
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            products.Remove(product);
            return NoContent();
        }
    }
}
