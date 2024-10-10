using Store.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }

        public List<BasketItem> Items { get; set; }


    }
}
