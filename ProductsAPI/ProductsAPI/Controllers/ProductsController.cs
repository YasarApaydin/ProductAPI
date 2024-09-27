
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext context;

        public ProductsController(ProductsContext _context)
                   {

            context = _context;
                      
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() 
        {
            var products = await context.Products.Where(i => i.IsActive).Select(p=>ProductToDTO(p)).ToListAsync();

            return Ok(products);
        
        }
        
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var p = await context.Products.Where(x => x.ProductId == id).Select(p => ProductToDTO(p)).FirstOrDefaultAsync();
            if(p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            context.Products.Add(entity);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct),new {id =entity.ProductId},entity);

        }


        [HttpPut("{id}")]
public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {
            if(id != entity.ProductId)
            {
                return BadRequest();
            }

            var product = await context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

             if(product == null)
            {
                return NotFound();
            }

            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.IsActive = entity.IsActive;
            try
            {
                await context.SaveChangesAsync();

            }catch(Exception )
            {
                return NotFound();

            }
            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if(product == null)
            {
                return NotFound();
            }


            context.Products.Remove(product);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();

        }



        public static ProductDTO ProductToDTO(Product p)
        {
            var entity = new ProductDTO();
            if(entity != null)
            {
                entity.ProductId = p.ProductId;
                entity.ProductName = p.ProductName;
                entity.Price = p.Price;
            }


            return entity;
        }
    
    }
}
