using Store.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Servecies.Contract
{
    public interface IProductService
    { 

       Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort, int? brandId,  int? typeId);
       Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
       Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
       Task<ProductDto> GetProductById(int id);


    }
}
