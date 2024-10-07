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

        public ProductSpecification(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {

                    case "priceAsc":
                        AddOrderBy(P => P.Price );
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P=>P.Price);
                        break;

                    default:
                        AddOrderBy(P => P.Name)  ; 
                        break;

                }

            }
            else
            {
                AddOrderBy(P => P.Name)  ;

            }


            ApplyIncludes();


        }


        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);

        }
    }
}
