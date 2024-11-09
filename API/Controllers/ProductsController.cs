using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
        return Ok(await repo.GetProductsAsync());
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);

        if(await repo.SaveChanggesAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
        }

        return BadRequest("Problem creating product");
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id)) 
            return BadRequest("Cannot update product");

        repo.UpdateProduct(product);

        if(await repo.SaveChanggesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);

        if (product == null) return NotFound();

        repo.DeleteProduct(product);

        if(await repo.SaveChanggesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting product");
    }

    private bool ProductExists(int id)
    {
        return repo.ProductExists(id);
    }
}
