using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
      var spec = new ProductSpecification(brand, type, sort);
      var products = await repo.ListAsync(spec);
      return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id){
      var product = await repo.GetByIdAsync(id);
      if(product is null){
        return NotFound();
      }
      return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product){
      repo.Add(product);
      if(await repo.SaveChangesAsync()){
        return CreatedAtAction("GetProduct", new {id = product.Id}, product);
      }
      return product;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product){
      if(id != product.Id || !repo.Exists(id)){
        return BadRequest("Cannot update this product");
      }
      repo.Update(product);
      if(await repo.SaveChangesAsync()){
        return NoContent();
      }
      return BadRequest("Failed to update product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id){
      var product = await repo.GetByIdAsync(id);
      if(product is null){
        return NotFound("Product not found");
      }
      repo.Remove(product);
      if(await repo.SaveChangesAsync()){
        return NoContent();  
      }
      return BadRequest("Failed to delete product");
    }
    
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
      var spec = new BrandListSpecifications();
      return Ok(await repo.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
      var spec = new TypeListSpecifications();
      return Ok(await repo.ListAsync(spec));
    }
}