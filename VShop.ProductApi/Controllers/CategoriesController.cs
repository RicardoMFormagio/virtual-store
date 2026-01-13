using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
    {
        try
        {
            var categories = await _categoryService.GetCategoriesProducts();
            if (categories is null)
            {
                return NotFound();
            }
            return Ok(categories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        
    }
    
    [HttpGet("categories-products")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategoriesAndProducts()
    {
        try
        {
            var categories = await _categoryService.GetCategoriesProducts();
            return Ok(categories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
    {
        try
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDto)
    {
        try
        {
            if (categoryDto is null)
            {
                return BadRequest("Invalid Data");
            }
            await _categoryService.AddCategory(categoryDto);
            return Created();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDto)
    {
        try
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest();
            }

            if (categoryDto is null)
            {
                return BadRequest();
            }
            
            await _categoryService.UpdateCategory(categoryDto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var categoriaDto = await _categoryService.GetCategoryById(id);
            if (categoriaDto is null)
            {
                return NotFound("Category not found");
            }
            
            await _categoryService.RemoveCategory(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}