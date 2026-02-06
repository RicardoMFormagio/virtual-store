using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Roles;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productService.GetAllProducts();

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }
    
    [HttpGet]
    public async Task<ActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        return View();
    }
    
    [HttpGet]
    public async Task<ActionResult> UpdateProduct(int id)
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");

        var result = await _productService.FindProductById(id);

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }
    
    [HttpGet]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var result = await _productService.FindProductById(id);

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVMRequest)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProduct(productVMRequest);

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
            }
        }

        return View(productVMRequest);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVMRequest)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProduct(productVMRequest);

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        return View(productVMRequest);
    }
    
    [Authorize(Roles = Role.Admin )]
    [HttpPost(), ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        
        var result = await _productService.DeleteProductById(id);

        if (!result)
        {
            return View("Error");
        }

        return RedirectToAction(nameof(Index));
    }
    
    
}