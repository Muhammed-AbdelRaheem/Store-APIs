using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Products
{
    public class ProductSpecParams
    {

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }
        public string? sort{get;set;}
        public int? brandId{get;set;}
        public int? typeId { get; set; }

        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize ; }
            set { pageSize = value >18 ?18:value; }
        }
        public int pageIndex { get; set; } = 1;


    }
}
