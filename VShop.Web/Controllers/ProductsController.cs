using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Roles;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

[Authorize(Roles = Role.Admin)]
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
        var result = await _productService.GetAllProducts(await GetAccessTokenAsync());

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [HttpGet]
    public async Task<ActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessTokenAsync()), "CategoryId", "Name");
        return View();
    }
    
    [HttpGet]
    public async Task<ActionResult> UpdateProduct(int id)
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessTokenAsync()), "CategoryId", "Name");

        var result = await _productService.FindProductById(id, await GetAccessTokenAsync());

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }
    
    [HttpGet]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var result = await _productService.FindProductById(id, await GetAccessTokenAsync());

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productVMRequest)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProduct(productVMRequest, await GetAccessTokenAsync());

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessTokenAsync()), "CategoryId", "Name");
            }
        }

        return View(productVMRequest);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productVMRequest)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProduct(productVMRequest, await GetAccessTokenAsync());

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        return View(productVMRequest);
    }
    
    [HttpPost(), ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        
        var result = await _productService.DeleteProductById(id, await GetAccessTokenAsync());

        if (!result)
        {
            return View("Error");
        }

        return RedirectToAction(nameof(Index));
    }
    private async Task<string> GetAccessTokenAsync()
    {
        return await HttpContext.GetTokenAsync("access_token");
    }
    
}