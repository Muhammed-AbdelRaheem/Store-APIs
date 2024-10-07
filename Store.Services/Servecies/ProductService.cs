using AutoMapper;
using Store.Core.Dtos;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Core.Servecies.Contract;
using Store.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Servecies
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort , int? brandId,  int? typeId)
        {
            var spec = new ProductSpecification(sort ,brandId,typeId);
            var AllProducts = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
            var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(AllProducts);
            return mappedProducts;
        }
         

        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecification(id);

            var ProductId = await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec);
            var mappedProductId = _mapper.Map<ProductDto>(ProductId);
            return mappedProductId;



        }


        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            var AllTypes = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedAllTypes = _mapper.Map<IEnumerable<TypeBrandDto>>(AllTypes);
            return mappedAllTypes;
        }
        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            var AllBrands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            var mappedAllBrands = _mapper.Map<IEnumerable<TypeBrandDto>>(AllBrands);
            return mappedAllBrands;
        }


      










    }
}
