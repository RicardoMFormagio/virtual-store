using VShop.Web.Models;

namespace VShop.Web.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts();
    Task<ProductViewModel> FindProductById(int id);
    Task<ProductViewModel> CreateProduct(ProductViewModel productVMRequest);
    Task<ProductViewModel> UpdateProduct(ProductViewModel productVMRequest);
    Task<bool> DeleteProductById(int id);
}