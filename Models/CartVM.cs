using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace selfCoding.Models
{
    public class CartVM
    {
        public List<CartItem> CartItems { get; set; }

        public decimal Total { get; set; }
    }
}