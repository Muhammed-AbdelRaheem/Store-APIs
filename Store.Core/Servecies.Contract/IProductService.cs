using Store.Core.Dtos;
using Store.Core.Helper;
using Store.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Servecies.Contract
{
    public interface IProductService
    {

        Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec);
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<ProductDto> GetProductById(int id);


    }
}
