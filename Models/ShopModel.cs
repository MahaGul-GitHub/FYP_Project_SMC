﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwissMetroCookware.Models
{
    public class ShopModel
    {
        public List<Category> Cat { get; set; }
        public List<SubCategory> Sub { get; set; }
        public List<Product> Pro { get; set; }
    }
}