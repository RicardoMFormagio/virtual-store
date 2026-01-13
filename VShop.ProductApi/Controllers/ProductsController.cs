using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllProducts();
            if (products is null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
    {
        try
        {
            var product = await _productService.GetProductById(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDto)
    {
        try
        {
            if (productDto is null)
            {
                return BadRequest("Invalid Data");
            }
            await _productService.AddProduct(productDto);
            return Created();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDto)
    {
        try
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            if (productDto is null)
            {
                return BadRequest();
            }
            
            await _productService.UpdateProduct(productDto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var productDto = await _productService.GetProductById(id);
            if (productDto is null)
            {
                return NotFound("Product not found");
            }
            
            await _productService.RemoveProduct(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}