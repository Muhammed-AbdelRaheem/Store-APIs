﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities.Basket
{
    public class BasketItem
    {

        public int Id { get; set; }
        public string PictureProduct { get; set; }

        public string Brand { get; set; }

        public string Category { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
