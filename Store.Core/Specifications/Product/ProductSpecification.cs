using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Products
{
    public class ProductSpecification : BaseSpecifications<Product, int>
    {

        public ProductSpecification(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();
        }

        public ProductSpecification()
        {
            ApplyIncludes();

        }


        private void ApplyIncludes()
        {
            Includes.Add(P=>P.Brand);
            Includes.Add(P => P.Type);

        }
    }
}
