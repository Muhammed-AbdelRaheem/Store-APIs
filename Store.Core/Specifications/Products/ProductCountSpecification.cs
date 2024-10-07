using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Products
{
    public class ProductCountSpecification : BaseSpecifications<Product, int>
    {

        public ProductCountSpecification(ProductSpecParams productSpec) : base(

                 P =>
                 (!productSpec.brandId.HasValue || productSpec.brandId == P.BrandId)
                 &&
                 (!productSpec.typeId.HasValue || productSpec.typeId == P.TypeId)
                    )
        {



        }

    }
}
