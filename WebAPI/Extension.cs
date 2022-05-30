using BusinessObject;
using WebAPI.DTO;

namespace WebAPI
{
    public static class Extension
    {
        public static ProductDTO AsProductDTO(this Product prod)
        {
            return new ProductDTO
            {
                ProductName = prod.ProductName,
                CategoryId = prod.CategoryId,
                ProductId = prod.ProductId,
                UnitPrice = prod.UnitPrice,
                UnitsInStock = prod.UnitsInStock,
                Category = prod.Category
            };
        }
        public static CategoryDTO AsCategoryDTO(this Category category)
        {
            return new CategoryDTO
            {
               CategoryId = category.CategoryId,
               CategoryName = category.CategoryName,
               Products = category.Products,
            };
        }
    }
}
